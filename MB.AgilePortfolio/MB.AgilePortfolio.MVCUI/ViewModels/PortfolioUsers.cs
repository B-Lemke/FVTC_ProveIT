using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.MVCUI.ViewModels
{
    public class PortfolioUsers
    {
        public Portfolio Portfolio { get; set; }
        public UserList Users { get; set; }
        public User User { get; set; }
        public PrivacyList Privacies { get; set; }
    }
}