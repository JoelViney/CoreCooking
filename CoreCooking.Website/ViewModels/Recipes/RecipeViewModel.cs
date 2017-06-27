using CoreCooking.Models;
using CoreCooking.Models.Categories;
using CoreCooking.Models.Recipes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCooking.Website.ViewModels.Recipes
{
    public class RecipeViewModel : ViewModelBase
    {
        public RecipeViewModel()
        {

        }

        public RecipeViewModel(Category category, Recipe item)
        {
            this.Guid = item.Guid;
            this.CategoryGuid = item.CategoryGuid;
            this.Name = item.Name;
            this.ImageUrl = item.ImageUrl;

            this.Notes = item.Notes;
            this.Ingredients = new List<IngredientViewModel>();
            foreach (var line in item.Ingredients)
            {
                var viewModelLine = new IngredientViewModel(line);
                Ingredients.Add(viewModelLine);
            }

            this.Steps = item.Steps;

            this.CategoryName = category.Name;
        }

        public Guid CategoryGuid { get; set; }
        public string CategoryName { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Notes { get; set; }

        public List<IngredientViewModel> Ingredients { get; set; }
        public List<string> Steps { get; set; }
    }
}
