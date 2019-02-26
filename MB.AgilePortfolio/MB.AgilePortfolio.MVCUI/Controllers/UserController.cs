using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;
using MB.AgilePortfolio.MVCUI.ViewModels;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class UserController : Controller
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
            uut.UserTypes.Load();
            return View(uut);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserUserTypes uut)
        {
            try
            {
                // TODO: Add insert logic here
                uut.User.Insert();
                return RedirectToAction("Index");
            }
            catch { return View(uut); }
        }

        // GET: User/Edit/5
        public ActionResult Edit(Guid id)
        {
            UserUserTypes uut = new UserUserTypes()
            {
                User = new User(),
                UserTypes = new UserTypeList()
            };
            uut.User.LoadById(id);
            uut.UserTypes.Load();
            return View(uut);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, UserUserTypes uut)
        {
            try
            {
                // TODO: Add update logic here
                uut.User.Update();
                return RedirectToAction("Index");
            }
            catch { return View(uut); }
        }

        // GET: User/Delete/5
        public ActionResult Delete(Guid id)
        {
            user = new User();
            user.LoadById(id);
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, User u)
        {
            try
            {
                // TODO: Add delete logic here
                u.Delete();
                return RedirectToAction("Index");
            }
            catch { return View(u); }
        }
    }
}
