using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;
using MB.AgilePortfolio.MVCUI.ViewModels;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class ScreenshotController : Controller
    {
        Screenshot screenshot;
        ScreenshotList screenshots;
        
        // GET: Screenshot
        public ActionResult Index()
        {
            screenshots = new ScreenshotList();
            screenshots.Load();
            return View(screenshots);
        }

        // GET: Screenshot/Details/5
        public ActionResult Details(Guid id)
        {
            screenshot = new Screenshot();
            screenshot.LoadById(id);
            return View(screenshot);
        }

        // GET: Screenshot/Create
        public ActionResult Create()
        {
            ScreenshotProjects sp = new ScreenshotProjects()
            {
                Screenshot = new Screenshot(),
                Projects = new ProjectList()
            };
            sp.Projects.Load();
            return View(sp);
        }

        // POST: Screenshot/Create
        [HttpPost]
        public ActionResult Create(ScreenshotProjects sp)
        {
            try
            {
                // TODO: Add insert logic here
                sp.Screenshot.Insert();
                return RedirectToAction("Index");
            }
            catch { return View(sp); }
        }

        // GET: Screenshot/Edit/5
        public ActionResult Edit(Guid id)
        {
            ScreenshotProjects sp = new ScreenshotProjects()
            {
                Screenshot = new Screenshot(),
                Projects = new ProjectList()
            };
            sp.Projects.Load();
            sp.Screenshot.LoadById(id);
            return View(sp);
        }

        // POST: Screenshot/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, ScreenshotProjects sp)
        {
            try
            {
                // TODO: Add update logic here
                sp.Screenshot.Update();
                return RedirectToAction("Index");
            }
            catch { return View(sp); }
        }

        // GET: Screenshot/Delete/5
        public ActionResult Delete(Guid id)
        {
            screenshot = new Screenshot();
            screenshot.LoadById(id);
            return View(screenshot);
        }

        // POST: Screenshot/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Screenshot s)
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
