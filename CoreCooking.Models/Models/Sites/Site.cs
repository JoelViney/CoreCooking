using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CoreCooking.Models.Sites
{
    public class Site
    {
        public Site()
        {
            this.Recipes = new List<RecipeIndex>();
        }

        public List<RecipeIndex> Recipes { get; set; }

        public List<RecipeIndex> GetRecipesByHashtag(string hashtag)
        {
            var list = new List<RecipeIndex>();

            foreach (var item in this.Recipes)
            {
                if (item.ContainsHashtag(hashtag))
                {
                    list.Add(item);
                }
            }

            list = list.OrderBy(o => o.Name).ToList();

            return list;
        }

        public List<Hashtag> GetHashtags()
        {
            List<Hashtag> list = new List<Hashtag>();

            foreach (var recipe in this.Recipes)
            {
                var words = recipe.HashtagsString.Split('#');

                foreach (var word in words)
                {
                    if (!String.IsNullOrWhiteSpace(word))
                    {
                        var hashtag = list.Where(x => x.Name == word.Trim()).FirstOrDefault();

                        if (hashtag == null)
                        {
                            hashtag = new Hashtag() { Name = word.Trim() };
                            list.Add(hashtag);
                        }
                        hashtag.Count++;
                    }
                }
            }


            list = list.OrderBy(o => o.Name).ToList();

            return list;
        }
    }
}
