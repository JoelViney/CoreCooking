using CoreCooking.Models.Sites;
using System;

namespace CoreCooking.Website.ViewModels.Recipes
{
    public class RecipeLineViewModel
    {
        public RecipeLineViewModel(RecipeIndex item)
        {
            this.Guid = item.Guid;
            this.Name = item.Name;
            this.ImageUrl = item.ImageUrl;
        }

        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
