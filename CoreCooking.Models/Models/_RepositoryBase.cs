using CoreCooking.Parsers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreCooking.Data;

namespace CoreCooking.Models
{
    public abstract class RepositoryBase <T>
        where T : ModelBase
    {
        protected string ConnectionString { get; set; }

        private AzureFileManager _fileManager;

        #region Constructors...

        public RepositoryBase(string connectionString, string containerPath)
        {
            this.ConnectionString = connectionString;
            this._fileManager = new AzureFileManager(this.ConnectionString, containerPath);
        }

        #endregion

        public virtual async Task<T> FindAsync(Guid guid)
        {
            string fileName = this.GetFileName(guid);

            if (!await _fileManager.FileExistsAsync(fileName))
                return null;

            var item = await GetAsync(guid);

            return item;
        }

        public virtual async Task<T> GetAsync(Guid guid)
        {
            string fileName = this.GetFileName(guid);
            var json = await _fileManager.GetTextFileAsync(fileName);
            var item = JsonConvert.DeserializeObject<T>(json);
            return item;
        }

        public virtual async Task SaveAsync(T item)
        {
            if (item.Guid.Equals(Guid.Empty))
            {
                item.Guid = Guid.NewGuid();
            }

            string fileName = this.GetFileName(item);
            string json = JsonConvert.SerializeObject(item);

            await _fileManager.SaveFileAsync(fileName, json);
        }

        public virtual async Task DeleteAsync(T item)
        {
            string fileName = this.GetFileName(item);

            await _fileManager.DeleteFileAsync(fileName);
        }
        
        public virtual async Task<List<T>> GetListAsync()
        {
            var fileNames = await _fileManager.GetFileNamesAsync();

            var tasks = new List<Task<string>>();
            foreach (var fileName in fileNames)
            {
                tasks.Add(_fileManager.GetTextFileAsync(fileName));
            }

            IEnumerable<string> jsonFiles = Task.WhenAll(tasks).Result;

            var list = new List<T>();

            foreach (var json in jsonFiles)
            {
                var item = JsonConvert.DeserializeObject<T>(json);
                list.Add(item);
            }
            
            return list;
        }

        #region Helpoer Methods...

        private string GetFileName(T item)
        {
            return GetFileName(item.Guid);
        }

        private string GetFileName(Guid guid)
        {
            return String.Format("{0}.json", guid);
        }

        #endregion


    }
}
