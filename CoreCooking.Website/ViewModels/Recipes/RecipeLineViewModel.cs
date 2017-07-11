using CoreCooking.Models;
using CoreCooking.Models.Recipes;
using CoreCooking.Models.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
