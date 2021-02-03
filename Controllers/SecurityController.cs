using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using OSEntitiesLib;

namespace OnlineShopping.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            var returnUrl = Request.QueryString.Get("ReturnUrl");
            ViewData.Add("returnUrl", returnUrl);
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login login)
        {
            OSBusinessLayerLib.OSBusinessLayer bll = new OSBusinessLayerLib.OSBusinessLayer();
            UserCredentials username = bll.GetUserName(login.UserName);
            string uname = username.UserName;
            UserCredentials pwd = bll.GetPassword(login.Password);
            string upwd = pwd.Password;
            if(login.UserName.Equals(uname)&&login.Password.Equals(upwd))
            {
                FormsAuthentication.SetAuthCookie(login.UserName, false);
                var returnUrl = Request.Form["returnUrl"];
                return Redirect(returnUrl);

            }
            else
            {
                ModelState.AddModelError("UserName", "UserName and Password are invalid,Please try again.");
            }
            var returnUrl2 = Request.QueryString.Get("ReturnUrl");
            ViewData.Add("returnUrl", returnUrl2);
            return View();
        }
    }
}