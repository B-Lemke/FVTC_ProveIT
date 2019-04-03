using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;
using MB.AgilePortfolio.MVCUI.ViewModels;
using MB.AgilePortfolio.MVCUI.Models;
using System.IO;

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
            ScreenshotProjects sp = new ScreenshotProjects()
            {
                Screenshot = new Screenshot(),
                Projects = new ProjectList()
            };
            sp.Projects.Load();
            sp.Screenshot.LoadById(id);

            return View(sp);
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
            ScreenshotProjects sp = new ScreenshotProjects()
            {
                Screenshot = new Screenshot(),
                Projects = new ProjectList()
            };
            sp.Projects.Load();
            sp.Screenshot.LoadById(id);

            return View(sp);
        }

        // POST: Screenshot/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Screenshot s)
        {
            try
            {
                Screenshot ss = new Screenshot();
                ss.LoadById(s.Id);
                s.Delete();
                return RedirectToAction("UploadProjectSliderImage", new { id = ss.ProjectId });
            }
            catch { return View(s); }
        }

        // GET: Admin
        public ActionResult UploadProjectSliderImage(Guid? id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Guid ID = id.GetValueOrDefault();
                if (ID == Guid.Empty)
                {
                    if (Authenticate.IsAuthenticated())
                    {
                        return RedirectToAction("EditProjects", "UserProfile", new { returnurl = HttpContext.Request.Url });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login", new { returnurl = HttpContext.Request.Url });
                    }
                }
                ScreenshotProjects sp = new ScreenshotProjects()
                {
                    Screenshot = new Screenshot(),
                    ScreenshotList = new ScreenshotList()
                };
                sp.ScreenshotList.LoadbyProjectID(ID);
                return View( sp);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        [HttpPost]
        public ActionResult UploadProjectSliderImage(Guid id, ScreenshotProjects sp)
        {

            HttpPostedFileBase fileupload= sp.Fileupload;
            if (fileupload != null)
            {
                User userin = System.Web.HttpContext.Current.Session["user"] as User;
                string username = userin.Username;
                string fileName = Path.GetFileName(fileupload.FileName);
                if (Directory.Exists("~/Images/ScreenShots/" + username))
                {

                }
                else
                {
                    //THIS IS HAVING ISSUES  LOCALLY
                    Directory.CreateDirectory(Server.MapPath("~/Images/ScreenShots/" + username));
                }
                fileupload.SaveAs(Server.MapPath("~/Images/ScreenShots/" + username + "/" + fileName));
                Screenshot ss = new Screenshot();

                ss.ProjectId = id;
                // ss.FilePath = Server.MapPath(~/Images/ScreenShots/" + username + "/" + fileName);

                ss.FilePath = "Images/ScreenShots/" + username + "/" + fileName;
                ss.Insert();
            }
            return RedirectToAction("UploadProjectSliderImage");
        }
    }
}
