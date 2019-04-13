using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.MVCUI.ViewModels
{
    public class UserUserTypes
    {
        public User User { get; set; }
        public UserTypeList UserTypes { get; set; }
        public string ConfirmPassword { get; set; } //Used for create new account
        public HttpPostedFileBase Fileupload { get; set; } //Used for display profile picture
    }
}