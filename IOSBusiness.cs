using OSEntitiesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSBusinessLayerLib
{
    public interface IOSBusiness
    {
        /// <summary>
        /// This method retrieve the products record from database based on product name
        /// </summary>
        /// <param name="productname"> It is used to pass the product name whose record is to be selected</param>
        /// <returns> product records found based on productname passed</returns>
        List<Products> GetProductsByName(string productname);
        /// <summary>
        /// This method retrieve the product records from database based on category name
        /// </summary>
        /// <param name="cname">It is used to pass the category name whose record is to be selected </param>
        /// <returns> product records found based on category name passed</returns>
        List<Products> GetProductsByCategoryName(string cname);
        /// <summary>
        /// This method retrieve the product record from database based on product id
        /// </summary>
        /// <param name="id"> It is used to pass the product id whose record is to be selected  </param>
        /// <returns> product record found based on product id passed</returns>
        Products GetProductDetailsById(int id);
        /// <summary>
        /// This method retrieve all the productcategory records from database
        /// </summary>
        /// <returns>productcategory record from database</returns>
        List<ProductCategory> GetAllCategories();
        /// <summary>
        /// This Method retrieve the usercredentials record from database based on username
        /// </summary>
        /// <param name="name"> It is used to pass the username whose record is to be selected</param>
        /// <returns> usercredentials record found based on username passed</returns>
        UserCredentials GetUserName(string name);
        /// <summary>
        /// This method retrieve the usercredentials record from database based on password
        /// </summary>
        /// <param name="name"> It is used to pass the password whose record is to be selected</param>
        /// <returns> usercredentials record found based on password passed</returns>
        UserCredentials GetPassword(string name);
        /// <summary>
        /// this method retrieves all the items in cart from database
        /// </summary>
        /// <returns>cart items record from data base</returns>
        List<CartItems> GetCartItems();
        /// <summary>
        /// This method to add items to cart
        /// </summary>
        /// <param name="lst">list as a parameter</param>
        void AddToCart(List<CartItems> lst);
        /// <summary>
        /// This method displays the products which are in cart list
        /// </summary>
        /// <param name="list">it is used to pass the cartlist whose record is to be selected</param>
        /// <returns>products are found</returns>
        List<Products> ViewCart(string list);
        /// <summary>
        /// this method is used to delete the product from cart based on productid
        /// </summary>
        /// <param name="id"> productid as parameter</param>
        void DeleteFromCartById(int id);



    }
}
