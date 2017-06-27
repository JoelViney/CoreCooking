using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CoreCooking.Data
{
    /// <summary>
    /// This provides an interface for Azure Storage.
    /// </summary>
    public class AzureFileManager
    {
        public string ContainerPath { get; set; }
        private string ConnectionString { get; set; }

        #region Constructors...

        public AzureFileManager(string connectionString, string containerPath)
        {
            this.ConnectionString = connectionString;
            this.ContainerPath = containerPath;
        }

        #endregion

        public async Task<List<string>> GetFileNamesAsync()
        {
            var list = new List<string>();

            CloudBlobContainer container = await this.GetContainerAsync();
            BlobContinuationToken token = new BlobContinuationToken();

            var blobs = new List<IListBlobItem>();
            do
            {
                BlobResultSegment response = await container.ListBlobsSegmentedAsync(token);
                token = response.ContinuationToken;
                blobs.AddRange(response.Results);
            }
            while (token != null);

            foreach (var blob in blobs)
            {
                CloudBlockBlob cbb = (CloudBlockBlob)blob;
                list.Add(cbb.Name);
            }

            return list;
        }

        public async Task<bool> FileExistsAsync(string fileName)
        {
            var container = await this.GetContainerAsync();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            return await blockBlob.ExistsAsync();
        }
        
        /// <summary>
        /// Handles pictures being uploaded to the server.
        /// The pictures are then given a guid based name which is returned to the calling process.
        /// </summary>
        /// <returns>Returns the URI path to the saved file.</returns>
        public async Task<string> SaveFileAsync(string fileName, Stream stream)
        {
            var container = await this.GetContainerAsync();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            await blockBlob.UploadFromStreamAsync(stream);

            // https://[InsertYourStorageAccountNameHere].blob.core.windows.net/democontainer/HelloWorld.png
            string urlFilePathName = blockBlob.Uri.ToString();
                        
            return urlFilePathName;
        }

        /// <summary>
        /// Handles pictures being uploaded to the server.
        /// The pictures are then given a guid based name which is returned to the calling process.
        /// </summary>
        /// <returns>Returns the URI path to the saved file.</returns>
        public async Task<string> SaveFileAsync(string fileName, string text)
        {
            var container = await this.GetContainerAsync();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            
            await blockBlob.UploadTextAsync(text);

            // https://[InsertYourStorageAccountNameHere].blob.core.windows.net/democontainer/HelloWorld.png
            string urlFilePathName = blockBlob.Uri.ToString();
            
            return urlFilePathName;
        }

        public async Task<string> GetTextFileAsync(string fileName)
        {
            CloudBlobContainer container = await this.GetContainerAsync();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            if (await blockBlob.ExistsAsync())
            {
                string text = await blockBlob.DownloadTextAsync();

                return text;
            }

            return null;
        }


        public async Task<Stream> GetStreamFile(string fileName)
        {
            CloudBlobContainer container = await this.GetContainerAsync();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            Stream stream = new MemoryStream();

            if (await blockBlob.ExistsAsync())
            {
                await blockBlob.DownloadToStreamAsync(stream);
                long streamlen = stream.Length;
                stream.Position = 0;
            }

            return stream;
        }

        public async Task DeleteFileAsync(string fileName)
        {
            CloudBlobContainer container = await this.GetContainerAsync();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            if (await blockBlob.ExistsAsync())
            {
                await blockBlob.DeleteAsync();
            }
        }





        /// <summary>
        /// Returns the container that is used for all of the file interactions.
        /// </summary>
        /// <returns></returns>
        private async Task<CloudBlobContainer> GetContainerAsync()
        {
            // Retrieve storage account information from connection string
            // How to create a storage connection string - http://msdn.microsoft.com/en-us/library/azure/ee758697.aspx
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(this.ConnectionString);

            // Create a blob client for interacting with the blob service.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Create a container for organizing blobs within the storage account.
            CloudBlobContainer container = blobClient.GetContainerReference(this.ContainerPath);

            try
            {
                await container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, new BlobRequestOptions(), new OperationContext());
            }
            catch (StorageException ex)
            {
                Console.WriteLine("If you are running with the default configuration please make sure you have started the storage emulator."
                    + "Press the Windows key and type Azure Storage to select and run it from the existingList of applications - then restart the sample.");
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
                throw;
            }

            return container;
        }

        /// <summary>
        /// Validates the connection string information in app.config and throws an exception if it looks like 
        /// the user hasn't updated this to valid values. 
        /// </summary>
        /// <param name="connectionString">The storage connection string</param>
        /// <returns>CloudStorageAccount object</returns>
        private static CloudStorageAccount CreateStorageAccountFromConnectionString(string connectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(connectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }


    }
}
