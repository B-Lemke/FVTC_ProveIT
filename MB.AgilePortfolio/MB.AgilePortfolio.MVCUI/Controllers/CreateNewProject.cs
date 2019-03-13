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
        public ActionResult Create(ProjectPrivaciesUserStatuses ppus, User user)
        {
            try
            {
                // TODO: Add insert logic here
                ppus.Project.Insert();
                return RedirectToAction("Index");
            }
            catch { return View(ppus); }
        }

        // GET: Project/Edit/5
        public ActionResult Edit(Guid id, User user)
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
                Statuses = new StatusList()
            };
            ppus.Project.LoadById(id);
            ppus.Privacies.Load();
            ppus.User.LoadById(user.Id);
            ppus.Statuses.Load();
            return View(ppus);
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, ProjectPrivaciesUserStatuses ppus, User user)
        {
            try
            {
                // TODO: Add update logic here
                ppus.Project.Update();
                return RedirectToAction("Index");
            }
            catch { return View(ppus); }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(Guid id, User user)
        {

            project = new Project();
            project.LoadById(id);
           
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Project p, User user)
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
