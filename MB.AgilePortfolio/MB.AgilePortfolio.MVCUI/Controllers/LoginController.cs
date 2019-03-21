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
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Contents.Abandon();
            Session.Contents.RemoveAll();
            return View();
        }

        public ActionResult Login()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(User user, string returnurl)
        {
            try
            {
                if (user.Login())
                {
                    ViewBag.Message = "Login successful";
                    ViewBag.FullName = user.FullName;
                    Session["user"] = user;
                    //Set UserType on login in a session
                    Session["UserType"] = user.UserTypeDescription;

                    // If no return url supplied, use referrer url.
                    // Protect against endless loop by checking for empty referrer.
                    if (String.IsNullOrEmpty(returnurl)
                        && Request.UrlReferrer != null
                        && Request.UrlReferrer.ToString().Length > 0)
                    {
                        return RedirectToAction("Index", "UserProfile");
                    }

                    return Redirect(returnurl);
                }
                else
                {
                    ViewBag.Message = "Wrong credentials...";
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(user);
            }
        }
    }
}
