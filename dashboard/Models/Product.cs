using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dashboard.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }


        public int  Quantity { get; set; }


    }
}