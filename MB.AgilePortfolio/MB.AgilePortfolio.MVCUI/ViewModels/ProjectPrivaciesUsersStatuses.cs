using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.MVCUI.ViewModels
{
    public class ProjectPrivaciesUsersStatuses
    {
        public Project Project { get; set; }
        public PrivacyList Privacies { get; set; }
        public UserList Users { get; set; }
        public StatusList Statuses { get; set; }
    }
}