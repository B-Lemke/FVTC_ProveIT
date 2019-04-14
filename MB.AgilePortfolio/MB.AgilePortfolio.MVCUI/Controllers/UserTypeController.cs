using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class UserTypeController : Controller
    {
        UserType userType;
        UserTypeList userTypes;
        
        // GET: UserType
        public ActionResult Index()
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            userTypes = new UserTypeList();
            userTypes.Load();
            return View(userTypes);
        }

        // GET: UserType/Details/5
        public ActionResult Details(Guid id)
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            userType = new UserType();
            userType.LoadById(id);
            return View(userType);
        }

        // GET: UserType/Create
        public ActionResult Create()
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            userType = new UserType();
            return View(userType);
        }

        // POST: UserType/Create
        [HttpPost]
        public ActionResult Create(UserType ut)
        {
            try
            {
                // TODO: Add insert logic here
                ut.Insert();
                return RedirectToAction("Index");
            }
            catch { return View(ut); }
        }

        // GET: UserType/Edit/5
        public ActionResult Edit(Guid id)
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            userType = new UserType();
            userType.LoadById(id);
            return View(userType);
        }

        // POST: UserType/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, UserType ut)
        {
            try
            {
                // TODO: Add update logic here
                ut.Update();
                return RedirectToAction("Index");
            }
            catch { return View(ut); }
        }

        // GET: UserType/Delete/5
        public ActionResult Delete(Guid id)
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }
            userType = new UserType();
            userType.LoadById(id);
            return View(userType);
        }

        // POST: UserType/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, UserType ut)
        {
            try
            {
                // TODO: Add delete logic here
                ut.Delete();
                return RedirectToAction("Index");
            }
            catch { return View(ut); }
        }
    }
}
