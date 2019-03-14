using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.MVCUI.ViewModels
{
    public class ProjectPrivaciesUserStatuses
    {
        public Project Project { get; set; }
        public PrivacyList Privacies { get; set; }
        public Privacy Privacy { get; set; }
        public User User { get; set; }
        public UserList Users { get; set; }
        public StatusList Statuses { get; set; }
    }
}