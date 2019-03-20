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
                Statuses = new StatusList(),
                User = new User()
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
                    User userin = System.Web.HttpContext.Current.Session["user"] as User;
                    ppus.Project.UserId = userin.Id;
                    ppus.Project.UserEmail = userin.Email;
                    Projects.LoadbyUser(userin);
                    if (ppus.Project.Name == null)
                    {
                        ModelState.AddModelError(string.Empty, "Project requires a name!");
                    }
                    else
                    {
                        foreach (Project p in Projects)
                        {
                            if (ppus.Project.Name == p.Name)
                            {
                                ModelState.AddModelError(string.Empty, "Project name already exists!");
                            }
                        }

                        if (ppus.DateCreated == null)
                        {
                            ModelState.AddModelError(string.Empty, "Date Created required!");
                        }
                        else if(ppus.LastUpdated == null)
                        {
                            ppus.LastUpdated = ppus.DateCreated;
                        }
                    }

                    if (!ModelState.IsValid)
                    {
                        ppus.Project = new Project();
                        ppus.Privacies = new PrivacyList();
                        ppus.Statuses = new StatusList();
                        ppus.User = new User();
                        ppus.User.LoadById(userin.Id);
                        ppus.Privacies.Load();
                        ppus.Statuses.Load();
                        return View(ppus);
                    }

                    ppus.Project.Insert();
                    return RedirectToAction("Index", "Admin", new { returnurl = HttpContext.Request.Url });
                }
                catch { return View(ppus); }
            }
            else
            {
                // THIS NEEDS TO HAVE LOGIN RETURN TO CREATE PROJECT SCREEN UPON LOGIN YET
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }

        }
    }
}
