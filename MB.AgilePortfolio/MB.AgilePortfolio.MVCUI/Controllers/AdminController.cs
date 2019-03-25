using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;
using MB.AgilePortfolio.MVCUI.Models;
using MB.AgilePortfolio.MVCUI.ViewModels;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated())
            {
                User userin = System.Web.HttpContext.Current.Session["user"] as User;

                if (userin.UserTypeDescription == "Admin")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}