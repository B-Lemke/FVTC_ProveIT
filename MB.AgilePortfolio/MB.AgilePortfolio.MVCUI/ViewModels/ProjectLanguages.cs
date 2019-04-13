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
    public class ProjectLanguages
    {
        public ProjectLanguageList projectLanguages { get; set; }
        public ProjectLanguage ProjectLanguage { get; set; }
        public Language Language { get; set; }
        public LanguageList Languages { get; set; }
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
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdated { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
    }
}