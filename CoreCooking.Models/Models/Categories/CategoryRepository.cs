using CoreCooking.Models.Sites;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCooking.Models.Categories
{
    public class CategoryRepository : RepositoryBase<Category>
    {
        public CategoryRepository(string connectionString) : base(connectionString, "categories")
        {

        }


        public CategoryRepository(string connectionString, string containerPath) : base(connectionString, containerPath)
        {

        }

        public override async Task<Category> GetAsync(Guid guid)
        {
            var item = await base.GetAsync(guid);
            item.Recipes = item.Recipes.OrderBy(o => o.Name).ToList();
            return item;
        }

        public override async Task DeleteAsync(Category item)
        {
            await base.DeleteAsync(item);

            // Update the reference values
            var siteRepository = new SiteRepository(this.ConnectionString);
            var site = await siteRepository.GetAsync();

            var categoryReference = site.Categories.Where(x => x.Guid == item.Guid).FirstOrDefault();

            if (categoryReference != null)
            {
                site.Categories.Remove(categoryReference);
                await siteRepository.SaveAsync(site);
            }
        }

        public override async Task SaveAsync(Category item)
        {
            await base.SaveAsync(item);

            // Update the reference values
            var siteRepository = new SiteRepository(this.ConnectionString);
            var site = await siteRepository.GetAsync();

            var categoryReference = site.Categories.Where(x => x.Guid == item.Guid).FirstOrDefault();

            if (categoryReference == null)
            {
                categoryReference = new CategoryReference() { Guid = item.Guid };
                site.Categories.Add(categoryReference);
            }
            categoryReference.Name = item.Name;

            await siteRepository.SaveAsync(site);
        }

        public async Task<Category> SearchAsync(string name)
        {
            var siteRepository = new SiteRepository(this.ConnectionString);
            var site = await siteRepository.GetAsync();

            var categoryReference = site.Categories.Where(x => x.Name == name).FirstOrDefault();

            var item = await this.GetAsync(categoryReference.Guid);

            return item;
        }

        public override async Task<List<Category>> GetListAsync()
        {
            var list = await base.GetListAsync();

            list = list.OrderBy(o => o.Name).ToList();

            return list;
        }
    }
}
