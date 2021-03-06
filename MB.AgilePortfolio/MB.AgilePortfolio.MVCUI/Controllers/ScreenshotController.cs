﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;
using MB.AgilePortfolio.MVCUI.ViewModels;
using MB.AgilePortfolio.MVCUI.Models;
using System.IO;
using System.Text;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class ScreenshotController : Controller
    {
        ScreenshotList screenshots;

        // GET: Screenshot
        public ActionResult Index()
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            screenshots = new ScreenshotList();
            screenshots.Load();
            return View(screenshots);
        }

        // GET: Screenshot/Details/5
        public ActionResult Details(Guid id)
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

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
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

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
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

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
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ScreenshotProjects sp = new ScreenshotProjects()
            {
                Projects = new ProjectList(),
                Screenshot = new Screenshot()
            };
            ProjectList pl = new ProjectList();
            pl.Load();

            sp.Projects = pl;
            Screenshot ss = new Screenshot();
            ss.LoadById(id);
            sp.Screenshot = ss;

            return View(sp);
        }

        // POST: Screenshot/Delete/5
        [HttpPost]
        //public ActionResult Delete(Guid screenShotId, ScreenshotProjects sp)
        public ActionResult Delete(Guid screenShotId, Guid projectId)
        {
            try
            {

                var screenShot = Screenshot.StaticLoadById(screenShotId);
                var fullPath = Server.MapPath("~/" + screenShot.FilePath);

                Screenshot.Delete(screenShotId);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    ViewBag.deleteSuccess = "true";
                }
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("UploadProjectSliderImage", new { id = projectId });
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
                sp.ProjectId = ID;
                sp.ScreenshotList.LoadbyProjectID(ID);
                return View(sp);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        [HttpPost]
        public ActionResult UploadProjectSliderImage(Guid id, ScreenshotProjects sp)
        {
            
            HttpPostedFileBase fileupload = sp.Fileupload;
            if (fileupload != null)
            {
                User userin = System.Web.HttpContext.Current.Session["user"] as User;
                string username = userin.Username;
                string strUserID = userin.Id.ToString();
                string fileName = Path.GetFileName(fileupload.FileName);
                string savepath = "";
                if (Directory.Exists("~/Assets/Images/ScreenShots"))
                {
                    savepath = "Assets/Images/ScreenShots/" + username;
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath("~/Assets/Images/ScreenShots/" + username));
                    savepath = "Assets/Images/ScreenShots/" + username;
                }
                Project p = new Project();
                p.LoadById(id);
                sp.Project = p;
                if (Directory.Exists("~/Assets/Images/ScreenShots/{username}/{sp.Project.Name}"))
                {
                    savepath = $"Assets/Images/ScreenShots/{username}/{sp.Project.Name}";
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath($"~/Assets/Images/ScreenShots/{username}/{sp.Project.Name}"));
                    savepath = $"Assets/Images/ScreenShots/{username}/{sp.Project.Name}";
                }


                Screenshot ss = new Screenshot();

                ss.ProjectId = id;


                // ss.FilePath = Server.MapPath(~Assets/Images/ScreenShots/" + username + "/" + fileName);


                fileupload.SaveAs(Server.MapPath("~/" + savepath + "/" + fileName));
                ss.FilePath = savepath + "/" + fileName;
                ss.Insert();
            }
            return RedirectToAction("UploadProjectSliderImage");
        }

        // GET: UserProfile/EditProject
        public ActionResult EditProject(Guid? id)
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
            else
            {
                ScreenshotProjects up = new ScreenshotProjects()
                {
                    SelectedLanguages = new List<string>(),
                    AvailableLanguages = new List<SelectListItem>(),
                    Project = new Project(),
                    Privacies = new PrivacyList(),
                    User = new User(),
                    Statuses = new StatusList(),
                    ScreenshotList = new ScreenshotList(),
                    Languages = new LanguageList(),
                    ProjectLanguage = new ProjectLanguage(),
                    ProjectLanguages = new ProjectLanguageList()
                };

                if (Authenticate.IsAuthenticated())
                {
                    up.Project.LoadById(ID);
                    Project project = new Project();
                    project.LoadById(up.Project.Id);
                    up.DateCreated = project.DateCreated;
                    up.LastUpdated = project.LastUpdated;
                    up.Languages.Load();
                    up.AvailableLanguages = GetLanguages(up.Languages);
                    up.Privacies.Load();
                    User userin = System.Web.HttpContext.Current.Session["user"] as User;
                    up.User.LoadById(userin.Id);
                    up.Statuses.Load();
                    up.Project.LoadById(ID);
                    up.ScreenshotList.LoadbyProjectID(ID);
                    up.ProjectLanguages.LoadByProjectId(ID);
                    up.ProjectId = ID;
                    foreach (ProjectLanguage pl in up.ProjectLanguages)
                    {
                        Language lang = new Language();
                        lang.LoadById(pl.LanguageId);
                        up.SelectedLanguages.Add(lang.Description + ";");
                    }
                    return View(up);
                }
                else
                {
                    return RedirectToAction("Index", "Login", new { returnurl = HttpContext.Request.Url });
                }
            }
        }

        // POST: UserProfile/EditProject
        [HttpPost]
        public ActionResult EditProject(Guid id, ScreenshotProjects ppus)
        {
            if (Authenticate.IsAuthenticated())
            {
                try
                {
                    ppus.Language = new Language();
                    ppus.Languages = new LanguageList();

                    if (ppus.SelectedLanguages == null)
                    {
                        ppus.SelectedLanguages = new List<string>();
                    }

                    if (ppus.AvailableLanguages == null)
                    {
                        ppus.AvailableLanguages = new List<SelectListItem>();
                        if(ppus.Languages.Count < 1)
                        {
                            //Load Languages
                            ppus.Languages.Load();
                        }
                        else
                        {
                            // Languages already loaded
                        }
                        ppus.AvailableLanguages = GetLanguages(ppus.Languages);
                    }
                    
                    PrivacyList plist = new PrivacyList();
                    plist.Load();
                    ppus.Privacies = plist;
                    StatusList slist = new StatusList();
                    slist.Load();
                    
                    ppus.ProjectLanguage = new ProjectLanguage();
                    ppus.ProjectLanguages = new ProjectLanguageList();
                    ppus.Statuses = slist;
                    ppus.ScreenshotList = new ScreenshotList();
                    ppus.ScreenshotList.LoadbyProjectID(id);
                    ppus.ProjectId = id;
                    
                    // Deletes all languages associated with project currently
                    ppus.ProjectLanguages.LoadByProjectId(id);
                    foreach (ProjectLanguage pl in ppus.ProjectLanguages)
                    {
                        pl.Delete();
                    }

                    // Insert New languages parsed from input into ProjectLanguage table
                    foreach (var pl in ppus.SelectedLanguages)
                    {
                        ProjectLanguage projlang = new ProjectLanguage();
                        ppus.ProjectLanguage.ProjectId = id;
                        ppus.ProjectLanguage.LanguageId = (Guid.Parse(pl));
                        ppus.ProjectLanguage.Insert();
                    }

                    User userin = System.Web.HttpContext.Current.Session["user"] as User;
                    ProjectList Projects = new ProjectList();
                    Projects.LoadbyUser(userin);
                    string username = userin.Username;
                    string strUserID = userin.Id.ToString();

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
                                if (ppus.Project.Id != p.Id)
                                {
                                    ModelState.AddModelError(string.Empty, "Another project already exists with this name!");
                                }

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
                    UploadedZip zu = new UploadedZip
                    {
                        FilePath = ppus.Project.Filepath,
                        Fileupload = ppus.ZipFileupload,
                        UserName = username,
                        ProjectName = ppus.Project.Name
                    };
                    UploadedImage ui = new UploadedImage
                    {
                        FilePath = ppus.Project.Image,
                        Fileupload = ppus.Fileupload,
                        UserName = username,
                        ObjectType = "Project",
                        ObjectName = ppus.Project.Name
                    };


                    if (!ModelState.IsValid)
                    {
                        ppus.Languages = new LanguageList();
                        ppus.SelectedLanguages = new List<string>();
                        ppus.AvailableLanguages = new List<SelectListItem>();
                        ppus.Privacies = new PrivacyList();
                        ppus.Statuses = new StatusList();
                        ppus.User = new User();
                        ppus.User.LoadById(userin.Id);
                        ppus.Privacies.Load();
                        ppus.Statuses.Load();
                        ppus.Languages.Load();
                        ppus.AvailableLanguages = GetLanguages(ppus.Languages);
                        return View(ppus);
                    }
                    string fp = ui.Upload();
                    string zp = zu.Upload();
                    // fp will return null if no upload file was choosen else use upload file to save to database
                    if (fp != null)
                    {
                        ppus.Project.Image = fp;
                    }
                    else
                    {
                        ppus.Project.Image = null;
                    }

                    if (zp != null)
                    {
                        ppus.Project.Filepath = zp;
                    }
                    else
                    {
                        ppus.Project.Filepath = null;
                    }

                    ppus.Project.DateCreated = ppus.DateCreated;
                    ppus.Project.LastUpdated = ppus.LastUpdated;
                    ppus.Project.Update();
                    return RedirectToAction("EditProjects", "UserProfile");
                }
                catch { return View(ppus); }
            }
            else
            {
                return RedirectToAction("Index", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }
        public IList<SelectListItem> GetLanguages(LanguageList languages)
        {
            List<SelectListItem> Langlist = new List<SelectListItem>();

            foreach (Language l in languages)
            {
                SelectListItem sl = new SelectListItem { Text = l.Description, Value = l.Id.ToString() };
                Langlist.Add(sl);
            }
            return Langlist;

        }
        
    }
}
