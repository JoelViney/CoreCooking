using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using CoreCooking.Models.Categories;
using CoreCooking.Data;
using CoreCooking.Models.Recipes;
using System.Linq;
using CoreCooking.Models.Sites;

namespace CoreCooking.Models
{
    [TestClass]
    public class CategoryTests
    {

        #region Helper Methods...

        public static CategoryRepository NewRepository()
        {
            var repository = new CategoryRepository(SettingsFactory.GetConnectionString(), @"test-categories");

            return repository;
        }

        public static async Task<Category> AssureCategoryExistsAsync(CategoryRepository repository = null)
        {
            if (repository == null)
                repository = NewRepository();

            var item = new Category() { Name = "Test" };

            await repository.SaveAsync(item);
            return item;
        }

        #endregion


        [TestMethod]
        public async Task CreateCategoryAsync()
        {
            // Arrange
            var repository = NewRepository();
            var item = new Category() { Name = "Test" };

            // Act
            await repository.SaveAsync(item);

            // Assert
            var item2 = await repository.FindAsync(item.Guid);
            Assert.AreEqual(item.Guid, item2.Guid);
            Assert.AreEqual("Test", item2.Name);
        }


        [TestMethod]
        public async Task UpdateAndGetCategoryAsync()
        {
            // Arrange
            var repository = NewRepository();
            var item = new Category() { Name = "Test" };
            await repository.SaveAsync(item);

            // Act
            var item2 = await repository.GetAsync(item.Guid);
            item2.Name = "Updated Name";
            await repository.SaveAsync(item2);

            // Assert
            var item3 = await repository.GetAsync(item.Guid);
            Assert.AreEqual("Updated Name", item3.Name);
        }

        [TestMethod]
        public async Task DeleteCategoryAsync()
        {
            // Arrange
            var repository = NewRepository();
            var item = await AssureCategoryExistsAsync(repository);

            // Act
            await repository.DeleteAsync(item);

            // Assert
            var item2 = await repository.FindAsync(item.Guid);
            Assert.IsNull(item2);
        }

        [TestMethod]
        public async Task GetCategoryListAsync()
        {
            // Arrange
            var repository = NewRepository();
            Category item = await AssureCategoryExistsAsync(repository);

            // Act
            List<Category> list = await repository.GetListAsync();

            // Assert
            Assert.IsTrue(list.Count > 0);
        }
    }
}
