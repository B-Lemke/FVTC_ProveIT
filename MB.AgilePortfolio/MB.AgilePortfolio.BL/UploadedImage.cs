using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MB.AgilePortfolio.PL;

namespace MB.AgilePortfolio.BL
{
    public class UploadedImage
    {
        public string FilePath { get; set; }
        public HttpPostedFileBase Fileupload { get; set; }
        public string ObjectType { get; set; }
        public string ObjectName { get; set; }
        public string UserName { get; set; }


        public UploadedImage() { }

        public UploadedImage(string filepath, HttpPostedFileBase fileupload, string objectType, string objectName, string username)
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
                    //Upload Project Display Image Logic
                    string fileName = "";

                    // Set Object type to Projects for filepath strings
                    ObjectType = "Projects";

                    //Default Image FilePath for Project
                    string DefaultFilePath = "Assets/Images/UserProfiles";
                    string DefaultFileName = "Default.png";
                    string absolutepath = "";
                    //Empty FilePath Check
                    if (FilePath == string.Empty || FilePath == null)
                    {
                        //Set empty filepath to Default image
                        SavePath = DefaultFilePath;
                        fileName = DefaultFileName;
                    }

                    //Null file upload check
                    if (Fileupload != null)
                    {
                        fileName = Path.GetFileName(Fileupload.FileName);
                        var fullPath = HttpContext.Current.Server.MapPath("~/" + FilePath);

                        // Check if existing filepath exists on server
                        if (System.IO.File.Exists(fullPath))
                        {
                            // Current fullPath of Project Exists Logic
                        }
                        else
                        {
                            // Set FilePath to default
                            FilePath = DefaultFilePath;
                        }

                        // Check for Existing Directory on server
                        if (Directory.Exists("~/Images/" + UserName))
                        {
                            // ObjectType Folder exists Logic
                        }
                        else
                        {
                            // Create ObjectType Folder
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Images/" + UserName));
                        }
                        SavePath = "Assets/Images/" + UserName;

                        // Check if UserName folder existings in ObjectType folder
                        if (Directory.Exists("~/Assets/Images/" + UserName + "/" + ObjectType))
                        {
                            // UserName folder in ObjectType folder Exists Logic
                        }
                        else
                        {
                            /* 
                             *  ObjectType folder in UserName folder DOESNT Exists
                             *       - Create ObjectType Folder in UserName folder
                            */
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Assets/Images/" + UserName + "/" + ObjectType));
                        }
                        SavePath = "Assets/Images/" + ObjectType + "/" + UserName;

                        // Check if ObjectName folder existings in UserName folder
                        if (Directory.Exists("~/Assets/Images/" + UserName + "/" + ObjectType + "/" + ObjectName))
                        {
                            // ObjectName folder in ObjectType folder Exists Logic
                        }
                        else
                        {
                            /* 
                             *  ObjectName folder in ObjectType folder DOESNT Exists
                             *       - Create ObjectName Folder in UserName folder
                            */
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Assets/Images/" + UserName + "/" + ObjectType + "/" + ObjectName));
                        }
                        SavePath = "Assets/Images/" + UserName + "/" + ObjectType + "/" + ObjectName;
                        fullPath = HttpContext.Current.Server.MapPath("~/" + SavePath + "/" + fileName);

                        // If File with same name exists delete it
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
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

                    //Default Image FilePath for Project
                    string DefaultFilePath = "Assets/Images/UserProfiles";
                    string DefaultFileName = "Default.png";
                    string absolutepath = "";
                    //Empty FilePath Check
                    if (FilePath == string.Empty || FilePath == null)
                    {
                        //Set empty filepath to Default image
                        SavePath = DefaultFilePath;
                        fileName = DefaultFileName;
                    }

                    //Null file upload check
                    if (Fileupload != null)
                    {
                        fileName = Path.GetFileName(Fileupload.FileName);
                        var fullPath = HttpContext.Current.Server.MapPath("~/" + FilePath);

                        // Check if existing filepath exists on server
                        if (System.IO.File.Exists(fullPath))
                        {
                            // Current fullPath of Project Exists Logic
                        }
                        else
                        {
                            // Set FilePath to default
                            FilePath = DefaultFilePath;
                        }

                        // Check for Existing Directory on server
                        if (Directory.Exists("~/Images/" + UserName))
                        {
                            // ObjectType Folder exists Logic
                        }
                        else
                        {
                            // Create ObjectType Folder
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Images/" + UserName));
                        }
                        SavePath = "Assets/Images/" + UserName;

                        // Check if UserName folder existings in ObjectType folder
                        if (Directory.Exists("~/Assets/Images/" + UserName + "/" + ObjectType))
                        {
                            // UserName folder in ObjectType folder Exists Logic
                        }
                        else
                        {
                            /* 
                             *  ObjectType folder in UserName folder DOESNT Exists
                             *       - Create ObjectType Folder in UserName folder
                            */
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Assets/Images/" + UserName + "/" + ObjectType));
                        }
                        SavePath = "Assets/Images/" + ObjectType + "/" + UserName;

                        // Check if ObjectName folder existings in UserName folder
                        if (Directory.Exists("~/Assets/Images/" + UserName + "/" + ObjectType + "/" + ObjectName))
                        {
                            // ObjectName folder in ObjectType folder Exists Logic
                        }
                        else
                        {
                            /* 
                             *  ObjectName folder in ObjectType folder DOESNT Exists
                             *       - Create ObjectName Folder in UserName folder
                            */
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Assets/Images/" + UserName + "/" + ObjectType + "/" + ObjectName));
                        }
                        SavePath = "Assets/Images/" + UserName + "/" + ObjectType + "/" + ObjectName;
                        fullPath = HttpContext.Current.Server.MapPath("~/" + SavePath + "/" + fileName);

                        // If File with same name exists delete it
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
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
                    //Upload Project Display Image Logic
                    string fileName = "";

                    // Set Object type to Projects for filepath strings
                    ObjectType = "Portfolios";

                    //Default Image FilePath for Portfolio
                    string DefaultFilePath = "Assets/Images/UserProfiles";
                    string DefaultFileName = "Default.png";
                    string absolutepath = "";
                    //Empty FilePath Check
                    if (FilePath == string.Empty || FilePath == null)
                    {
                        //Set empty filepath to Default image
                        SavePath = DefaultFilePath;
                        fileName = DefaultFileName;
                    }

                    //Null file upload check
                    if (Fileupload != null)
                    {
                        fileName = Path.GetFileName(Fileupload.FileName);
                        var fullPath = HttpContext.Current.Server.MapPath("~/" + FilePath);

                        // Check if existing filepath exists on server
                        if (System.IO.File.Exists(fullPath))
                        {
                            // Current fullPath of Project Exists Logic
                        }
                        else
                        {
                            // Set FilePath to default
                            FilePath = DefaultFilePath;
                        }

                        // Check for Existing Directory on server
                        if (Directory.Exists("~/Images/" + UserName))
                        {
                            // ObjectType Folder exists Logic
                        }
                        else
                        {
                            // Create ObjectType Folder
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Images/" + UserName));
                        }
                        SavePath = "Assets/Images/" + UserName;

                        // Check if UserName folder existings in ObjectType folder
                        if (Directory.Exists("~/Assets/Images/" + UserName + "/" + ObjectType))
                        {
                            // UserName folder in ObjectType folder Exists Logic
                        }
                        else
                        {
                            /* 
                             *  ObjectType folder in UserName folder DOESNT Exists
                             *       - Create ObjectType Folder in UserName folder
                            */
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Assets/Images/" + UserName + "/" + ObjectType));
                        }
                        SavePath = "Assets/Images/" + ObjectType + "/" + UserName;

                        // Check if ObjectName folder existings in UserName folder
                        if (Directory.Exists("~/Assets/Images/" + UserName + "/" + ObjectType + "/" + ObjectName))
                        {
                            // ObjectName folder in ObjectType folder Exists Logic
                        }
                        else
                        {
                            /* 
                             *  ObjectName folder in ObjectType folder DOESNT Exists
                             *       - Create ObjectName Folder in UserName folder
                            */
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Assets/Images/" + UserName + "/" + ObjectType + "/" + ObjectName));
                        }
                        SavePath = "Assets/Images/" + UserName + "/" + ObjectType + "/" + ObjectName;
                        fullPath = HttpContext.Current.Server.MapPath("~/" + SavePath + "/" + fileName);

                        // If File with same name exists delete it
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
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

        public bool Delete()
        {
            /*      PURPOSE: Delete any Uploaded Image and Deletes ObjectName Folder if there are no files in it
             *      
             *      PARAMETERS REQUIRED ON OBJECT: 
             *          - ObjectType
             *          - UserName
             *          - ObjectName (this can be null for Profile)
             *      
             *      RETURNS: Bool for Deletion Success (false return means file didnt exist)
             *      
             *      OTHER INFO: Doesnt do screenshots
            */

            try
            {
                // If File exists delete it
                if (System.IO.File.Exists(FilePath))
                {
                    // File Exists
                    System.IO.File.Delete(FilePath);
                    if (ObjectType == "Profile" || ObjectType == "Profiles")
                    {
                        ObjectName = "DisplayImage";
                    }

                    // If no files in objectname folder delete objectName folder
                    if (Directory.Exists("~/Assets/Images/" + UserName + "/" + ObjectType + "/" + ObjectName))
                    {
                        if ((Directory.GetFiles("~/Assets/Images/" + UserName + "/" + ObjectType + "/" + ObjectName)).Count() < 1)
                        {
                            System.IO.Directory.Delete("~/Assets/Images/" + UserName + "/" + ObjectType + "/" + ObjectName);
                        }
                    }

                    return true;
                }
                else
                {
                    // File Doesn't Exist
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteUserFolderByUserName(string UserName)
        {
            /*      PURPOSE: Delete Entire User upload folder
             *          - To be used when a user is deleted to remove all uploaded display images
             *      
             *      PARAMETERS REQUIRED TO BE PASSED: 
             *          - UserName = The username of the User to Delete all uploaded profile, portfolio and project images for
             *      
             *      RETURNS: Bool for Deletion Success (false return means folder didnt exist)
             *      
             *      OTHER INFO: Doesnt do screenshots
            */

            try
            {
                // If File exists delete it
                if (Directory.Exists("~/Assets/Images/" + UserName))
                {
                    // Username Folder Exists
                    System.IO.Directory.Delete("~/Assets/Images/" + UserName);
                    return true;
                }
                else
                {
                    // Username Folder Doesn't Exist
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


