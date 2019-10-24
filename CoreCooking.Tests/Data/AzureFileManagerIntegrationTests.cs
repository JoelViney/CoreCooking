using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CoreCooking.Data
{
    [TestClass]
    public class AzureFileManagerIntegrationTests
    {
        private AzureFileManager NewAzureFileManager()
        {
            var manager = new AzureFileManager(SettingsFactory.GetConnectionString(), "test");

            return manager;
        }


        [TestMethod]
        public async Task GetFolder()
        {
            AzureFileManager manager = this.NewAzureFileManager();

            await manager.GetFileNamesAsync();
        }

        [TestMethod]
        public async Task SaveFile()
        {
            AzureFileManager manager = this.NewAzureFileManager();

            string test = "Test File";
            // convert string to stream
            byte[] byteArray = Encoding.ASCII.GetBytes(test);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                string url = await manager.SaveFileAsync("Test.txt", stream);
            }
        }
    }
}
