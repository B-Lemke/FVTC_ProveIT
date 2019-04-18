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
    public class PortfolioProjectsUsersPrivacies
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
        [DataType(DataType.Date)]
        public HttpPostedFileBase Fileupload { get; set; }

    }
}