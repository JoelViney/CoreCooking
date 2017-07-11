using CoreCooking.Models.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCooking.Website.ViewModels.Home
{
    public class HomeViewModel
    {
        public List<HashtagLineViewModel> Hashtags { get; set; }

        public HomeViewModel()
        {
            this.Hashtags = new List<HashtagLineViewModel>();
        }

        public HomeViewModel(Site site)
        {
            this.Hashtags = new List<HashtagLineViewModel>();
            var list = site.GetHashtags();

            foreach (var item in list)
            {
                var viewModel = new HashtagLineViewModel(item);
                this.Hashtags.Add(viewModel);
            }
        }
    }
}
