using CoreCooking.Models.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCooking.Website.ViewModels.Recipes
{
    public class IngredientViewModel
    {
        public decimal? Quantity { get; set; }
        public string Uom { get; set; }
        public string Name { get; set; }
        public string Directions { get; set; }

        public string DisplayQuantityAndUom;

        public IngredientViewModel()
        {

        }

        public IngredientViewModel(Ingredient item)
        {
            this.Quantity = item.Quantity;
            this.Uom = item.Uom;
            this.Name = item.Name;
            this.Directions = item.Directions;
            this.DisplayQuantityAndUom = item.DisplayQuantityAndUom;
        }
    }
}
