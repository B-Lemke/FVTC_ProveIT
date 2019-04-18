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
    public class PortfoliosController : Controller
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

        #region Portfolio
        // ----------------------------------- START PORTFOLIO ------------------------------------------


        // GET: PublicPortfolio
        public ActionResult PublicPortfolio(string username, string portfolioName)
        {
            User user = new User();
            UserProfile up = new UserProfile()
            {
                Portfolio = new Portfolio(),
                Portfolios = new PortfolioList(),
                Projects = new ProjectList(),
                Privacies = new PrivacyList(),
                User = new User()
            };

            UserList users = new UserList();
            users.Load();
            up.User = users.FirstOrDefault(p => p.UrlFriendlyName == username.ToLower());
            Guid userId = user.CheckIfUsernameExists(up.User.Username);
            if (userId != Guid.Empty)
            {
                PortfolioList pl = new PortfolioList();
                pl.LoadbyUserID(userId);

                up.Portfolio = pl.FirstOrDefault(p => p.UrlFriendlyName == portfolioName.ToLower());

                if (up.Portfolio != null && up.Portfolio.Id != Guid.Empty)
                {
                    up.User.LoadById(userId);
                    up.Projects.LoadbyPortfolioID(up.Portfolio.Id);
                }
                else
                {
                    // Portfolio doesnt exit (cleaned portfolio name passed

                    // TO DO: ADD LOGIC FOR THIS?
                }
            }
            else
            {
                // Username passed doesnt exist (cleaned username passed)

                // TO DO: ADD LOGIC FOR THIS?
            }

            return View(up);
        }

        // POST: PublicPortfolio
        [HttpPost]
        public ActionResult PublicPortfolio(Guid id, UserProfile up)
        {
            try
            {
                return View(up);
            }
            catch { return View(up); }
        }

        //          ++++++++++ START PORTFOLIOS +++++++++++++

        // GET: UserProfile/PublicPortfolios
        public ActionResult PublicPortfolios(string username)
        {
            User user = new User();
            UserProfile up = new UserProfile()
            {
                Portfolio = new Portfolio(),
                Portfolios = new PortfolioList(),
                User = new User()
            };

            UserList users = new UserList();
            users.Load();
            up.User = users.FirstOrDefault(p => p.UrlFriendlyName == username.ToLower());
            Guid ID = up.User.CheckIfUsernameExists(up.User.Username);
            if (ID != Guid.Empty)
            {
                up.User.LoadById(ID);
                up.Portfolios.LoadbyUser(up.User);
            }
            else
            {

            }
            return View(up);
        }

        // POST: UserProfile/PublicPortfolios
        [HttpPost]
        public ActionResult PublicPortfolios(Guid id, UserProfile up)
        {
            try
            {
                return View(up);
            }
            catch { return View(up); }
        }
        //          ++++++++++ END PORTFOLIOS +++++++++++++


        //          ++++++++++++ START EDIT +++++++++++++++

        // GET: EditPortfolio
        public ActionResult EditPortfolio(Guid? id)
        {
            Guid ID = id.GetValueOrDefault();
            if (ID == Guid.Empty)
            {
                if (Authenticate.IsAuthenticated())
                {
                    return RedirectToAction("EditPortfolios", "UserProfile", new { returnurl = HttpContext.Request.Url });
                }
                else
                {
                    return RedirectToAction("Index", "Login", new { returnurl = HttpContext.Request.Url });
                }
            }
            else
            {
                UserProfile up = new UserProfile()
                {
                    Portfolio = new Portfolio(),
                    Privacies = new PrivacyList(),
                    Projects = new ProjectList(),
                    User = new User()
                };

                if (Authenticate.IsAuthenticated())
                {

                    up.Portfolio.LoadById(ID);
                    Portfolio portfolio = new Portfolio();
                    portfolio.LoadById(up.Portfolio.Id);
                    up.Privacies.Load();
                    User userin = System.Web.HttpContext.Current.Session["user"] as User;
                    up.Projects.LoadbyPortfolioID(up.Portfolio.Id);
                    foreach (Project p in up.Projects)
                    {
                        if (p.Image == string.Empty)
                        {
                            p.Image = "Images/UserProfiles/Default.png";
                        }
                    }
                    up.User.LoadById(userin.Id);
                    return View(up);
                }
                else
                {
                    return RedirectToAction("Index", "Login", new { returnurl = HttpContext.Request.Url });
                }
            }
        }

        // POST: EditPortfolio
        [HttpPost]
        public ActionResult EditPortfolio(Guid id, UserProfile up)
        {
            if (Authenticate.IsAuthenticated())
            {
                try
                {
                    User userin = System.Web.HttpContext.Current.Session["user"] as User;
                    PortfolioList Portfolios = new PortfolioList();
                    Portfolios.LoadbyUser(userin);
                    up.Portfolios = Portfolios;
                    ProjectList projects = new ProjectList();
                    projects.LoadbyPortfolioID(id);
                    PrivacyList privacies = new PrivacyList();
                    privacies.Load();
                    up.Privacies = privacies;
                    up.Projects = projects;
                    string username = userin.Username;

                    if (up.Portfolio.Name == null)
                    {
                        ModelState.AddModelError(string.Empty, "Portfolio requires a name!");
                    }
                    else
                    {
                        foreach (Portfolio p in Portfolios)
                        {
                            if (up.Portfolio.Name == p.Name)
                            {
                                if (up.Portfolio.Id != p.Id)
                                {
                                    ModelState.AddModelError(string.Empty, "Another portfolio already exists with this name!");
                                }
                            }
                        }
                    }
                    UploadedImage ui = new UploadedImage
                    {
                        FilePath = up.Portfolio.PortfolioImage,
                        Fileupload = up.Fileupload,
                        UserName = username,
                        ObjectType = "Portfolio",
                        ObjectName = up.Portfolio.Name
                    };

                    string fp = ui.Upload();

                    // fp will return null if no upload file was choosen else use upload file to save to database
                    if (fp != null)
                    {
                        up.Portfolio.PortfolioImage = fp;
                    }
                    else
                    {
                        up.Portfolio.PortfolioImage = null;
                    }

                    if (!ModelState.IsValid)
                    {
                        up.Privacies = new PrivacyList();
                        up.User = new User();
                        up.User.LoadById(userin.Id);
                        up.Privacies.Load();
                        up.Projects.LoadbyPortfolioID(up.Portfolio.Id);
                        return View(up);
                    }
                    up.Portfolio.Update();
                    return RedirectToAction("EditPortfolios");
                }
                catch { return View(up); }
            }
            else
            {
                return RedirectToAction("Index", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        //               EDIT PORTFOLIOS HAS NO POST
        //GET: EditPortfolios
        public ActionResult EditPortfolios()
        {
            UserProfile up = new UserProfile()
            {
                Portfolios = new PortfolioList(),
                Privacies = new PrivacyList(),
                User = new User()
            };

            if (Authenticate.IsAuthenticated())
            {
                User userin = System.Web.HttpContext.Current.Session["user"] as User;
                up.User.LoadById(userin.Id);
                up.Portfolios.LoadbyUser(up.User);
                return View(up);
            }
            else
            {
                return RedirectToAction("Index", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }
        //              ++++++++++++ END EDIT +++++++++++++++++

        // GET: DeletePortfolio
        public ActionResult DeletePortfolio(Guid? id)
        {
            Guid ID = id.GetValueOrDefault();
            if (ID == Guid.Empty)
            {
                if (Authenticate.IsAuthenticated())
                {
                    return RedirectToAction("EditPortfolios", "UserProfile", new { returnurl = HttpContext.Request.Url });
                }
                else
                {
                    return RedirectToAction("Index", "Login", new { returnurl = HttpContext.Request.Url });
                }
            }
            else
            {
                UserProfile up = new UserProfile()
                {
                    Portfolio = new Portfolio(),
                    Privacies = new PrivacyList(),
                    User = new User()
                };

                if (Authenticate.IsAuthenticated())
                {
                    up.Portfolio.LoadById(ID);
                    up.Privacies.Load();
                    User userin = System.Web.HttpContext.Current.Session["user"] as User;
                    up.User.LoadById(userin.Id);
                    return View(up);
                }
                else
                {
                    return RedirectToAction("Index", "Login", new { returnurl = HttpContext.Request.Url });
                }
            }
        }

        // POST: DeletePortfolio
        [HttpPost]
        public ActionResult DeletePortfolio(Guid id, UserProfile up)
        {
            if (Authenticate.IsAuthenticated())
            {
                try
                {
                    User userin = System.Web.HttpContext.Current.Session["user"] as User;
                    Portfolio Portfolio = new Portfolio();
                    Portfolio.LoadById(id);
                    up.Portfolio = Portfolio;
                    UploadedImage ui = new UploadedImage();

                    // THIS DOESNT WORK LOCALLY IF THE DIRECTORY WAS MADE BEFORE IT WAS PUSHED
                    ui.DeletePortfolioUploadFolder(userin.Username, up.Portfolio.Name);
                    up.Portfolio.Delete();
                    return RedirectToAction("PortfolioDeleted");
                }
                catch { return View(up); } //NEEDS A WAY TO SHOW ERROR MESSAGE RETURN OF WRONG OLD PASSWORD ENTERED
            }
            else
            {
                return RedirectToAction("Index", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        public ActionResult PortfolioDeleted(UserProfile up)
        {
            if (Authenticate.IsAuthenticated())
            {
                try
                {
                    return View(up);
                }
                catch { return View(up); } //NEEDS A WAY TO SHOW ERROR MESSAGE RETURN OF WRONG OLD PASSWORD ENTERED
            }
            else
            {
                return RedirectToAction("Index", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        // ----------------------------------- END PORTFOLIOS ------------------------------------------
        #endregion Portfolio
    }
}
