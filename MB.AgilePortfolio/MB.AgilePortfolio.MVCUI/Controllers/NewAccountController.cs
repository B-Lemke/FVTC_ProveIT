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

        // GET: User/Create
        public ActionResult Index()
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
        public ActionResult Index(UserUserTypes uut)
        {
            try
            {
                UploadedImage ui = new UploadedImage();
                if(uut.User.Email == null)
                {
                    ModelState.AddModelError(string.Empty, "Email address is required");
                }

                else if (uut.User.CheckIfEmailExists(uut.User.Email) == true)
                {
                    ModelState.AddModelError(string.Empty, "Email Already Exists");
                }

                if (uut.User.CheckIfUsernameExists(uut.User.Username) != Guid.Empty)
                {
                    ModelState.AddModelError(string.Empty, "Username Already Exists");
                }
                else
                {
                    ui.FilePath = uut.User.ProfileImage;
                    ui.Fileupload = uut.Fileupload;
                    ui.UserName = uut.User.Username;
                    ui.ObjectType = "Profile";
                    ui.ObjectName = null;
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

                //Uploads image and returns string value entered in database for image
                string fp = ui.Upload();

                // fp will return null if no upload file was choosen else use upload file to save to database
                if (fp != null)
                {
                    uut.User.ProfileImage = fp;
                }
                else
                {
                    uut.User.ProfileImage = null;
                }


                //Success, insert and redirect to the login!
                uut.User.Insert();
                return RedirectToAction("Index", "Login");
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
