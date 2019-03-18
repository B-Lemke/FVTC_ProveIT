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
                    return RedirectToAction("Index", "Admin", new { returnurl = HttpContext.Request.Url });
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
