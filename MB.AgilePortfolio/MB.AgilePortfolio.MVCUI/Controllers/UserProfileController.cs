using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;
using MB.AgilePortfolio.MVCUI.Models;
using MB.AgilePortfolio.MVCUI.ViewModels;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class UserProfileController : Controller
    {
        User user;

        // GET: UserProfile
        public ActionResult Index()
        {
            UserProfile up = new UserProfile();
            if (Authenticate.IsAuthenticated())
            {


                // REDIRECT TO PROFILE EDIT PAGE
                //return RedirectToAction("EditProfile", "UserProfile", new { returnurl = HttpContext.Request.Url });
                User userin = System.Web.HttpContext.Current.Session["user"] as User;
                return RedirectToAction("EditProfile", "UserProfile");
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }

        }


        // GET: UserProfile/Details/5
        public ActionResult Details(Guid id)
        {
            user = new User();
            user.LoadById(id);
            return View(user);
        }

        public ActionResult EditProjects()
        {
            UserProfile up = new UserProfile()
            {
                Projects = new ProjectList(),
                User = new User()
            };
            if (Authenticate.IsAuthenticated())
            {
                User userin = System.Web.HttpContext.Current.Session["user"] as User;
                up.User.LoadById(userin.Id);
                up.Projects.LoadbyUser(up.User);


                // REDIRECT TO PROJECT EDIT PAGE AND REDIRECTION LOGIC HERE
                return View(up);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: UserProfile/EditProject
        public ActionResult EditProject(Guid id)
        {
            UserProfile up = new UserProfile()
            {
                Project = new Project(),
                Privacies = new PrivacyList(),
                User = new User(),
                Statuses = new StatusList()
            };
            if (Authenticate.IsAuthenticated())
            {
                up.Project.LoadById(id);
                up.Privacies.Load();
                User userin = System.Web.HttpContext.Current.Session["user"] as User;
                up.User.LoadById(userin.Id);
                up.Statuses.Load();
                return View(up);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: UserProfile/EditProject
        [HttpPost]
        public ActionResult EditProject(Guid id, UserProfile up)
        {
            if (Authenticate.IsAuthenticated())
            {
                try
                {
                    // TODO: Add update logic here
                    up.Project.Update();
                    return RedirectToAction("EditProjects");
                }
                catch { return View(up); }
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        //TODO NEEDS DELETE PROJECT ACTION HERE

        // UserProfile/EditPortfolios
        public ActionResult EditPortfolios()
        {
            UserProfile up = new UserProfile()
            {
                Portfolios = new PortfolioList(),
                User = new User()
            };
            if (Authenticate.IsAuthenticated())
            {
                User userin = System.Web.HttpContext.Current.Session["user"] as User;
                up.User.LoadById(userin.Id);
                up.Portfolios.LoadbyUser(up.User);


                // REDIRECT TO PROJECT EDIT PAGE AND REDIRECTION LOGIC HERE
                return View(up);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Edit User Profile Redirect (UserProfile/EditPortfolio)
        public ActionResult EditPortfolio(Guid id)
        {
            UserProfile up = new UserProfile()
            {
                Portfolio = new Portfolio(),
                User = new User()
            };
            if (Authenticate.IsAuthenticated())
            {
                up.Portfolio.LoadById(id);
                User userin = System.Web.HttpContext.Current.Session["user"] as User;
                up.User.LoadById(userin.Id);

                return View(up);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Edit User Profile Redirect (UserProfile/EditPortfolio)
        [HttpPost]
        public ActionResult EditPortfolio(Guid id, UserProfile up)
        {
            if (Authenticate.IsAuthenticated())
            {
                try
                {
                    // TODO: Add update logic here
                    up.Portfolio.Update();
                    return RedirectToAction("EditPortfolios");
                }
                catch { return View(up); }
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }
        //TODO NEEDS DELETE PORTFOLIO ACTION HERE

        // GET: Edit User Profile (UserProfile/EditProfile)
        public ActionResult EditProfile()
        {
            UserProfile up = new UserProfile()
            {
                Projects = new ProjectList(),
                Portfolios = new PortfolioList(),
                User = new User(),
                UserTypes = new UserTypeList()
            };

            if (Authenticate.IsAuthenticated())
            {
                User userin = System.Web.HttpContext.Current.Session["user"] as User;
                up.User.LoadById(userin.Id);
                up.UserTypes.LoadNonAdmin();
                return View(up);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }


        // POST: Edit User Profile (UserProfile/EditProfile)
        [HttpPost]
        public ActionResult EditProfile(UserProfile up)
        {
            try
            {
                User userin = System.Web.HttpContext.Current.Session["user"] as User;
                string currentemail = userin.Email;
                if (up.User.Email == null)
                {
                    ModelState.AddModelError(string.Empty, "Email address is required");
                }

                else if (up.User.Email != currentemail)
                {
                    if (up.User.CheckIfEmailExists(up.User.Email) == true)
                    {
                        ModelState.AddModelError(string.Empty, "Email Already In Use");
                    }
                }

                if (up.User.FirstName == null)
                {
                    ModelState.AddModelError(string.Empty, "First Name is required");
                }

                if (up.User.LastName == null)
                {
                    ModelState.AddModelError(string.Empty, "Last Name is required");
                }

                // TODO: NEEDS UPDATE PASSWORD ACTION ADDED

                /*
                if (uut.User.Password == null)
                {
                    ModelState.AddModelError(string.Empty, "Password is required");
                }

                else if (uut.User.Password.Length < 6)
                {
                    ModelState.AddModelError(string.Empty, "Password needs to be at least 6 characters");
                }

                else if (uut.User.Password.Length > 16)
                {
                    ModelState.AddModelError(string.Empty, "Password needs to be less than 16 characters");
                }

                else if (uut.ConfirmPassword != uut.User.Password)
                {
                    ModelState.AddModelError(string.Empty, "Passwords did not match");
                }
                // TODO:
                // ADD VALIDATION FOR EMPLOYER?
                */

                if (!ModelState.IsValid)
                {
                    up.UserTypes = new UserTypeList();
                    up.UserTypes.LoadNonAdmin();
                    return View(up);
                }
                up.User.Id = userin.Id;
                up.User.Update();

                //TODO: Needs Redirect to confimration or Confirmation message of saved changes here!
                return RedirectToAction("Index", "Admin", new { returnurl = HttpContext.Request.Url });
            }
            catch
            {
                return View(up);
            }
        }


    }
}
