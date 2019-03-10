using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;
using MB.AgilePortfolio.MVCUI.ViewModels;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class PortfolioProjectController : Controller
    {
        PortfolioProjectList portfolioProjects;

        // GET: PortfolioProject
        public ActionResult Index()
        {
            portfolioProjects = new PortfolioProjectList();
            portfolioProjects.Load();
            return View(portfolioProjects);
        }

        // GET: PortfolioProject/Details/5
        public ActionResult Details(Guid id)
        {
            PortfolioProjectViewModel ppvm = new PortfolioProjectViewModel()
            {
                PortfolioProject = new PortfolioProject(),
                Portfolios = new PortfolioList(),
                Projects = new ProjectList(),
            };
            ppvm.PortfolioProject.LoadById(id);
            ppvm.Projects.Load();
            ppvm.Portfolios.Load();


            return View(ppvm);
        }

        // GET: PortfolioProject/Create
        public ActionResult Create()
        {
            PortfolioProjectViewModel ppvm = new PortfolioProjectViewModel()
            {
                PortfolioProject = new PortfolioProject(),
                Portfolios = new PortfolioList(),
                Projects = new ProjectList(),
            };
            ppvm.Projects.Load();
            ppvm.Portfolios.Load();
            
            return View(ppvm);
        }

        // POST: PortfolioProject/Create
        [HttpPost]
        public ActionResult Create(PortfolioProjectViewModel ppvm )
        {
            try
            {
                // TODO: Add insert logic here
                ppvm.PortfolioProject.Insert();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PortfolioProject/Edit/5
        public ActionResult Edit(Guid id)
        {
            PortfolioProjectViewModel ppvm = new PortfolioProjectViewModel()
            {
                PortfolioProject = new PortfolioProject(),
                Portfolios = new PortfolioList(),
                Projects = new ProjectList(),
            };
            ppvm.PortfolioProject.LoadById(id);
            ppvm.Projects.Load();
            ppvm.Portfolios.Load();

            return View(ppvm);
        }

        // POST: PortfolioProject/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, PortfolioProjectViewModel ppvm)
        {
            try
            {
                // TODO: Add update logic here
                ppvm.PortfolioProject.Update();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(ppvm);
            }
        }

        // GET: PortfolioProject/Delete/5
        public ActionResult Delete(Guid id)
        {
            PortfolioProjectViewModel ppvm = new PortfolioProjectViewModel()
            {
                PortfolioProject = new PortfolioProject(),
                Portfolios = new PortfolioList(),
                Projects = new ProjectList(),
            };
            ppvm.PortfolioProject.LoadById(id);
            ppvm.Projects.Load();
            ppvm.Portfolios.Load();

            return View(ppvm);
        }

        // POST: PortfolioProject/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, PortfolioProject pp)
        {
            try
            {
                // TODO: Add delete logic here
                pp.Delete();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(pp);
            }
        }
    }
}
