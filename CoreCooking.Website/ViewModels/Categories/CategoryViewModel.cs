using CoreCooking.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCooking.Website.ViewModels.Categories;
using CoreCooking.Website.ViewModels.Recipes;

namespace CoreCooking.Website.ViewModels.Categories
{
    public class CategoryViewModel : ViewModelBase
    {
        public string Name { get; set; }

        public List<RecipeLineViewModel> Lines { get; set; }

        public CategoryViewModel()
        {
            this.Lines = new List<RecipeLineViewModel>();
        }

        public CategoryViewModel(Category item)
        {
            this.Guid = item.Guid;
            this.Name = item.Name;
            this.Lines = new List<RecipeLineViewModel>();

            foreach (var line in item.Recipes)
            {
                var viewModelLine = new RecipeLineViewModel(line);

                this.Lines.Add(viewModelLine);
            }
        }


        public void FillModel(Category item)
        {
            item.Guid = this.Guid;
            item.Name = this.Name;
        }

    }
}
