using CoreCooking.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreCooking.Models.Sites
{
    public class SiteRepository
    {
        private AzureFileManager _fileManager;

        public SiteRepository(string connectionString)
        {
            this._fileManager = new AzureFileManager(connectionString, "site");
        }


        public virtual async Task<Site> GetAsync()
        {
            if (await _fileManager.FileExistsAsync("site.json") == false)
            {
                return new Site();
            }

            var json = await _fileManager.GetTextFileAsync("site.json");
            var item = JsonConvert.DeserializeObject<Site>(json);
            return item;
        }


        public virtual async Task SaveAsync(Site item)
        {
            string json = JsonConvert.SerializeObject(item);

            await _fileManager.SaveFileAsync("site.json", json);
        }

    }
}
