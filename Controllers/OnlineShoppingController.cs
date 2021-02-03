using OSEntitiesLib;
using OSBusinessLayerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace OnlineShoppingWebApi.Controllers
{
    public class OnlineShoppingController : ApiController
    {
        /// <summary>
        /// This method retrieve the products record from database based on product name
        /// </summary>
        /// <param name="productname"> It is used to pass the product name whose record is to be selected</param>
        /// <returns> product records found based on productname passed</returns>
        [Route("api/OnlineShopping/GetProductsByName/{productname}")]
        //To get the products based on search by product name 
        public List<Products> GetProductsByName(string productname)
        {
            OSBusinessLayer bll = new OSBusinessLayer();
            var lstpro = bll.GetProductsByName(productname);
            return lstpro;
        }
        /// <summary>
        /// This method retrieve the product records from database based on category name
        /// </summary>
        /// <param name="cname">It is used to pass the category name whose record is to be selected </param>
        /// <returns> product records found based on category name passed</returns>
        [Route("api/OnlineShopping/GetProductsByCategoryName/{cname}")]
        //To get all the products in category based on Category name
        public List<Products> GetProductsByCategoryName(string cname)
        {
            OSBusinessLayer bll = new OSBusinessLayer();
            var lstc = bll.GetProductsByCategoryName(cname);
            return lstc;
        }
        /// <summary>
        /// This method retrieve the product record from database based on product id
        /// </summary>
        /// <param name="id"> It is used to pass the product id whose record is to be selected  </param>
        /// <returns> product record found based on product id passed</returns>
        [Route("api/OnlineShopping/GetProductDetailsById/{id}")]
        //To get all the details of the product based on productid
        public Products GetProductDetailsById(int id)
        {
            OSBusinessLayer bll = new OSBusinessLayer();
            var pro = bll.GetProductDetailsById(id);
            return pro;
        }
        /// <summary>
        /// This method retrieve all the productcategory records from database
        /// </summary>
        /// <returns>productcategory record from database</returns>
        [Route("api/Onlineshopping/GetAllCategories")]
        //to get all the categories from the database
        public List<ProductCategory> GetAllCategories()
        {
            OSBusinessLayer bll = new OSBusinessLayer();
            var lstc = bll.GetAllCategories();
            return lstc;
        }
        /// <summary>
        /// this method retrieves all the items in cart from database
        /// </summary>
        /// <returns>cart items record from data base</returns>

        [Route("api/OnlineShopping/Get")]
        //to show all the products from the cartlist
        public List<CartItems> GetCartItems()
        {
            OSBusinessLayer bll = new OSBusinessLayer();
            var lstcart = bll.GetCartItems();
            return lstcart;
        }
        /// <summary>
        /// This method to add items to cart
        /// </summary>
        /// <param name="lst">list as a parameter</param>
        //to post the product detail in the cartitem
        public HttpResponseMessage Post([FromBody] List<CartItems> lst)
        {
            HttpResponseMessage errRes = Request.CreateErrorResponse(HttpStatusCode.OK, "Record posted");
            try
            {
                OSBusinessLayer bll = new OSBusinessLayer();
                bll.AddToCart(lst);
            }
            catch (Exception ex)
            {
                errRes = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record could not posted");
            }
            return errRes;

        }
        /// <summary>
        /// this method is used to delete the product from cart based on productid
        /// </summary>
        /// <param name="id"> productid as parameter</param>
        [Route("api/OnlineShopping/DeleteFromCart")]
        //to delete the product from the cart based on product id
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage errRes = Request.CreateErrorResponse(HttpStatusCode.OK, "Record deleted");
            try
            {
                OSBusinessLayer bll = new OSBusinessLayer();
                bll.DeleteFromCartById(id);

            }
            catch (Exception ex)
            {
                errRes = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product id not found,could not delete");
            }
            return errRes;


        }
    }
}
