using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSEntitiesLib
{
    /// <summary>
    /// CartItems Class
    /// </summary>
    public class CartItems
    {
        /// <summary>
        /// ProductId
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// ProductName
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Image of the product
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Price of the product
        /// </summary>
        public decimal Price { get; set; }
    }
}
