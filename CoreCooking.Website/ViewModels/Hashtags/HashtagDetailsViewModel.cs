using CoreCooking.Models.Sites;
using CoreCooking.Website.ViewModels.Recipes;
using System.Collections.Generic;

namespace CoreCooking.Website.ViewModels.Hashtags
{
    public class HashtagDetailsViewModel
    {
        public string Name { get; set; }

        public List<RecipeLineViewModel> Lines { get; set; }

        public HashtagDetailsViewModel()
        {
            this.Lines = new List<RecipeLineViewModel>();
        }

        public HashtagDetailsViewModel(string name, List<RecipeIndex> list)
        {
            this.Name = name;
            this.Lines = new List<RecipeLineViewModel>();

            foreach (var line in list)
            {
                var viewModelLine = new RecipeLineViewModel(line);

                this.Lines.Add(viewModelLine);
            }
        }
    }
}
