using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;
using MB.AgilePortfolio.MVCUI.ViewModels;
using MB.AgilePortfolio.MVCUI.Models;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class CreateNewProjectController : Controller
    {
        ProjectList projects;

        // GET: Project
        public ActionResult Index()
        {
            projects = new ProjectList();
            projects.Load();
            return View(projects);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            ProjectPrivaciesUserStatuses ppus = new ProjectPrivaciesUserStatuses()
            {
                Project = new Project(),
                Privacies = new PrivacyList(),
                User = new User(),
                Statuses = new StatusList(),
                Users = new UserList()
            };
            ppus.Privacies.Load();
            ppus.Statuses.Load();
            if (Authenticate.IsAuthenticated())
            {
                User userin = System.Web.HttpContext.Current.Session["user"] as User;
                ppus.User.LoadById(userin.Id);
                return View(ppus);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(ProjectPrivaciesUserStatuses ppus)
        {
            //double check authentication
            if (Authenticate.IsAuthenticated())
            {
                try
                {
                    ProjectList Projects = new ProjectList();
                    Projects.Load(ppus.Project.UserId);

                    foreach (Project p in Projects)
                    {
                        if (ppus.Project.Name == p.Name)
                        {
                            ModelState.AddModelError(string.Empty, "Project name already exists!");
                        }
                    }

                    if (!ModelState.IsValid)
                    {
                        return View(ppus);
                    }

                    ppus.Project.Insert();
                    return RedirectToAction("Index", "Admin", new { returnurl = HttpContext.Request.Url });
                }
                catch { return View(ppus); }
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }

        }
    }
}
