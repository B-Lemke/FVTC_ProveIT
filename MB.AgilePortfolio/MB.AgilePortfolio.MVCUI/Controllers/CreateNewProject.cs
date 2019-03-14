using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;
using MB.AgilePortfolio.MVCUI.ViewModels;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class CreateNewProjectController : Controller
    {
        User user;
        //UserList users;
        Project project;
        ProjectList projects;

        // GET: Project
        public ActionResult Index()
        {
            projects = new ProjectList();
            projects.Load();
            return View(projects);
        }

        // GET: Project/Details/5
        public ActionResult Details(Guid id)
        {

            project = new Project();
            project.LoadById(id);


            return View(project);
        }

        // GET: Project/Create
        public ActionResult Create(User user)
        {

            // TODO: 
            // NEEDS LOGIC IF USER IS LOGGED IN OR NOT HERE
            // - If not logged in redirect to login page
            // - else user = currently logged in user.... not entirely sure how to approach this part

            ProjectPrivaciesUserStatuses ppus = new ProjectPrivaciesUserStatuses()
            {
                Project = new Project(),
                Privacies = new PrivacyList(),
                User = new User(),
                Statuses = new StatusList(),
                Users = new UserList()
            };

            //Test Setup
            ppus.Users.Load();
            ppus.User = ppus.Users.FirstOrDefault(u => u.Email == "joe@wetzel.com");
            Guid uid = ppus.User.Id;
            //END Test Setup

            ppus.Privacies.Load();
            //ppus.User.LoadById(uid);
            ppus.Statuses.Load();
            return View(ppus);
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(ProjectPrivaciesUserStatuses ppus)
        {
            try
            {
                // TODO: Add insert logic here
                ProjectList Projects = new ProjectList();
                Projects.Load(ppus.User.Id);
                
                foreach (Project p in Projects)
                {
                    if(ppus.Project.Name == p.Name)
                    {
                        ModelState.AddModelError(string.Empty, "Project name already exists!");
                    }
                }

                if (!ModelState.IsValid)
                {
                    ppus.Privacies.Load();
                    ppus.Statuses.Load();
                    return View(ppus);
                }
                // if projectname already exists in profile throw warning "Name already exists in Portfolio"
                
                // THIS BREAKS DUE TO NULL PRIVACIES YET

                ppus.Project.Insert();
                return RedirectToAction("Create");
            }
            catch { return View(ppus); }
        }
    }
}
