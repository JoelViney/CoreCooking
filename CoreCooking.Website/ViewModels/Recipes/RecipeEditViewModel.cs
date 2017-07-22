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

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Hashtags { get; set; }

        public int? Serves { get; set; }

        [MaxLength(2048)]
        public string IngredientsText { get; set; }

        [MaxLength(2048)]
        public string StepsText { get; set; }

        [MaxLength(1024)]
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
