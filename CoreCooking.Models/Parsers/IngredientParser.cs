using System;
using System.Collections.Generic;
using System.Text;
using CoreCooking.Models.Recipes;
using System.Linq;

namespace CoreCooking.Parsers
{

    public class IngredientParser
    {
        public static readonly Uom[] UOMS =
        {
            new Uom("lug", "lug"),
            new Uom("swig", "swig"),
            new Uom("cup", "cups"),
            new Uom("cm", "cm", "centimeter", "centimeters"),
            new Uom("ml", "ml"),
            new Uom("g", "g", "grams", "gram"),
            new Uom("kg", "kgs"),
            new Uom("ltr", "ltrs", "liter", "liters"),
            new Uom("clove", "cloves"),
            new Uom("bunch", "bunches"),
            new Uom("part", "parts"),
            new Uom("tin", "tins"),
            new Uom("tsp", "tsp", "teaspoon", "teaspoons", "tea spoon", "tea spoons"),
            new Uom("tblsp", "tblsp", "tablespoon", "tablespoons", "table spoon", "table spoons", "tbl spoon", "tbl spoons", "tble spoon", "tble spoons", "tbsp", "tbsps"),
        };

        public List<Ingredient> Parse(string text)
        {
            List<Ingredient> list = new List<Ingredient>();

            if (!String.IsNullOrEmpty(text))
            {
                string[] lines = text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.None);

                foreach (string line in lines)
                {
                    Ingredient item = ParseLine(line);

                    list.Add(item);
                }
            }

            return list;
        }

        public Ingredient ParseLine(string line)
        {
            Ingredient item = new Ingredient();
            
            int i;

            {   // Parse the Quantity
                char[] list = line.ToCharArray();

                string quantityString = "";
                for (i = 0; i <= line.Length - 1; i++)
                {
                    char ch = line[i];
                    if ((ch >= 48 && ch <= 57) || ch == '/' || ch == ' ' || ch == '.')
                        quantityString += ch;
                    else
                        break;
                }
                quantityString = quantityString.Trim();
                item.Quantity = FractionToDouble(quantityString);

                line = line.Substring(i);
            }


            { // Parse the UOM

                foreach (var uom in UOMS)
                {
                    string matchedPortion;
                    if (uom.TryMatch(line, out matchedPortion))
                    {
                        if (item.Quantity != null && item.Quantity > 1)
                            item.Uom = uom.Plural;
                        else
                            item.Uom = uom.Singular;

                        line = line.Substring(matchedPortion.Length).Trim();
                        break;
                    }
                }
            }


            {   // Parse any Fillwords
                if (line.StartsWith("of "))
                    line = line.Substring("of ".Length);
            }

            if (line.IndexOf('-') >= 0)
            {
                int index = line.IndexOf('-');
                item.Name = line.Substring(0, index).Trim();

                item.Directions = line.Substring(index + 1).Trim();
            }
            else
            {
                item.Name = line;
            }
            
            return item;
        }

        private decimal? FractionToDouble(string fraction)
        {
            if (fraction == "")
                return null;

            decimal result;

            if (Decimal.TryParse(fraction, out result))
            {
                return result;
            }

            string[] split = fraction.Split(new char[] { ' ', '/' });

            if (split.Length == 2 || split.Length == 3)
            {
                int a, b;

                if (int.TryParse(split[0], out a) && int.TryParse(split[1], out b))
                {
                    if (split.Length == 2)
                    {
                        return (decimal)a / b;
                    }

                    int c;

                    if (int.TryParse(split[2], out c))
                    {
                        return a + (decimal)b / c;
                    }
                }
            }

            throw new FormatException("Not a valid fraction.");
        }
    }
}
