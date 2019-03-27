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
    public class CreateNewPortfolioController : Controller
    {
        Portfolio Portfolio;
        PortfolioList Portfolios;
        
        // GET: Portfolio
        public ActionResult Index()
        {
            Portfolios = new PortfolioList();
            Portfolios.Load();
            return View(Portfolios);
        }

        // GET: Portfolio/Create
        public ActionResult Create()
        {
            PortfolioUsers pu = new PortfolioUsers()
            {
                Portfolio = new Portfolio(),
                User = new User()
            };

            if (Authenticate.IsAuthenticated())
            {
                User userin = System.Web.HttpContext.Current.Session["user"] as User;
                pu.User.LoadById(userin.Id);
                return View(pu);
            }
            else
            {
                return RedirectToAction("Index", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Portfolio/Create
        [HttpPost]
        public ActionResult Create(PortfolioUsers pu)
        {
            //double check authentication
            if (Authenticate.IsAuthenticated())
            {
                try
            {
                    PortfolioList portfolios = new PortfolioList();
                    User userin = System.Web.HttpContext.Current.Session["user"] as User;
                    pu.Portfolio.UserId = userin.Id;
                    pu.Portfolio.UserEmail = userin.Email;
                    portfolios.LoadbyUser(userin);

                    if (pu.Portfolio.Name == null)
                    {
                        ModelState.AddModelError(string.Empty, "Portfolio requires a name!");
                    }
                    else
                    {
                        foreach (Portfolio p in portfolios)
                        {
                            if (pu.Portfolio.Name == p.Name)
                            {
                                ModelState.AddModelError(string.Empty, "Portfolio name already exists!");
                            }
                        }
                    }

                    if (!ModelState.IsValid)
                    {
                        pu.Portfolio = new Portfolio();
                        pu.User = new User();
                        pu.User.LoadById(userin.Id);
                        return View(pu);
                    }

                    pu.Portfolio.Insert();
                    return RedirectToAction("EditPortfolios", "UserProfile", new { returnurl = HttpContext.Request.Url });
                }
            catch { return View(pu); }
            }
            else
            {
                return RedirectToAction("Index", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }
    }
}
