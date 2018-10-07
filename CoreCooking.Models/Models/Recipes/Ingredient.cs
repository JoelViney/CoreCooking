using CoreCooking.Parsers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCooking.Models.Recipes
{
    public class Ingredient
    {
        public decimal? Quantity { get; set; }
        public string Uom { get; set; }
        public string Name { get; set; }
        public string Directions { get; set; }

        #region Helper Methods

        [JsonIgnore]
        public string DisplayQuantityAndUom
        {
            get
            {
                if (this.Quantity == null)
                {
                    return String.Format("{0}", this.Uom);
                }
                else if ((this.Quantity % 1) == 0)
                {   // Whole Number.
                    return String.Format("{0} {1}", this.Quantity, this.Uom);
                }
                else
                {   // Decimal
                    
                    Fraction fraction = FractionParser.RealToFraction(this.Quantity.Value);

                    return String.Format("{0} {1}", fraction.ToString(), this.Uom);
                }
            }

        }
        
        #endregion
    }
}
