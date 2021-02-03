using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using OSEntitiesLib;
using OSBusinessLayerLib;
using Newtonsoft.Json;
//using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class OnlineShoppingMController : Controller
    {
        // GET: OnlineShoppingM
        public ActionResult Index()
        {
            //localhost connection of API
            Uri uri = new Uri("http://localhost:51563/api/");
            //call API for GET
            using (var client = new HttpClient())
            {
                //sets the base address defines the uri path
                client.BaseAddress = uri;
                var result = client.GetStringAsync("OnlineShopping/GetAllCategories/").Result;
                //to get list of categories
                var lstc = JsonConvert.DeserializeObject<List<ProductCategory>>(result);
                //returns the list of productcategory to "GetAllCategories" view to display
                return View(lstc);
            }
        }
        
        public ActionResult GetProductsByName(string productname)
        {
            //localhost connection of API
            Uri uri = new Uri("http://localhost:51563/api/");
            //call API for Get
            using (var client = new HttpClient())
            {
                //sets the base address defines the uri path
                client.BaseAddress = uri;
                var result = client.GetStringAsync("OnlineShopping/GetProductsByName/" + productname).Result;
                //gets the list of products on searching product name
                var lstpro = JsonConvert.DeserializeObject<List<Products>>(result);
                //returns the list of products to "GetProductByName" view to display
                return View(lstpro);
            }
        }

        public ActionResult GetProductsByCategoryName(string cname)
        {
            //localhost connection of API
            Uri uri = new Uri("http://localhost:51563/api/");
            //call API for Get
            using (var client = new HttpClient())
            {
                //sets the base address defines the uri path
                client.BaseAddress = uri;
                var result = client.GetStringAsync("OnlineShopping/GetProductsByCategoryName/" + cname).Result;
                //gets the list of products on clicking the category name
                var lstpro = JsonConvert.DeserializeObject<List<Products>>(result);
                //returns the list of products to "GetProductsByCategoryName" view to display
                return View("GetProductsByCategoryName", lstpro);
            }
        }
        [HttpGet]
        public ActionResult GetProductDetailsById(int id)
        {
            //localhost connection of API
            Uri uri = new Uri("http://localhost:51563/api/");
            //call API for Get
            using (var client = new HttpClient())
            {
                //sets the base address defines the uri path
                client.BaseAddress = uri;
                var result = client.GetStringAsync("OnlineShopping/GetProductDetailsById/" + id).Result;
                //gets the details of product
                var pro = JsonConvert.DeserializeObject<Products>(result);
                //returns the products to "GetProductDetailsById" to display
                return View(pro);

            }
        }
       
        //[HttpGet]
        //public ActionResult AddToCart()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AddToCart(CartItems item)
        //{
        //    if (Session["cart"] == null)
        //    {
        //        List<CartItems> items = new List<CartItems>();
        //        items.Add(item);
        //        Session.Add("cart", items);
        //    }
        //    else
        //    {
        //        var cartList = (List<CartItems>)Session["cart"];
        //        cartList.Add(item);
        //        Session["cart"] = cartList;
        //    }
        //    // return View();
        //    return RedirectToAction("DisplayCart", new { id = item.ProductId });
        //}
        //public ActionResult DisplayCart()
        //{
        //    Uri uri = new Uri("http://localhost:51563/api/");
        //    var cartList = (List<CartItems>)Session["cart"];
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = uri;
        //        var result = client.PostAsJsonAsync<List<CartItems>>("OnlineShopping/", cartList).Result;
        //        if (result.IsSuccessStatusCode == true)
        //        {
        //            ViewData.Add("msg", "List are posted");
        //        }
        //        else
        //        {
        //            ViewData.Add("msg", "Something went wrong");
        //        }

        //    }
        //    return View(cartList);

        //}
        //[HttpGet]
        //[Route("api/OnlineShopping/DeletefromCart")]
        //public ActionResult DeletefromCart(int id)
        //{
        //    var cartList = (List<CartItems>)Session["cart"];
        //    var item = cartList.Where(o => o.ProductId == id).FirstOrDefault();
        //    cartList.Remove(item);
        //    Session["cart"] = cartList;
        //    return RedirectToAction("Index");
        //}


        //this action used to add the products in cart 
        public ActionResult AddToCarts(int id,string name)
        {
            Session["cart_values"] = Session["cart_values"] + id.ToString() + ",";
            return RedirectToAction("Index");
        }
        /// <summary>
        /// This method displays the products which are in cart list
        /// </summary>
        /// <param name="list">it is used to pass the cartlist whose record is to be selected</param>
        /// <returns>products are found</returns>
        public ActionResult ViewCart(string list)
        {
            var items = Session["cart_values"];
            if(items == null)
            {
                ViewBag.Message = "No products to display";
                items = "0";
            }
            else { ViewBag.Message = ""; }
            OSBusinessLayer obj = new OSBusinessLayer();
            var lstproduct = obj.ViewCart(items.ToString());
            return View(lstproduct);                                  
        }
        //This action is used to delete the items in cart
        public ActionResult DeleteFromCart(int id)
        {
            string items = Session["cart_values"].ToString();
            string[] selections=items.Split(new char[]{ ','});
            var list = new List<string>(selections);
            list.Remove(id.ToString());
            list.RemoveAll(s => string.IsNullOrWhiteSpace(s));
            Session["cart_values"] = "";
            for(int i =0;i<list.Count;i++)
            {
                Session["cart_values"] = Session["cart_values"] + list[i] + ",";
            }
            return RedirectToAction("ViewCart");            
        }
        //this action is used to order the product
        public ActionResult OrderNow()
        {
            return View();
        }



    }
}
        
    

