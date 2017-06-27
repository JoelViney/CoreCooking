using CoreCooking.Models.Recipes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCooking.Models.Categories
{
    public class Category : ModelBase
    {
        public Category()
        {
            this.Recipes = new List<RecipeReference>();
        }

        public override Guid Guid { get; set; }

        public string Name { get; set; }

        public List<RecipeReference> Recipes { get; set; }
    }
}
