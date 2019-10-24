using CoreCooking.Parsers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoreCooking.Models.Recipes
{
    public class Recipe : ModelBase
    {
        public Recipe() : base()
        {
            this.Steps = new List<string>();
            this.Ingredients = new List<Ingredient>();
        }

        /// <summary>The unique identifier for the Recipe.</summary>
        public override Guid Guid { get; set; }

        /// <summary>Links the Recipe to a single Category.</summary>
        public Guid CategoryGuid { get; set; }

        /// <summary>The title or brief heading of the Recipe.</summary>
        public string Name { get; set; }

        /// <summary>The tags that define the recipe delimited by # the character.</summary>
        public string HashtagsString { get; set; }

        public int? Serves { get; set; }

        public string ImageUrl { get; set; }

        public string StepsText { get; set; }

        public string IngredientsText { get; set; }

        public string Notes { get; set; }

        [JsonIgnore]
        public List<Ingredient> Ingredients { get; set; }

        [JsonIgnore]
        public List<string> Steps { get; set; }

        /// <summary>Generates the Ingredients and Steps</summary>
        public void ProcessRecipe()
        {
            this.Steps = new List<string>();
            if (!String.IsNullOrEmpty(this.StepsText))
            {
                string[] lines = this.StepsText.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.None);
                this.Steps = new List<string>(lines);
            }

            IngredientParser parser = new IngredientParser();
            this.Ingredients = parser.Parse(this.IngredientsText);
        }


        public List<string> GetHashtags()
        {
            var list = new List<string>();

            var words = this.HashtagsString.Split('#');

            foreach (var word in words)
            {
                if (!String.IsNullOrWhiteSpace(word))
                {
                    list.Add(word.Trim());
                }
            }

            return list;
        }
    }
}
