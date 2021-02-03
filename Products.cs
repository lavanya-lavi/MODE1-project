using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSEntitiesLib
{
    /// <summary>
    /// Products Class
    /// </summary>
    public class Products
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Category Id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Product Name
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Picture Of Product
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// Price Of Product
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Content of Product
        /// </summary>
        public string Content { get; set; }
        public string DisplayContent()
        {
            string data = "1";
            string[] cols = Content.Split(',');
            foreach(var col in cols)
            {
                string colName, colValue;
                string[] colNameValue = col.Split(':');
                colName = colNameValue[0];
                colValue = colNameValue[1];
                data += colName + ":" + colValue;
            }
            return data;
        }

    }
}
