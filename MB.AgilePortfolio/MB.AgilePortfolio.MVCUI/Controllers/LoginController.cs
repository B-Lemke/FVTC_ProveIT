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

        public ActionResult Index()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public ActionResult Index(User user, string returnurl)
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
                        if (Session["UserType"].ToString() == "User")
                            return RedirectToAction("Index", "UserProfile");
                        if (Session["UserType"].ToString() == "Employer")
                            return RedirectToAction("EmployerIndex", "UserProfile");
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


                lc.SendPasswordReset(lc.Email, "ProveIT Account Password Reset", "http://proveit.azurewebsites.net/ResetPassword/" + linkId.ToString());
                return RedirectToAction("EmailSent", "Login", new { returnurl = HttpContext.Request.Url });
            }
            catch { return View(lc); }

        }



        public ActionResult ResetPassword(Guid? passwordResetId)
        {
            Guid ID = passwordResetId.GetValueOrDefault();
            if (ID == Guid.Empty)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {
                UserProfile up = new UserProfile();
                up.User = new User();
                up.User.LoadByForgottenPassRequestId(ID);

                if (up.User.Id != Guid.Empty)
                {

                    return View(up);
                }
                else
                {
                    //Invalid Id
                    ViewBag.Message = "Reset Link was Invalid";
                    return RedirectToAction("ResetPasswordInvalid", "Login");
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
                ForgotPassword fg = new ForgotPassword();
                fg.LoadByUserId(lc.User.Id);

                if (fg != null)
                {
                    if (DateTime.Now > fg.ExpirationDate)
                    {
                        ViewBag.Message = "Reset Link has Expired";
                        fg.Delete();
                        return View(lc);
                    }
                    else
                    {
                        if (lc.User.Password == null)
                        {
                            ModelState.AddModelError(string.Empty, "Password is required");
                        }

                        if (lc.User.Password == null)
                        {
                            ModelState.AddModelError(string.Empty, "Password is required");
                        }

                        else if (lc.User.Password.Length < 6)
                        {
                            ModelState.AddModelError(string.Empty, "Password needs to be at least 6 characters");
                        }

                        else if (lc.User.Password.Length > 16)
                        {
                            ModelState.AddModelError(string.Empty, "Password needs to be less than 16 characters");
                        }

                        else if (lc.ConfirmPassword != lc.User.Password)
                        {
                            ModelState.AddModelError(string.Empty, "Passwords did not match");
                        }

                        if (!ModelState.IsValid)
                        {
                            return View(lc);
                        }
                        else
                        {
                            if (lc.ConfirmPassword == lc.User.Password)
                            {
                                lc.User.ChangeForgottenPassword(lc.User.Password);

                                //DELETE ON SUCCESSFUL USE
                                fg.Delete();

                                return RedirectToAction("ResetPasswordSuccess", "Login");
                            }
                            else
                            {
                                ViewBag.Message = "Passwords need to match";
                                return View(lc);
                            }
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "Reset Link Doesn't Exist";
                    return View(lc);
                }


            }
            catch
            {
                ViewBag.Message = "Error Occured While Attempted to Reset Password";
                return View(lc);
            }

        }

        public ActionResult ResetPasswordSuccess()
        {
            try
            {
                UserProfile up = new UserProfile();
                return View(up);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult ResetPasswordInvalid()
        {
            try
            {
                UserProfile up = new UserProfile();
                return View(up);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
