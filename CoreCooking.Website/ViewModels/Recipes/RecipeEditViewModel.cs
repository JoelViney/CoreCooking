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
        /// <summary>The parent Hashtag that defines where the user came from to view this Recipe</summary>
        public string Hashtag { get; set; }


        public string ImageUrl { get; set; }

        [Display(Name = "Hashtags")]
        public string HashtagsString { get; set; }

        public string Name { get; set; }

        public string Hashtags { get; set; }

        public int? Serves { get; set; }

        public string IngredientsText { get; set; }

        public string StepsText { get; set; }

        public string Notes { get; set; }

        #region Constructors...

        public RecipeEditViewModel()
        {

        }

        public RecipeEditViewModel(string hashtag, Recipe item)
        {
            this.Hashtag = hashtag;

            this.Guid = item.Guid;
            this.Name = item.Name;
            this.HashtagsString = item.HashtagsString;
            this.Serves = item.Serves;
            this.ImageUrl = item.ImageUrl;

            this.IngredientsText = item.IngredientsText;
            this.StepsText = item.StepsText;

            this.Notes = item.Notes;
        }

        #endregion

        public void FillModel(Recipe item)
        {
            item.Guid = this.Guid;
            item.Name = this.Name.Trim();
            item.HashtagsString = this.HashtagsString;
            item.Serves = this.Serves;
            item.ImageUrl = this.ImageUrl;

            item.IngredientsText = this.IngredientsText;
            item.StepsText = this.StepsText;

            item.Notes = this.Notes;
        }
    }
}
