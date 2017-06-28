using CoreCooking.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CoreCooking.Data;
using ImageSharp;

namespace CoreCooking.Models.Images
{
    public class ImageRepository
    {
        public const int MaxWidth = 800;
        public const int MaxHeight = 600;

        private AzureFileManager _fileManager;

        public ImageRepository(string connectionString)
        {
            this._fileManager = new AzureFileManager(connectionString, "pictures");
        }

        public ImageRepository(string connectionString, string containerPath) 
        {
            this._fileManager = new AzureFileManager(connectionString, containerPath);
        }

        public virtual async Task<List<string>> GetFileNamesAsync()
        {
            return await _fileManager.GetFileNamesAsync();
        }

        public virtual async Task DeleteAsync(string fileName)
        {
            await _fileManager.DeleteFileAsync(fileName);
        }

        public async Task<string> SaveAsync(Stream stream)
        {
            string fileName = String.Format("{0}.jpg", Guid.NewGuid());

            // Resize
            using (var image = Image.Load<Rgba32>(stream))
            {
                using (var resizedImage = ImageHelper.Resize(image, MaxWidth, MaxHeight))
                {
                    using (Stream stream2 = new MemoryStream())
                    {
                        resizedImage.Save(stream2);
                        stream2.Position = 0;
                        var url = await _fileManager.SaveFileAsync(fileName, stream2);

                        return url;

                    }
                }
            }
        }
    }
}
