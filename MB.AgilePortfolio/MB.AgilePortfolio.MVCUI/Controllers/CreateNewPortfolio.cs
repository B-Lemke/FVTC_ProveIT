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
                    HttpPostedFileBase fileupload = pu.Fileupload;
                    string fileName = Path.GetFileName(fileupload.FileName);
                    string savepath = "";
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

                    if (pu.Portfolio.PortfolioImage == string.Empty)
                    {
                        pu.Portfolio.PortfolioImage = "Assets/Images/UserProfiles/Default.png";
                    }
                    else
                    {
                        var fullPath = Server.MapPath("~/" + pu.Portfolio.PortfolioImage);

                        if (System.IO.File.Exists(fullPath))
                        {

                        }
                        else
                        {
                            pu.Portfolio.PortfolioImage = "Assets/Images/UserProfiles/Default.png";
                        }

                        if (Directory.Exists("~/Images/PortfolioImages"))
                        {
                            savepath = "Assets/Images/PortfolioImages";
                        }
                        else
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Assets/Images/PortfolioImages"));
                            savepath = "Assets/Images/PortfolioImages";
                        }

                        if (Directory.Exists("~/Assets/Images/PortfolioImages/" + username))
                        {
                            savepath = "Assets/Images/PortfolioImages/" + username;
                        }
                        else
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Assets/Images/PortfolioImages/" + username));
                            savepath = "Assets/Images/PortfolioImages/" + username;
                        }

                        if (Directory.Exists("~/Assets/Images/PortfolioImages/" + username + "/" + pu.Portfolio.Name))
                        {
                            savepath = "Assets/Images/PortfolioImages/" + username + "/" + pu.Portfolio.Name;
                        }
                        else
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Assets/Images/PortfolioImages/" + username + "/" + pu.Portfolio.Name));
                            savepath = "Assets/Images/PortfolioImages/" + username + "/" + pu.Portfolio.Name;
                        }

                        fullPath = Server.MapPath("~/Assets/Images/PortfolioImages/" + username + "/" + pu.Portfolio.Name + "/" + fileName);

                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                            ViewBag.deleteSuccess = "true";
                        }

                        fileupload.SaveAs(Server.MapPath("~/" + savepath + "/" + fileName));
                        pu.Portfolio.PortfolioImage = savepath + "/" + fileName;
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
