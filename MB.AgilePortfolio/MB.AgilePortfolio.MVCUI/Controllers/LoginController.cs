using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return View();
        }

        public ActionResult Login(string returnurl)
        {
            // Specify the home page
            if (returnurl == null)
                returnurl = HttpContext.Request.UrlReferrer.AbsoluteUri;
            User user = new User();
            ViewBag.ReturnUrl = returnurl;
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(User user, string returnurl)
        {
            try
            {
                ViewBag.ReturnUrl = returnurl;
                if (user.Login())
                {
                    ViewBag.Message = "Login successful";
                    Session["user"] = user;
                    return Redirect(returnurl);
                }
                else
                {
                    ViewBag.Message = "Wrong credentials...";

                    // Go back to the login screen again
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;

                // Go back to the login screen again
                return View(user);
            }
        }
    }
}
