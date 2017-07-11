using CoreCooking.Models.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCooking.Website.ViewModels.Home
{
    public class HashtagLineViewModel
    {
        public HashtagLineViewModel()
        {

        }

        public HashtagLineViewModel(Hashtag item)
        {
            this.Name = item.Name;
            this.Count = item.Count;
        }

        public HashtagLineViewModel(string hashtag)
        {
            this.Name = hashtag;
        }


        public string Name { get; set; }
        public int Count { get; set; }
    }
}
