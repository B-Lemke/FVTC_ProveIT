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
                // TODO: Add insert logic here
                //uut.User.Email
                //users = new UserList();
                //if(uut.User.Email == null || users.e)

                if (uut.User.Email == null)
                {
                    ModelState.AddModelError(string.Empty, "Email address is required");
                }

                else if (uut.User.CheckIfEmailExists(uut.User.Email) == true)
                {
                    ModelState.AddModelError(string.Empty, "Email Already Exists");
                }

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
                return View(uut);
            }
        }


    }
}
