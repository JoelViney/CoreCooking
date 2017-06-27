using CoreCooking.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CoreCooking.Data;

namespace CoreCooking.Models.Images
{
    public class ImageRepository
    {
        private AzureFileManager _fileManager;

        public ImageRepository(string connectionString)
        {
            this._fileManager = new AzureFileManager(connectionString, "pictures");
        }

        public ImageRepository(string connectionString, string containerPath) 
        {
            this._fileManager = new AzureFileManager(connectionString, containerPath);
        }
        

        public async Task<string> SaveAsync(Stream stream)
        {
            string fileName = String.Format("{0}.jpg", Guid.NewGuid());

            string url = await _fileManager.SaveFileAsync(fileName, stream);

            return url;
        }
    }
}
