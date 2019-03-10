using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;
using MB.AgilePortfolio.MVCUI.ViewModels;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class ProjectController : Controller
    {
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
        public ActionResult Create()
        {
            ProjectPrivaciesUsersStatuses ppus = new ProjectPrivaciesUsersStatuses()
            {
                Project = new Project(),
                Privacies = new PrivacyList(),
                Users = new UserList(),
                Statuses = new StatusList()
            };
            ppus.Privacies.Load();
            ppus.Users.Load();
            ppus.Statuses.Load();
            return View(ppus);
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(ProjectPrivaciesUsersStatuses ppus)
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
        public ActionResult Edit(Guid id)
        {
            ProjectPrivaciesUsersStatuses ppus = new ProjectPrivaciesUsersStatuses()
            {
                Project = new Project(),
                Privacies = new PrivacyList(),
                Users = new UserList(),
                Statuses = new StatusList()
            };
            ppus.Project.LoadById(id);
            ppus.Privacies.Load();
            ppus.Users.Load();
            ppus.Statuses.Load();
            return View(ppus);
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, ProjectPrivaciesUsersStatuses ppus)
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
        public ActionResult Delete(Guid id)
        {

            project = new Project();
            project.LoadById(id);
           
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Project p)
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
