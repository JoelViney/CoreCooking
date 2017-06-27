using CoreCooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCooking.Website.ViewModels.Categories;
using CoreCooking.Website.ViewModels.Recipes;
using CoreCooking.Models.Categories;

namespace CoreCooking.Website.ViewModels.Categories
{
    public class CategoryListViewModel
    {
        public CategoryListViewModel()
        {
            this.Lines = new List<CategoryLineViewModel>();
        }

        public CategoryListViewModel(List<Category> list)
        {
            this.Lines = new List<CategoryLineViewModel>();
            foreach (var item in list)
            {
                var line = new CategoryLineViewModel(item);
                this.Lines.Add(line);
            }
        }

        public List<CategoryLineViewModel> Lines { get; set; }
    }
}
