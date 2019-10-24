using CoreCooking.Website.ViewModels.Hashtags;
using CoreCooking.Website.ViewModels.Home;
using CoreCooking.Website.ViewModels.Recipes;
using System;
using System.Net;

namespace CoreCooking.Website.Helpers
{
    /// <summary>
    /// This is to handle the http://Site/Category/Recipe/ concept..
    /// </summary>
    public static class UrlBuilder
    {
        public static string Encode(string str)
        {
            WebUtility.UrlEncode(str);
            return str;
        }


        // Hashtags
        public static string GetUrl(HashtagLineViewModel item)
        {
            return GetUrl(item.Name);
        }
        public static string GetUrl(HashtagDetailsViewModel item, string action = null)
        {
            return GetUrl(item.Name, action);
        }

        public static string GetUrl(HashtagDetailsViewModel item, RecipeLineViewModel recipe)
        {
            return GetUrl(item.Name, recipe.Name);
        }

        // Recipe 
        public static string GetUrl(RecipeViewModel item, string action = null)
        {
            return GetUrl(item.Hashtag, item.Name, action);
        }
        public static string GetUrl(RecipeEditViewModel item, string action = null)
        {
            return GetUrl(item.Hashtag, item.Name, action);
        }

        public static string GetUrl(string hashtag, string recipeName = null, string action = null)
        {
            if (recipeName == null && action == null)
                return String.Format("/{0}", Encode(hashtag));
            else if (action != null)
                return String.Format("/{0}/{1}/{2}", Encode(hashtag), Encode(recipeName), action);
            else
                return String.Format("/{0}/{1}", Encode(hashtag), Encode(recipeName));
        }
    }
}
