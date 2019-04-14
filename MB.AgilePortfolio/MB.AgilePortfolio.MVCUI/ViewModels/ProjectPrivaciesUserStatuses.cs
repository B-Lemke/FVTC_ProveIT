using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.MVCUI.ViewModels
{
    public class ProjectPrivaciesUserStatuses
    {
        public Project Project { get; set; }
        public ProjectList Projects { get; set; }
        public PrivacyList Privacies { get; set; }
        public User User { get; set; }
        public UserList Users { get; set; }
        public StatusList Statuses { get; set; }
        public UserTypeList UserTypes { get; set; }
        public Language Language { get; set; }
        public LanguageList Languages { get; set; }
        public ProjectLanguageList ProjectLanguages { get; set; }
        public ProjectLanguage ProjectLanguage { get; set; }
        public HttpPostedFileBase Fileupload { get; set; }
        public IList<string> SelectedLanguages { get; set; }
        public IList<SelectListItem> AvailableLanguages { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdated {get;set;}
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
    }
}