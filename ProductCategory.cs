using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSEntitiesLib
{
    /// <summary>
    /// ProductCategory class
    /// </summary>
    public class ProductCategory
    {
     /// <summary>
     /// CategoryId
     /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// CategoryName
        /// </summary>
        public String CategoryName { get; set; }
        /// <summary>
        /// List Of Products
        /// </summary>
        public List<Products> Products { get; set; }
    }
}
