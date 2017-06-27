using CoreCooking.Models;
using CoreCooking.Models.Categories;
using CoreCooking.Models.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCooking.Website.ViewModels.Recipes
{
    public class RecipeLineViewModel
    {
        public RecipeLineViewModel(RecipeReference item)
        {
            this.Guid = item.Guid;
            this.Name = item.Name;
        }

        public Guid Guid { get; set; }
        public string Name { get; set; }
    }
}
