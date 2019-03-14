using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;
using MB.AgilePortfolio.MVCUI.ViewModels;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class NewAccountController : Controller
    {
        User user;
        UserList users;

        // GET: User
        public ActionResult Index()
        {
            users = new UserList();
            users.Load();
            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(Guid id)
        {
            user = new User();
            user.LoadById(id);
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            UserUserTypes uut = new UserUserTypes()
            {
                User = new User(),
                UserTypes = new UserTypeList()
            };
            uut.UserTypes.LoadNonAdmin();

            return View(uut);
        }


        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserUserTypes uut)
        {
            try
            {
                if(uut.User.Email == null)
                {
                    ModelState.AddModelError(string.Empty, "Email address is required");
                }

                else if (uut.User.CheckIfEmailExists(uut.User.Email) == true)
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

                if (!ModelState.IsValid)
                {
                    uut.UserTypes = new UserTypeList();
                    uut.UserTypes.LoadNonAdmin();
                    return View(uut);
                }


                uut.User.Insert();
                return RedirectToAction("Create");
            }
            catch
            {
                uut.UserTypes = new UserTypeList();
                uut.UserTypes.LoadNonAdmin();
                return View(uut);
            }
        }


    }
}
