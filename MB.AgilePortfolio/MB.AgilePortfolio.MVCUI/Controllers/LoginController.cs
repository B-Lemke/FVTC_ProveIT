using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;
using MB.AgilePortfolio.MVCUI.ViewModels;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Contents.Abandon();
            Session.Contents.RemoveAll();
            return View();
        }

        public ActionResult Login()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(User user, string returnurl)
        {
            try
            {
                if (user.Login())
                {
                    ViewBag.Message = "Login successful";
                    ViewBag.FullName = user.FullName;
                    Session["user"] = user;
                    //Set UserType on login in a session
                    Session["UserType"] = user.UserTypeDescription;

                    // If no return url supplied, use referrer url.
                    // Protect against endless loop by checking for empty referrer.
                    if (String.IsNullOrEmpty(returnurl)
                        && Request.UrlReferrer != null
                        && Request.UrlReferrer.ToString().Length > 0)
                    {
                        return RedirectToAction("Index", "UserProfile");
                    }

                    return Redirect(returnurl);
                }
                else
                {
                    ViewBag.Message = "Wrong credentials...";
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(user);
            }
        }

        //TODO THIS NEEDS TO GO TO LOGINyo
        public ActionResult EmailSent()
        {
            return View();
        }

        // GET: UserProfile/ForgotPassword
        public ActionResult ForgotPassword()
        {
            UserProfile lc = new UserProfile();

            
            return View(lc);

        }


        // POST: UserProfile/ForgotPassword
        [HttpPost]
        public ActionResult ForgotPassword(UserProfile lc)
        {
            try
            {
                lc.User = new User();
                Guid linkId = lc.User.ForgotPasswordKeyGen(lc.Email);


                lc.SendPasswordReset(lc.Email, "ProveIT Account Password Reset", "http://localhost:58140/ResetPassword/"+linkId.ToString());
                return RedirectToAction("EmailSent", "Login", new { returnurl = HttpContext.Request.Url });
            }
            catch { return View(lc); }

        }



        public ActionResult ResetPassword(Guid passwordResetId)
        {
            try
            {
                UserProfile up = new UserProfile();
                up.User = new User();
                up.User.LoadByForgottenPassRequestId(passwordResetId);

                if (up.User.Id != Guid.Empty)
                {

                    return View(up);
                }
                else
                {
                    //Invalid Id
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        // POST: UserProfile/ForgotPassword
        [HttpPost]
        public ActionResult ResetPassword(UserProfile lc)
        {
            try
            {
               if(lc.ConfirmPassword == lc.User.Password)
               {
                    lc.User.ChangeForgottenPassword(lc.User.Password);
                    //Password reset. TO DO, display the message here
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    return View(lc);
                }
            }
            catch { return View(lc); }

        }
    }
}
