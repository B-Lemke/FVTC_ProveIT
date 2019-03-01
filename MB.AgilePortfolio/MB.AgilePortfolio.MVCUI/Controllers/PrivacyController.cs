using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class PrivacyController : Controller
    {
        Privacy privacy;
        PrivacyList privacies;
        
        // GET: Privacy
        public ActionResult Index()
        {
            privacies = new PrivacyList();
            privacies.Load();
            return View(privacies);
        }

        // GET: Privacy/Details/5
        public ActionResult Details(Guid id)
        {
            privacy = new Privacy();
            privacy.LoadById(id);
            return View(privacy);
        }

        // GET: Privacy/Create
        public ActionResult Create()
        {
            privacy = new Privacy();
            return View(privacy);
        }

        // POST: Privacy/Create
        [HttpPost]
        public ActionResult Create(Privacy p)
        {
            try
            {
                // TODO: Add insert logic here
                p.Insert();
                return RedirectToAction("Index");
            }
            catch { return View(p); }
        }

        // GET: Privacy/Edit/5
        public ActionResult Edit(Guid id)
        {
            privacy = new Privacy();
            privacy.LoadById(id);
            return View(privacy);
        }

        // POST: Privacy/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Privacy p)
        {
            try
            {
                // TODO: Add update logic here
                p.Update();
                return RedirectToAction("Index");
            }
            catch { return View(p); }
        }

        // GET: Privacy/Delete/5
        public ActionResult Delete(Guid id)
        {
            privacy = new Privacy();
            privacy.LoadById(id);
            return View(privacy);
        }

        // POST: Privacy/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Privacy p)
        {
            try
            {
                // TODO: Add delete logic here
                p.Delete();
                return RedirectToAction("Index");
            }
            catch { return View(p); }
        }
    }
}
