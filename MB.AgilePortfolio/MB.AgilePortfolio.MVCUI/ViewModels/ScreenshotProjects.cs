using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.MVCUI.ViewModels
{
    public class ScreenshotProjects
    {
        public Screenshot Screenshot { get; set; }
        public ProjectList Projects { get; set; }

        [DisplayName("Project Name")]
        public string projectName { get; set; }
    }
}