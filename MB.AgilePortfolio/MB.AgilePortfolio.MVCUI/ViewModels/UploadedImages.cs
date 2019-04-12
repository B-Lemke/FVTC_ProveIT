using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.MVCUI.ViewModels
{
    public class UploadedImages
    {
        public string FilePath { get; set; }
        public HttpPostedFileBase Fileupload { get; set; }
        public string ObjectType { get; set; }
        public string ObjectName { get; set; }
        public string UserName { get; set; }
        public UploadedImage UploadedImage { get; set; }
    }
}