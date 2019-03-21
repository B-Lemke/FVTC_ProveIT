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
        UserList users;

        // GET: User
        public ActionResult Index()
        {
            UserProfile up = new UserProfile();
            if (Authenticate.IsAuthenticated())
            {


                // REDIRECT TO PROFILE EDIT PAGE
                return RedirectToAction("EditProfile", "UserProfile", new { returnurl = HttpContext.Request.Url });
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }

        }
        

        // GET: User/Details/5
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
                //return RedirectToAction("EditProjects", "UserProfile", new { returnurl = HttpContext.Request.Url });
                return View(up);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Edit User Profile Redirect (?/EditProject)
        public ActionResult EditProject(Guid id)
        {
            UserProfile up = new UserProfile()
            {
                Project = new Project(),
                User = new User()
            };
            if (Authenticate.IsAuthenticated())
            {
                User userin = System.Web.HttpContext.Current.Session["user"] as User;
                up.User.LoadById(userin.Id);
                up.Project.Id = id;


                // REDIRECT TO PROJECT EDIT PAGE AND REDIRECTION LOGIC HERE
                return View(up);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

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
                //return RedirectToAction("EditProjects", "UserProfile", new { returnurl = HttpContext.Request.Url });
                return View(up);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Edit User Profile Redirect (?/EditProject)
        public ActionResult EditPortfolio(Guid id)
        {
            UserProfile up = new UserProfile()
            {
                Portfolio = new Portfolio(),
                User = new User()
            };
            if (Authenticate.IsAuthenticated())
            {
                User userin = System.Web.HttpContext.Current.Session["user"] as User;
                up.User.LoadById(userin.Id);
                up.Portfolio.Id = id;


                // REDIRECT TO PORTFOLIO EDIT PAGE AND REDIRECTION LOGIC HERE
                return RedirectToAction("Portfolio", "Edit", new { returnurl = HttpContext.Request.Url });
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Edit User Profile (UserProfile/EditProfile)
        public ActionResult EditProfile()
        {
            UserProfile up = new UserProfile()
            {
                Projects = new ProjectList(),
                Portfolios = new PortfolioList(),
                User = new User()
            };
            
            if (Authenticate.IsAuthenticated())
            {
                User userin = System.Web.HttpContext.Current.Session["user"] as User;
                up.User.LoadById(userin.Id);
                up.Projects.LoadbyUser(up.User);
                //up.Statuses.Load();
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
                /*
                if(up.User.Email == null)
                {
                    ModelState.AddModelError(string.Empty, "Email address is required");
                }

                else if (up.User.CheckIfEmailExists(uut.User.Email) == true)
                {
                    ModelState.AddModelError(string.Empty, "Email Already Exists");

                    // TODO:
                    // REDIRECT TO LOGIN SCREEN HERE?
                }

                if (uut.User.FirstName == null)
                {
                    ModelState.AddModelError(string.Empty, "First Name is required");
                }

                if (uut.User.LastName == null)
                {
                    ModelState.AddModelError(string.Empty, "Last Name is required");
                }

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
                     
                    return View(up);
                }


                up.User.Update();
                //needs to be moved to edit project action
                //up.Project.Update();
                //needs to be moved to edit portfolio action
                //up.Portfolio.Update();
                return RedirectToAction("Index", "Admin", new { returnurl = HttpContext.Request.Url });
            }
            catch
            {
                return View(up);
            }
        }


    }
}
