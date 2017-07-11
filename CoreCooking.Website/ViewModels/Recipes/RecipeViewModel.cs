using CoreCooking.Models;
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

        public RecipeViewModel(string hashtag, Recipe item)
        {
            this.Hashtag = hashtag;

            this.Guid = item.Guid;
            this.Name = item.Name;
            this.Hashtags = item.GetHashtags();
            this.Serves = item.Serves;
            this.ImageUrl = item.ImageUrl;
            
            this.Notes = item.Notes;
            this.Ingredients = new List<IngredientViewModel>();
            foreach (var line in item.Ingredients)
            {
                var viewModelLine = new IngredientViewModel(line);
                Ingredients.Add(viewModelLine);
            }

            this.Steps = item.Steps;
        }

        /// <summary>The parent Hashtag that defines where the user came from to view this Recipe</summary>
        public string Hashtag { get; set; }

        public string Name { get; set; }

        [Display(Name = "Hashtags")]
        public List<string> Hashtags { get; set; }

        public int? Serves { get; set; }

        public string ImageUrl { get; set; }

        public string Notes { get; set; }

        public List<IngredientViewModel> Ingredients { get; set; }

        public List<string> Steps { get; set; }
    }
}
