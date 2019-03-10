using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.MVCUI.ViewModels
{
    public class PortfolioProjectViewModel
    {
        public PortfolioProject PortfolioProject { get; set; }
        public PortfolioList Portfolios { get; set; }
        public ProjectList Projects { get; set; }
    }
}