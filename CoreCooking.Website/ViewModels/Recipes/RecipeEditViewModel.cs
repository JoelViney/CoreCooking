using CoreCooking.Models.Categories;
using CoreCooking.Models.Recipes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCooking.Website.ViewModels.Recipes
{
    public class RecipeEditViewModel : ViewModelBase
    {
        public string ImageUrl { get; set; }

        [DisplayName("Category")]
        public Guid CategoryGuid { get; set; }

        public string CategoryName { get; set; }

        public string Name { get; set; }

        public int? Serves { get; set; }

        public string IngredientsText { get; set; }

        public string StepsText { get; set; }

        public string Notes { get; set; }

        #region Constructors...

        public RecipeEditViewModel()
        {

        }

        public RecipeEditViewModel(Category category, Recipe item)
        {
            this.Guid = item.Guid;
            this.Name = item.Name;
            this.Serves = item.Serves;
            this.ImageUrl = item.ImageUrl;

            this.IngredientsText = item.IngredientsText;
            this.StepsText = item.StepsText;

            this.Notes = item.Notes;

            this.CategoryGuid = category.Guid;
            this.CategoryName = category.Name;
        }

        #endregion

        public void FillModel(Recipe item)
        {
            item.Guid = this.Guid;
            item.CategoryGuid = this.CategoryGuid;
            item.Name = this.Name.Trim();
            item.Serves = this.Serves;
            item.ImageUrl = this.ImageUrl;

            item.IngredientsText = this.IngredientsText;
            item.StepsText = this.StepsText;

            item.Notes = this.Notes;
        }
    }
}
