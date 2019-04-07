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
        HttpPostedFileBase Fileupload;

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
                    HttpPostedFileBase fileupload = ppus.Fileupload;
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
                    string savepath = "";
                    ppus.Project.UserId = userin.Id;
                    ppus.Project.UserEmail = userin.Email;
                    string fileName = Path.GetFileName(fileupload.FileName);

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

                    if (ppus.Project.Image == string.Empty)
                    {
                        ppus.Project.Image = "Assets/Images/UserProfiles/Default.png";


                    }
                    else
                    {
                        if (Directory.Exists("~/Images/ScreenShots"))
                        {
                            savepath = "Assets/Images/ScreenShots";
                        }
                        else
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Assets/Images/ScreenShots"));
                            savepath = "Assets/Images/ScreenShots";
                        }

                        if (Directory.Exists("~/Assets/Images/ScreenShots/" + username))
                        {
                            savepath = "Assets/Images/ScreenShots/" + username;
                        }
                        else
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Assets/Images/ScreenShots/" + username));
                            savepath = "Assets/Images/ScreenShots/" + username;
                        }

                        if (Directory.Exists("~/Assets/Images/ScreenShots/" + username + "/" + ppus.Project.Name))
                        {
                            savepath = "Assets/Images/ScreenShots/" + username + "/" + ppus.Project.Name;
                        }
                        else
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Assets/Images/ScreenShots/" + username + "/" + ppus.Project.Name));
                            savepath = "Assets/Images/ScreenShots/" + username + "/" + ppus.Project.Name;
                        }

                        var fullPath = Server.MapPath("~/Assets/Images/ScreenShots/" + username + "/" + ppus.Project.Name + "/" + fileName);

                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                            ViewBag.deleteSuccess = "true";
                        }

                        fileupload.SaveAs(Server.MapPath("~/" + savepath + "/" + fileName));
                        ppus.Project.Image = savepath + "/" + fileName;
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
