using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.MVCUI.ViewModels
{
    public class UserProfile
    {
        public Project Project { get; set; }
        public ProjectList Projects { get; set; }
        public PrivacyList Privacies { get; set; }
        public Portfolio Portfolio { get; set; }
        public PortfolioList Portfolios { get; set; }
        public User User { get; set; }
        public UserTypeList UserTypes { get; set; }
        public UserList Users { get; set; }
        public UserType UserType { get; set; }
        public StatusList Statuses { get; set; }
        public string ConfirmPassword { get; set; } //Used for EditProfilePassword
        [DisplayName("Current Password")]
        public string OldPassword { get; set; } //Used for EditProfilePassword\
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdated { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
        public HttpPostedFileBase Fileupload { get; set; }

        public void SendPasswordReset(string Email, string Subject, string ResetLink)
        {

            string html = "To reset your ProveIT account password click this link: <a href=\"" + ResetLink + "\">Reset Password</a><br/>If you did not attempt to reset your password please report this";
            html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + ResetLink + "<br/> If you did not attempt to reset your password please report this");
            
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("ProveITConfirmation@gmail.com", "ProveIT Support");
            msg.To.Add(new MailAddress(Email));
            msg.Subject = Subject;
            msg.Body = string.Format(html, msg.From.DisplayName, msg.From.Address, msg);
            msg.IsBodyHtml = true;


            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "ProveITConfirmation@gmail.com",
                    Password = "Pa$$word1"
                };
                smtp.Credentials = credential;
                //smtp.Host = "smtp-relay.gmail.com";
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }
        }


    }
}