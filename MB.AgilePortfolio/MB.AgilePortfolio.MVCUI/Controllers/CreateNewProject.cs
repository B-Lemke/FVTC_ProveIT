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
                return RedirectToAction("Index", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(ProjectPrivaciesUserStatuses ppus, string returnurl)
        {

            //double check authentication
            if (Authenticate.IsAuthenticated())
            {
                try
                {

                    PrivacyList plist = new PrivacyList();
                    plist.Load();
                    ppus.Privacies = plist;
                    StatusList slist = new StatusList();
                    slist.Load();
                    ppus.Statuses = slist;

                    ProjectList Projects = new ProjectList();
                    User userin = System.Web.HttpContext.Current.Session["user"] as User;
                    string username = userin.Username;
                    string strUserID = userin.Id.ToString();
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
                        else if (ppus.LastUpdated == null)
                        {
                            ppus.LastUpdated = ppus.DateCreated;
                        }
                    }

                    UploadedImage ui = new UploadedImage
                    {
                        FilePath = ppus.Project.Image,
                        Fileupload = ppus.Fileupload,
                        UserName = username,
                        ObjectType = "Project",
                        ObjectName = ppus.Project.Name
                    };

                    
                    string fp = ui.Upload();
                    // fp will return null if no upload file was choosen else use upload file to save to database
                    if (fp != null)
                    {
                        ppus.Project.Image = fp;
                    }
                    else
                    {
                        // I honestly don't know when this would happen but just in case
                        ModelState.AddModelError(string.Empty, "Portfolio Image could not found");
                    }

                    if (!ModelState.IsValid)
                    {
                        ppus.User = new User();
                        ppus.User.LoadById(userin.Id);
                        return View(ppus);
                    }

                    ppus.Project.Insert();
                    return RedirectToAction("EditProjects", "UserProfile", new { returnurl = HttpContext.Request.Url });
                }
                catch { return View(ppus); }
            }
            else
            {
                return RedirectToAction("Index", "Login", new { returnurl = HttpContext.Request.Url });
            }

        }
    }
}
