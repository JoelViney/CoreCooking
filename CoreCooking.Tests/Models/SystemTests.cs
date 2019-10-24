using CoreCooking.Data;
using CoreCooking.Models.Images;
using CoreCooking.Models.Sites;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CoreCooking.Models
{
    [TestClass]
    public class SystemTests
    {
        // This can be run manually to clean out all the dead images.
        [Ignore]
        [TestMethod]
        public async Task DeleteUnlinkedImages()
        {
            var siteRepository = new SiteRepository(SettingsFactory.GetConnectionString());
            var site = await siteRepository.GetAsync();

            var imageRepository = new ImageRepository(SettingsFactory.GetConnectionString());

            var list = await imageRepository.GetFileNamesAsync();

            foreach (var fileName in list)
            {
                bool found = false;
               
                foreach (var recipe in site.Recipes)
                {
                    if (recipe.ImageUrl != null && recipe.ImageUrl.Contains(fileName))
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    await imageRepository.DeleteAsync(fileName);
                }
            }
        }
        
    }
}
