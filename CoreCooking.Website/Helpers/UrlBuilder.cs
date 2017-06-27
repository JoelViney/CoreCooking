using CoreCooking.Website.ViewModels.Categories;
using CoreCooking.Website.ViewModels.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

        // Category 
        public static string GetUrl(CategoryViewModel item, string action = null)
        {
            if (action != null)
                return String.Format("/{0}/{1}", Encode(item.Name), action);
            else
                return String.Format("/{0}", Encode(item.Name));
        }

        // Category 
        public static string GetUrl(CategoryLineViewModel item)
        {
            return String.Format("/{0}", Encode(item.Name));
        }

        // Category
        public static string GetCategoryUrl(RecipeViewModel item)
        {
            return String.Format("/{0}", Encode(item.CategoryName));
        }

        // Category
        public static string GetCategoryUrl(RecipeEditViewModel item)
        {
            return String.Format("/{0}", Encode(item.CategoryName));
        }

        // Recipe 
        public static string GetUrl(CategoryViewModel category, RecipeLineViewModel item)
        {
            return String.Format("/{0}/{1}", Encode(category.Name), Encode(item.Name));
        }

        // Recipe 
        public static string GetUrl(RecipeEditViewModel item, string action = null)
        {
            if (action != null)
                return String.Format("/{0}/{1}/{2}", Encode(item.CategoryName), Encode(item.Name), action);
            else
                return String.Format("/{0}/{1}", Encode(item.CategoryName), Encode(item.Name));
        }

        // Recipe 
        public static string GetUrl(RecipeViewModel item, string action = null)
        {
            if (action != null)
                return String.Format("/{0}/{1}/{2}", Encode(item.CategoryName), Encode(item.Name), action);
            else
                return String.Format("/{0}/{1}", Encode(item.CategoryName), Encode(item.Name));
        }
    }
}
