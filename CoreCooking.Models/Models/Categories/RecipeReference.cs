using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCooking.Models.Categories
{
    public class RecipeReference
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
