using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace MB.AgilePortfolio.BL
{
    public class UploadedZip
    {
        public string FilePath { get; set; }
        public HttpPostedFileBase Fileupload { get; set; }
        public string ObjectType { get; set; }
        public string ObjectName { get; set; }
        public string UserName { get; set; }

        public UploadedZip() { }

        public UploadedZip(string filepath, HttpPostedFileBase fileupload, string objectType, string objectName, string username)
        {
            FilePath = filepath;
            Fileupload = fileupload;
            ObjectType = objectType;
            ObjectName = objectName;
            UserName = username;

        }

        public string Upload()
        {
            /*      PURPOSE: allow display images upload for anything that has only 1 display image
             *      
             *      RETURNS: database value for imagepath
             *          - IE: Assets/Images/Projects/[username]/[ProjectName]/[ImageNameAndExt]
             *
             *       OTHER INFO: 
             *          - This will not work with screenshots
             *          - This will delete any images with the same name located in the same location because:
             *              + A Project can only have 1 Display Image
             *              + A Portfolio can only have 1 Display Image
             *              + A Profile can only have 1 Display Image
             *              
             *       PARAMETERS FROM OBJECT:
             *          - UserName = The Username of the user uploading image
             *          - FilePath = Objects Current Image Path as saved in database
             *          - FileUpload = The File attempting to uploaded by a User
             *          - ObjectType = The type of object its uploading for (Project, Portfolio, Profile)
             *          - ObjectName = The Name of the Object type 
             *              + IE: if ObjectType=Project then ObjectName=[NAME OF PROJECT]
             *              + IE: if ObjectType=Portfolio then then ObjectName=[NAME OF PORTFOLIO]
             *              + IE: if ObjectType=Profile then ObjectName will be set to "DisplayImage"
            */

            try
            {
                string SavePath = "";

                if (ObjectType == "Project" || ObjectType == "Projects")
                {
                    //Upload Zip Logic
                    string fileName = "";

                    // Set Object type to Projects for filepath strings
                    ObjectType = "Projects";

                    //Default Zip FilePath for Project
                    string DefaultFilePath = "Assets/ZipFiles/" + UserName;
                    string DefaultFileName = "Default";
                    string absolutepath = "";
                    //Empty FilePath Check
                    if (FilePath == string.Empty || FilePath == null)
                    {
                        //Set empty filepath to Default zip
                        SavePath = "Assets/ZipFiles/UserProfiles";
                        fileName = DefaultFileName;
                        FilePath = SavePath + "/" + fileName;

                    }

                    //Null file upload check
                    if (Fileupload != null)
                    {
                        fileName = Path.GetFileName(Fileupload.FileName);
                        var fullPath = HttpContext.Current.Server.MapPath("~/" + FilePath);

                        // Check if existing filepath exists on server
                        if (File.Exists(fullPath))
                        {
                            // Current fullPath of Project Exists Logic
                        }
                        else
                        {
                            // Set FilePath to default
                            FilePath = DefaultFilePath;
                        }

                        // Check for Existing Directory on server
                        if (Directory.Exists("~/ZipFiles/" + UserName))
                        {
                            // ObjectType Folder exists Logic
                        }
                        else
                        {
                            // Create ObjectType Folder
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/ZipFiles/" + UserName));
                        }
                        SavePath = "Assets/ZipFiles/" + UserName;

                        // Check if UserName folder existings in ObjectType folder
                        if (Directory.Exists("~/Assets/ZipFiles/" + UserName + "/" + ObjectType))
                        {
                            // UserName folder in ObjectType folder Exists Logic
                        }
                        else
                        {
                            /* 
                             *  ObjectType folder in UserName folder DOESNT Exists
                             *       - Create ObjectType Folder in UserName folder
                            */
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Assets/ZipFiles/" + UserName + "/" + ObjectType));
                        }
                        SavePath = "Assets/ZipFiles/" + ObjectType + "/" + UserName;

                        // Check if ObjectName folder existings in UserName folder
                        if (Directory.Exists("~/Assets/ZipFiles/" + UserName + "/" + ObjectType + "/" + ObjectName))
                        {
                            // ObjectName folder in ObjectType folder Exists Logic
                        }
                        else
                        {
                            /* 
                             *  ObjectName folder in ObjectType folder DOESNT Exists
                             *       - Create ObjectName Folder in UserName folder
                            */
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Assets/ZipFiles/" + UserName + "/" + ObjectType + "/" + ObjectName));
                        }
                        SavePath = "Assets/ZipFiles/" + UserName + "/" + ObjectType + "/" + ObjectName;
                        fullPath = HttpContext.Current.Server.MapPath("~/" + SavePath + "/" + fileName);

                        // If File with same name exists delete it
                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }
                        absolutepath = SavePath + "/" + fileName;
                    }
                    else
                    {
                        // FileUpload is null
                        absolutepath = FilePath;
                    }

                    // Upload new file to server if not null
                    if (Fileupload != null)
                    {
                        Fileupload.SaveAs(HttpContext.Current.Server.MapPath("~/" + absolutepath));
                    }
                    return absolutepath;
                }
                else if (ObjectType == "Profile" || ObjectType == "Profiles")
                {
                    //Upload Project Display Image Logic
                    string fileName = "";
                    ObjectName = "ProfileImage";
                    // Set Object type to Projects for filepath strings
                    ObjectType = "Profiles";

                    //Default Zip FilePath for profile
                    string DefaultFilePath = "Assets/ZipFiles/" + UserName;
                    string DefaultFileName = "Default";
                    string absolutepath = "";
                    //Empty FilePath Check
                    if (FilePath == string.Empty || FilePath == null)
                    {
                        //Set empty filepath to Default image
                        SavePath = "Assets/ZipFiles/UserProfiles";
                        fileName = DefaultFileName;
                        FilePath = SavePath + "/" + fileName;
                    }

                    //Null file upload check
                    if (Fileupload != null)
                    {
                        fileName = Path.GetFileName(Fileupload.FileName);
                        var fullPath = HttpContext.Current.Server.MapPath("~/" + FilePath);

                        // Check if existing filepath exists on server
                        if (File.Exists(fullPath))
                        {
                            // Current fullPath of Project Exists Logic
                        }
                        else
                        {
                            // Set FilePath to default
                            FilePath = DefaultFilePath;
                        }

                        // Check for Existing Directory on server
                        if (Directory.Exists("~/ZipFiles/" + UserName))
                        {
                            // ObjectType Folder exists Logic
                        }
                        else
                        {
                            // Create ObjectType Folder
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/ZipFiles/" + UserName));
                        }
                        SavePath = "Assets/ZipFiles/" + UserName;

                        // Check if UserName folder existings in ObjectType folder
                        if (Directory.Exists("~/Assets/ZipFiles/" + UserName + "/" + ObjectType))
                        {
                            // UserName folder in ObjectType folder Exists Logic
                        }
                        else
                        {
                            /* 
                             *  ObjectType folder in UserName folder DOESNT Exists
                             *       - Create ObjectType Folder in UserName folder
                            */
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Assets/ZipFiles/" + UserName + "/" + ObjectType));
                        }
                        SavePath = "Assets/ZipFiles/" + ObjectType + "/" + UserName;

                        // Check if ObjectName folder existings in UserName folder
                        if (Directory.Exists("~/Assets/ZipFiles/" + UserName + "/" + ObjectType + "/" + ObjectName))
                        {
                            // ObjectName folder in ObjectType folder Exists Logic
                        }
                        else
                        {
                            /* 
                             *  ObjectName folder in ObjectType folder DOESNT Exists
                             *       - Create ObjectName Folder in UserName folder
                            */
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Assets/ZipFiles/" + UserName + "/" + ObjectType + "/" + ObjectName));
                        }
                        SavePath = "Assets/ZipFiles/" + UserName + "/" + ObjectType + "/" + ObjectName;
                        fullPath = HttpContext.Current.Server.MapPath("~/" + SavePath + "/" + fileName);

                        // If File with same name exists delete it
                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }
                        absolutepath = SavePath + "/" + fileName;
                    }
                    else
                    {
                        // FileUpload is null
                        absolutepath = FilePath;
                    }

                    // Upload new file to server if not null
                    if (Fileupload != null)
                    {
                        Fileupload.SaveAs(HttpContext.Current.Server.MapPath("~/" + absolutepath));
                    }
                    return absolutepath;
                }
                else if (ObjectType == "Portfolio" || ObjectType == "Portfolios")
                {
                    //Upload Project Zip Logic
                    string fileName = "";

                    // Set Object type to Projects for filepath strings
                    ObjectType = "Portfolios";

                    //Default Zip FilePath for Portfolio
                    string DefaultFilePath = "Assets/ZipFiles/" + UserName;
                    string DefaultFileName = "Default";
                    string absolutepath = "";
                    //Empty FilePath Check
                    if (FilePath == string.Empty || FilePath == null)
                    {
                        //Set empty filepath to Default image
                        SavePath = "Assets/ZipFiles/UserProfiles";
                        fileName = DefaultFileName;
                        FilePath = SavePath + "/" + fileName;
                    }

                    //Null file upload check
                    if (Fileupload != null)
                    {
                        fileName = Path.GetFileName(Fileupload.FileName);
                        var fullPath = HttpContext.Current.Server.MapPath("~/" + FilePath);

                        // Check if existing filepath exists on server
                        if (File.Exists(fullPath))
                        {
                            // Current fullPath of Project Exists Logic
                        }
                        else
                        {
                            // Set FilePath to default
                            FilePath = DefaultFilePath;
                        }

                        // Check for Existing Directory on server
                        if (Directory.Exists("~/ZipFiles/" + UserName))
                        {
                            // ObjectType Folder exists Logic
                        }
                        else
                        {
                            // Create ObjectType Folder
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/ZipFiles/" + UserName));
                        }
                        SavePath = "Assets/ZipFiles/" + UserName;

                        // Check if UserName folder existings in ObjectType folder
                        if (Directory.Exists("~/Assets/ZipFiles/" + UserName + "/" + ObjectType))
                        {
                            // UserName folder in ObjectType folder Exists Logic
                        }
                        else
                        {
                            /* 
                             *  ObjectType folder in UserName folder DOESNT Exists
                             *       - Create ObjectType Folder in UserName folder
                            */
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Assets/ZipFiles/" + UserName + "/" + ObjectType));
                        }
                        SavePath = "Assets/ZipFiles/" + ObjectType + "/" + UserName;

                        // Check if ObjectName folder existings in UserName folder
                        if (Directory.Exists("~/Assets/ZipFiles/" + UserName + "/" + ObjectType + "/" + ObjectName))
                        {
                            // ObjectName folder in ObjectType folder Exists Logic
                        }
                        else
                        {
                            /* 
                             *  ObjectName folder in ObjectType folder DOESNT Exists
                             *       - Create ObjectName Folder in UserName folder
                            */
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Assets/ZipFiles/" + UserName + "/" + ObjectType + "/" + ObjectName));
                        }
                        SavePath = "Assets/ZipFiles/" + UserName + "/" + ObjectType + "/" + ObjectName;
                        fullPath = HttpContext.Current.Server.MapPath("~/" + SavePath + "/" + fileName);

                        // If File with same name exists delete it
                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }
                        absolutepath = SavePath + "/" + fileName;
                    }
                    else
                    {
                        // FileUpload is null
                        absolutepath = FilePath;
                    }

                    // Upload new file to server if not null
                    if (Fileupload != null)
                    {
                        Fileupload.SaveAs(HttpContext.Current.Server.MapPath("~/" + absolutepath));
                    }
                    return absolutepath;
                }
                else
                {
                    // ObjectType not set to Profile(s), Portfolio(s), or Profile(s)
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
