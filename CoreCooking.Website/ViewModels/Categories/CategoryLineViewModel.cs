using CoreCooking.Models;
using CoreCooking.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCooking.Website.ViewModels.Categories
{
    public class CategoryLineViewModel
    {
        public CategoryLineViewModel(Category item)
        {
            this.Guid = item.Guid;
            this.Name = item.Name;
        }

        public Guid Guid { get; set; }

        public string Name { get; set; }
    }
}
