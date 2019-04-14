using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class LanguageController : Controller
    {
        Language language;
        LanguageList languages;

        // GET: Language
        public ActionResult Index()
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            languages = new LanguageList();
            languages.Load();
            return View(languages);
        }

        // GET: Language/Details/5
        public ActionResult Details(Guid id)
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            language = new Language();
            language.LoadById(id);
            return View(language);
        }

        // GET: Language/Create
        public ActionResult Create()
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            language = new Language();
            return View(language);
        }

        // POST: Language/Create
        [HttpPost]
        public ActionResult Create(Language l)
        {
            try
            {
                // TODO: Add insert logic here
                l.Insert();
                return RedirectToAction("Index");
            }
            catch { return View(l); }
        }

        // GET: Language/Edit/5
        public ActionResult Edit(Guid id)
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            language = new Language();
            language.LoadById(id);
            return View(language);
        }

        // POST: Language/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Language l)
        {
            try
            {
                // TODO: Add update logic here
                l.Update();
                return RedirectToAction("Index");
            }
            catch { return View(l); }
        }

        // GET: Language/Delete/5
        public ActionResult Delete(Guid id)
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            language = new Language();
            language.LoadById(id);
            return View(language);
        }

        // POST: Language/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Language l)
        {
            try
            {
                // TODO: Add delete logic here
                l.Delete();
                return RedirectToAction("Index");
            }
            catch { return View(l); }
        }



    }
}
