using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCooking.Models.Sites
{
    public class Site
    {
        public Site()
        {
            this.Categories = new List<CategoryReference>();
        }

        public List<CategoryReference> Categories { get; set; }
    }
}
