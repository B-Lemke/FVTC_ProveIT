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
        public string ProjectName { get; set; }
        public string UserName { get; set; }

        public UploadedZip() { }

        public UploadedZip(string filepath, HttpPostedFileBase fileupload, string projectname, string username)
        {
            FilePath = filepath;
            Fileupload = fileupload;
            ProjectName = projectname;
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
             *          - ProjectName = The Name of the Object type 
             *              + IE: if ObjectType=Project then ProjectName=[NAME OF PROJECT]
             *              + IE: if ObjectType=Portfolio then then ProjectName=[NAME OF PORTFOLIO]
             *              + IE: if ObjectType=Profile then ProjectName will be set to "DisplayImage"
            */

            try
            {
                string SavePath = "";

                //Upload Zip Logic
                string fileName = "";

                //Default Zip FilePath for Project
                string DefaultFilePath = "Assets/Zipfiles/" + UserName;
                string DefaultFileName = "Default";
                string absolutepath = "";
                //Empty FilePath Check
                if (FilePath == string.Empty || FilePath == null)
                {
                    //Set empty filepath to Default zip
                    SavePath = "Assets/Zipfiles/UserProfiles";
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
                    if (Directory.Exists("~/Zipfiles/" + UserName))
                    {
                        // Project Folder exists Logic
                    }
                    else
                    {
                        // Create Project Folder
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Zipfiles/" + UserName));
                    }
                    SavePath = "Assets/Zipfiles/" + UserName;

                    // Check if UserName folder existings in Project folder
                    if (Directory.Exists("~/Assets/Zipfiles/" + UserName + "/" + "Projects"))
                    {
                        // UserName folder in Project folder Exists Logic
                    }
                    else
                    {
                        /* 
                         *  Project folder in UserName folder DOESN'T Exist
                         *       - Create Project Folder in UserName folder
                        */
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Assets/Zipfiles/" + UserName + "/" + "Projects"));
                    }
                    SavePath = "Assets/Zipfiles/" + "Projects" + "/" + UserName;

                    // Check if ProjectName folder existings in UserName folder
                    if (Directory.Exists("~/Assets/Zipfiles/" + UserName + "/" + "Projects" + "/" + ProjectName))
                    {
                        // ProjectName folder in ObjectType folder Exists Logic
                    }
                    else
                    {
                        /* 
                         *  ProjectName folder in ObjectType folder DOESNT Exists
                         *       - Create ProjectName Folder in UserName folder
                        */
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Assets/Zipfiles/" + UserName + "/" + "Projects" + "/" + ProjectName));
                    }
                    SavePath = "Assets/Zipfiles/" + UserName + "/" + "Projects" + "/" + ProjectName;
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteProjectUploadFolder(string UserName, string ProjectName)
        {
            /*      PURPOSE: Delete Entire Project upload folder
             *          - To be used when a project is deleted to remove all uploaded zip files for that project
             *      
             *      PARAMETERS REQUIRED TO BE PASSED: 
             *          - UserName = The username of the User to Delete all uploaded project zip files for
             *          - ProjectName = Name of the Project to delete folder of
             *      
             *      RETURNS: Bool for Deletion Success (false return means folder didnt exist)
             *      
             *      OTHER INFO: Only Projects hold zip files
            */

            try
            {
                // Folder exists
                if (Directory.Exists("~/Assets/Zipfiles/" + UserName + "/Projects/" + ProjectName))
                {
                    // Delete Folder
                    Directory.Delete("~/Assets/Zipfiles/" + UserName + "/Projects/" + ProjectName);
                    return true;
                }
                else
                {
                    // Folder doesnt exist
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
