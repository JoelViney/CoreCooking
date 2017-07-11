using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCooking.Models.Sites
{
    public class RecipeIndex
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string HashtagsString { get; set; }
        public string ImageUrl { get; set; }

        public bool ContainsHashtag(string hashtag)
        {
            var list = this.HashtagsString.Split('#');

            foreach (var item in list)
            {
                if (item.Trim() == hashtag)
                    return true;
            }

            return false;
        }
    }
}
