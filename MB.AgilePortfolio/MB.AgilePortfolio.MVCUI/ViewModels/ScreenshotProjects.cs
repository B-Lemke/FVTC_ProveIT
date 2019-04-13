using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.MVCUI.ViewModels
{
    public class ScreenshotProjects
    {
        public Screenshot Screenshot { get; set; }
        public ProjectList Projects { get; set; }
        public Guid ProjectId { get; set; }
        public ScreenshotList ScreenshotList { get; set; }
        public HttpPostedFileBase Fileupload { get; set; }
        public Project Project { get; set; }
        public PrivacyList Privacies { get; set; }
        public Privacy Privacy { get; set; }
        public Status Status { get; set; }
        public StatusList Statuses { get; set; }
        public Language Language { get; set; }
        public LanguageList Languages { get; set; }
        public ProjectLanguageList ProjectLanguages { get; set; }
        public ProjectLanguage ProjectLanguage { get; set; }
        public string InputLanguages { get; set; }
        public User User { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdated { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
    }
}