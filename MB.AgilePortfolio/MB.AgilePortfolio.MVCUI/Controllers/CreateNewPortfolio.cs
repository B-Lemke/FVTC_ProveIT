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
                Privacies = new PrivacyList(),
                User = new User()
            };
            pu.Privacies.Load();
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
                    pu.Privacies = new PrivacyList();
                    pu.Privacies.Load();

                    
                    PortfolioList portfolios = new PortfolioList();
                    User userin = System.Web.HttpContext.Current.Session["user"] as User;
                    string username = userin.Username;
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

                    UploadedImage ui = new UploadedImage
                    {
                        FilePath = pu.Portfolio.PortfolioImage,
                        Fileupload = pu.Fileupload,
                        UserName = username,
                        ObjectType = "Portfolio",
                        ObjectName = pu.Portfolio.Name
                    };

                    string fp = ui.Upload();

                    // fp will return null if no upload file was choosen else use upload file to save to database
                    if (fp != null)
                    {
                        pu.Portfolio.PortfolioImage = fp;
                    }
                    else
                    {
                        // I honestly don't know when this would happen but just in case
                        ModelState.AddModelError(string.Empty, "Portfolio Image could not found");
                    }
                    

                    if (!ModelState.IsValid)
                    {
                        pu.Portfolio = new Portfolio();
                        pu.User = new User();
                        pu.Privacies = new PrivacyList();
                        pu.Privacies.Load();
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
