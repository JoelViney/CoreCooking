using CoreCooking.Data;
using CoreCooking.Models.Recipes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCooking.Models
{
    [TestClass]
    public class RecipeTests
    {
        #region Helper Methods...

        public static RecipeRepository NewRepository()
        {
            var repository = new RecipeRepository(SettingsFactory.GetConnectionString(), "test-recipes");

            return repository;
        }

        public static async Task<Recipe> AssureRecipeExistsAsync(RecipeRepository repository)
        {
            var item = new Recipe() { Name = "Test" };

            await repository.SaveAsync(item);

            return item;
        }

        #endregion

        [TestMethod]
        public async Task CreateRecipeAsync()
        {
            // Arrange
            var repository = NewRepository();
            var item = await AssureRecipeExistsAsync(repository);

            // Act
            item.Name = "Updated Name";
            await repository.SaveAsync(item);

            // Assert
            var item2 = await repository.GetAsync(item.Guid);

            Assert.IsNotNull(item2);
            Assert.AreEqual("Updated Name", item2.Name);
        }

        [TestMethod]
        public async Task UpdateRecipeAsync()
        {
            // Arrange
            var repository = NewRepository();
            var item = await AssureRecipeExistsAsync(repository);

            // Act
            item.Name = "Updated Name";
            await repository.SaveAsync(item);

            // Assert
            var item2 = await repository.GetAsync(item.Guid);

            Assert.IsNotNull(item2);
            Assert.AreEqual("Updated Name", item2.Name);
        }

        [TestMethod]
        public async Task DeleteRecipeAsync()
        {
            // Arrange
            var repository = NewRepository();
            var item = await AssureRecipeExistsAsync(repository);

            // Act
            await repository.DeleteAsync(item);

            // Assert
            var item2 = await repository.FindAsync(item.CategoryGuid);
            Assert.IsNull(item2);
        }
    }
}
