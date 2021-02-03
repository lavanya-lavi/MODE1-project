using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopping.Models
{
    public class ProductCategory
    {
        public int CategoryId { get; set; }
        public String CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}