using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreCooking.Parsers
{
    public class Uom
    {
        public string Singular { get; set; }
        public string Plural { get; set; }

        public string[] Aliases { get; set; }

        public Uom(string singular, string plural)
        {
            this.Singular = singular;
            this.Plural = plural;

            // Add the singular and plural to the list
            List<string> list = new List<string>();
            list.Add(this.Singular);
            list.Add(this.Plural);
            list = list.OrderByDescending(x => x.Length).ToList();
            this.Aliases = list.ToArray();
        }

        public Uom(string singular, string plural, params string[] aliases)
        {
            this.Singular = singular;
            this.Plural = plural;
            // Add the singular and plural to the list
            List<string> list = new List<string>(aliases);
            list.Add(this.Singular);
            list.Add(this.Plural);
            list = list.OrderByDescending(x => x.Length).ToList();
            this.Aliases = list.ToArray();
        }

        public bool TryMatch(string line, out string matchedPortion)
        {
            string lineLower = line.ToLower();

            foreach (string str in this.Aliases)
            {
                if (lineLower.StartsWith(str + " "))
                {
                    matchedPortion = str;
                    return true;
                }
            }

            matchedPortion = null;
            return false;
        }
    }
}
