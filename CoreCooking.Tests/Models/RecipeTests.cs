using CoreCooking.Data;
using CoreCooking.Models.Categories;
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

        public static async Task<Recipe> AssureRecipeExistsAsync(RecipeRepository repository, Category category = null)
        {
            if (category == null)
                category = await CategoryTests.AssureCategoryExistsAsync();

            var item = new Recipe() { CategoryGuid = category.Guid, Name = "Test" };

            await repository.SaveAsync(item);

            return item;
        }

        #endregion

        [TestMethod]
        public async Task CreateRecipeAsync()
        {
            // Arrange
            var repository = NewRepository();
            Category category = await CategoryTests.AssureCategoryExistsAsync();
            var item = new Recipe() {  CategoryGuid = category.Guid, Name = "Test Recipe", IngredientsText =  "1 cup Test - Lightly chopped", StepsText = "Do Test" };

            // Act
            await repository.SaveAsync(item);

            // Assert
            var item2 = await repository.GetAsync(item.Guid);

            Assert.IsNotNull(item2);
            Assert.AreEqual(category.Guid, item2.CategoryGuid);
            Assert.AreEqual("Test Recipe", item2.Name);
            Assert.AreEqual("1 cup Test - Lightly chopped", item2.IngredientsText);
            Assert.AreEqual("Do Test", item2.StepsText);
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
