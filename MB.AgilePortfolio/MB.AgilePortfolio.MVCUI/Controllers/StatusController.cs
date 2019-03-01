using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class StatusController : Controller
    {
        Status status;
        StatusList statuses;
        
        // GET: Status
        public ActionResult Index()
        {
            statuses = new StatusList();
            statuses.Load();
            return View(statuses);
        }

        // GET: Status/Details/5
        public ActionResult Details(Guid id)
        {
            status = new Status();
            status.LoadById(id);
            return View(status);
        }

        // GET: Status/Create
        public ActionResult Create()
        {
            status = new Status();
            return View(status);
        }

        // POST: Status/Create
        [HttpPost]
        public ActionResult Create(Status s)
        {
            try
            {
                // TODO: Add insert logic here
                s.Insert();
                return RedirectToAction("Index");
            }
            catch { return View(s); }
        }

        // GET: Status/Edit/5
        public ActionResult Edit(Guid id)
        {
            status = new Status();
            status.LoadById(id);
            return View(status);
        }

        // POST: Status/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Status s)
        {
            try
            {
                // TODO: Add update logic here
                s.Update();
                return RedirectToAction("Index");
            }
            catch { return View(s); }
        }

        // GET: Status/Delete/5
        public ActionResult Delete(Guid id)
        {
            status = new Status();
            status.LoadById(id);
            return View(status);
        }

        // POST: Status/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Status s)
        {
            try
            {
                // TODO: Add delete logic here
                s.Delete();
                return RedirectToAction("Index");
            }
            catch { return View(s); }
        }
    }
}
