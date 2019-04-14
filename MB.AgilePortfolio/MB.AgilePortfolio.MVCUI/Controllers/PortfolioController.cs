using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;
using MB.AgilePortfolio.MVCUI.ViewModels;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class PortfolioController : Controller
    {
        Portfolio portfolio;
        PortfolioList portfolios;
        
        // GET: Portfolio
        public ActionResult Index()
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            portfolios = new PortfolioList();
            portfolios.Load();
            return View(portfolios);
        }

        // GET: Portfolio/Details/5
        public ActionResult Details(Guid id)
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            PortfolioUsers pu = new PortfolioUsers()
            {
                Portfolio = new Portfolio(),
                Users = new UserList()
            };
            pu.Portfolio.LoadById(id);
            pu.Users.Load();


            return View(pu);
        }

        // GET: Portfolio/Create
        public ActionResult Create()
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            PortfolioUsers pu = new PortfolioUsers()
            {
                Portfolio = new Portfolio(),
                Privacies = new PrivacyList(),
                Users = new UserList()
            };
            pu.Privacies.Load();
            pu.Users.Load();
            return View(pu);
        }

        // POST: Portfolio/Create
        [HttpPost]
        public ActionResult Create(PortfolioUsers pu)
        {
            try
            {
                // TODO: Add insert logic here
                pu.Portfolio.Insert();
                return RedirectToAction("Index");
            }
            catch { return View(pu); }
        }

        // GET: Portfolio/Edit/5
        public ActionResult Edit(Guid id)
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            PortfolioUsers pu = new PortfolioUsers()
            {
                Portfolio = new Portfolio(),
                Privacies = new PrivacyList(),
                Users = new UserList()
            };
            pu.Portfolio.LoadById(id);
            pu.Privacies.Load();
            pu.Users.Load();
            return View(pu);
        }

        // POST: Portfolio/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, PortfolioUsers pu)
        {
            try
            {
                // TODO: Add update logic here
                pu.Portfolio.Update();
                return RedirectToAction("Index");
            }
            catch { return View(pu); }
        }

        // GET: Portfolio/Delete/5
        public ActionResult Delete(Guid id)
        {
            User userin = System.Web.HttpContext.Current.Session["user"] as User;
            if (userin == null || userin.UserTypeDescription != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            PortfolioUsers pu = new PortfolioUsers()
            {
                Portfolio = new Portfolio(),
                Users = new UserList()
            };
            pu.Portfolio.LoadById(id);
            pu.Users.Load();


            return View(pu);
        }

        // POST: Portfolio/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Portfolio p)
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
