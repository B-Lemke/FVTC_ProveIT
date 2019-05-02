using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.AgilePortfolio.PL;

namespace MB.AgilePortfolio.BL
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Link")]
        public string Location { get; set; }
        public string Filepath { get; set; }
        [DisplayName("Privacy")]
        public Guid PrivacyId { get; set; }
        [DisplayName("Privacy")]
        public string PrivacyDescription { get; set; }
        public string Image { get; set; }
        public string ShortDescription
        {
            get
            {
                if(Description == null)
                {
                    return String.Empty;
                }
                else if(Description.Length < 100)
                {
                    return Description;
                }
                else
                {
                    StringBuilder output = new StringBuilder();
                    int i = 100;
                    bool flag = true;
                    output.Append(Description.Substring(0, 100));
                    while (flag)
                    {
                        if(i <= 125)
                        {
                            if(Description[i] != ' ')
                            {
                                output.Append(Description[i]);
                            }
                            else
                            {
                                output.Append("...");
                                flag = !flag;
                            }
                        }
                        else
                        {
                            output.Append("...");
                            flag = !flag;
                        }
                        i++;
                    }
                    return output.ToString();
                }
            }
        }
  
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DisplayName("User")]
        public Guid UserId { get; set; }
        [DisplayName("User")]
        public string UserEmail { get; set; }
        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.MultilineText)]
        public string Purpose { get; set; }
        public string Environment { get; set; }
        [DataType(DataType.MultilineText)]
        public string Challenges { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Future Plans")]
        public string FuturePlans { get; set; }
        public string Collaborators { get; set; }
        [DisplayName("Last Updated")]
        public DateTime LastUpdated { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Software Used")]
        public string SoftwareUsed { get; set; }
        [DisplayName("Status")]
        public Guid StatusId { get; set; }
        [DisplayName("Status")]
        public string StatusDescription { get; set; }
        public LanguageList Languages { get; set; }
        public bool UsesDefaultImage { get; set; }
        public string CreatorUserName { get; set; }
        public string UrlFriendlyName { get { return URLFriendly(this.Name); } }
        public string UrlFriendlyCreatorUserName { get { return URLFriendly(this.CreatorUserName); } }


        /// <summary>
        /// Produces optional, URL-friendly version of a title, "like-this-one". 
        /// hand-tuned for speed, reflects performance refactoring contributed
        /// by John Gietzen (user otac0n) 
        /// </summary>
        private static string URLFriendly(string title)
        {
            if (title == null) return "";

            const int maxlen = 80;
            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }

        private static string URLFriendlyByUserID(Guid UserID)
        {
            User user = new User();
            user.LoadById(UserID);
            string title = user.Username;


            if (title == null) return "";

            const int maxlen = 80;
            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }

        private static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();
            if ("àåáâäãåą".Contains(s))
            {
                return "a";
            }
            else if ("èéêëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'Þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else
            {
                return "";
            }
        }

        public Project() { }

        public Project(Guid id, string name, string location, string filepath, Guid privacyId, string image, string description, Guid userId, DateTime dateCreated,
                       string purpose, string environment, string challenges, string futurePlans, string collaborators, DateTime lastUpdated, string softwareUsed, Guid statusId)
        {
            Id = id;
            Name = name;
            Location = location;
            Filepath = filepath;
            PrivacyId = privacyId;
            Image = image;
            Description = description;
            UserId = userId;
            DateCreated = dateCreated;
            Purpose = purpose;
            Environment = environment;
            Challenges = challenges;
            FuturePlans = futurePlans;
            Collaborators = collaborators;
            LastUpdated = lastUpdated;
            SoftwareUsed = softwareUsed;
            StatusId = statusId;
        }

        public Project(Guid id, string name, string location, string filepath, Guid privacyId, string image, string description, Guid userId, DateTime dateCreated,
                       string purpose, string environment, string challenges, string futurePlans, string collaborators, DateTime lastUpdated, string softwareUsed, Guid statusId, string privacy, string status, string email)
        {
            Id = id;
            Name = name;
            Location = location;
            Filepath = filepath;
            PrivacyId = privacyId;
            Image = image;
            Description = description;
            UserId = userId;
            DateCreated = dateCreated;
            Purpose = purpose;
            Environment = environment;
            Challenges = challenges;
            FuturePlans = futurePlans;
            Collaborators = collaborators;
            LastUpdated = lastUpdated;
            SoftwareUsed = softwareUsed;
            StatusId = statusId;
            PrivacyDescription = privacy;
            StatusDescription = status;
            UserEmail = email;
        }

        // Old insert
        public int Insert()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblProject project = new tblProject()
                    {
                        Id = Guid.NewGuid(),
                        Name = Name,
                        Location = Location,
                        Filepath = Filepath,
                        PrivacyId = PrivacyId,
                        Image = Image,
                        Description = Description,
                        UserId = UserId,
                        DateCreated = DateCreated,
                        Purpose = Purpose,
                        Environment = Environment,
                        Challenges = Challenges,
                        FuturePlans = FuturePlans,
                        Collaborators = Collaborators,
                        LastUpdated = LastUpdated,
                        SoftwareUsed = SoftwareUsed,
                        StatusId = StatusId
                    };
                    //Save the Id
                    this.Id = project.Id;

                    dc.tblProjects.Add(project);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void Insert(IList<string> SelectedLanguages)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblProject project = new tblProject()
                    {
                        Id = Guid.NewGuid(),
                        Name = Name,
                        Location = Location,
                        Filepath = Filepath,
                        PrivacyId = PrivacyId,
                        Image = Image,
                        Description = Description,
                        UserId = UserId,
                        DateCreated = DateCreated,
                        Purpose = Purpose,
                        Environment = Environment,
                        Challenges = Challenges,
                        FuturePlans = FuturePlans,
                        Collaborators = Collaborators,
                        LastUpdated = LastUpdated,
                        SoftwareUsed = SoftwareUsed,
                        StatusId = StatusId
                    };
                    //Save the Id
                    this.Id = project.Id;

                    dc.tblProjects.Add(project);
                    dc.SaveChanges();
                    foreach (var pl in SelectedLanguages)
                    {
                        ProjectLanguage projlang = new ProjectLanguage();
                        projlang.ProjectId = project.Id;
                        projlang.LanguageId = (Guid.Parse(pl));
                        projlang.Insert();
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        // Inserts project without portfolio
        public void Insert(Guid userId)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblProject project = new tblProject()
                    {
                        Id = Guid.NewGuid(),
                        Name = Name,
                        Location = Location,
                        Filepath = Filepath,
                        PrivacyId = PrivacyId,
                        Image = Image,
                        Description = Description,
                        UserId = userId,
                        DateCreated = DateCreated,
                        Purpose = Purpose,
                        Environment = Environment,
                        Challenges = Challenges,
                        FuturePlans = FuturePlans,
                        Collaborators = Collaborators,
                        LastUpdated = LastUpdated,
                        SoftwareUsed = SoftwareUsed,
                        StatusId = StatusId
                    };
                    dc.tblProjects.Add(project);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        // Insert project with portfolio
        public void Insert(Guid userId, Guid portfolioId)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblProject project = new tblProject()
                    {
                        Id = Guid.NewGuid(),
                        Name = Name,
                        Location = Location,
                        Filepath = Filepath,
                        PrivacyId = PrivacyId,
                        Image = Image,
                        Description = Description,
                        UserId = userId,
                        DateCreated = DateCreated,
                        Purpose = Purpose,
                        Environment = Environment,
                        Challenges = Challenges,
                        FuturePlans = FuturePlans,
                        Collaborators = Collaborators,
                        LastUpdated = LastUpdated,
                        SoftwareUsed = SoftwareUsed,
                        StatusId = StatusId
                    };
                    dc.tblProjects.Add(project);
                    tblPortfolioProject portProj = new tblPortfolioProject()
                    {
                        Id = Guid.NewGuid(),
                        ProjectId = project.Id,
                        PortfolioId = portfolioId
                    };
                    dc.tblPortfolioProjects.Add(portProj);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        // Adds project to porfolio
        public void AddToPortfolio(Guid portfolioId)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblProject project = dc.tblProjects.Where(p => p.Id == Id).FirstOrDefault();
                    tblPortfolioProject portProj = new tblPortfolioProject()
                    {
                        Id = Guid.NewGuid(),
                        PortfolioId = portfolioId,
                        ProjectId = project.Id
                    };
                    dc.tblPortfolioProjects.Add(portProj);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        // Deletes project from portfolio
        public void DeleteFromPortfolio(Guid portProjId)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblPortfolioProject portProj = dc.tblPortfolioProjects.Where(pp => pp.Id == portProjId).FirstOrDefault();
                    if (portProj != null)
                    {
                        dc.tblPortfolioProjects.Remove(portProj);
                        dc.SaveChanges();
                    }
                    else throw new Exception("Project not found in portfolio");
                }
            }
            catch (Exception ex) { throw ex; }
        }


        public int Delete()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblProject project = dc.tblProjects.Where(p => p.Id == Id).FirstOrDefault();
                    if (project != null)
                    {
                        ProjectLanguageList pll = new ProjectLanguageList();
                        pll.LoadByProjectId(project.Id);
                        foreach (ProjectLanguage pl in pll)
                        {
                            pl.Delete();
                        }
                        dc.tblProjects.Remove(project);
                        return dc.SaveChanges();
                    }
                    else throw new Exception("Project not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public int Update()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblProject project = dc.tblProjects.Where(p => p.Id == Id).FirstOrDefault();
                    if (project != null)
                    {
                        project.Name = Name;
                        project.Location = Location;
                        project.Filepath = Filepath;
                        project.PrivacyId = PrivacyId;
                        project.Image = Image;
                        project.Description = Description;
                        project.UserId = UserId;
                        project.DateCreated = DateCreated;
                        project.Purpose = Purpose;
                        project.Environment = Environment;
                        project.Challenges = Challenges;
                        project.FuturePlans = FuturePlans;
                        project.Collaborators = Collaborators;
                        project.LastUpdated = LastUpdated;
                        project.SoftwareUsed = SoftwareUsed;
                        project.StatusId = StatusId;

                        return dc.SaveChanges();
                    }
                    else throw new Exception("Project not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void LoadLanguages()
        {
            //Load languages for a project with this Id
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    //Instantiate the language list
                    this.Languages = new LanguageList();

                    var ProjectLanguages = (from pl in dc.tblProjectLanguages
                                            join l in dc.tblLanguages on pl.LanguageId equals l.Id
                                            where pl.ProjectId == this.Id
                                            select new
                                            {
                                                l.Id,
                                                l.Description
                                            }).ToList();

                    foreach(var language in ProjectLanguages)
                    {
                        Language lang = new Language();
                        lang.Id = language.Id;
                        lang.Description = language.Description;
                        this.Languages.Add(lang);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void LoadById(Guid id)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var project = (from p in dc.tblProjects
                                join pr in dc.tblPrivacies on p.PrivacyId equals pr.Id
                                join u in dc.tblUsers on p.UserId equals u.Id
                                join s in dc.tblStatuses on p.StatusId equals s.Id
                                where p.Id == id
                                select new
                                {
                                    p.Id,
                                    p.Name,
                                    p.Location,
                                    p.Filepath,
                                    p.PrivacyId,
                                    p.Image,
                                    p.Description,
                                    p.UserId,
                                    p.DateCreated,
                                    p.Purpose,
                                    p.Environment,
                                    p.Challenges,
                                    p.FuturePlans,
                                    p.Collaborators,
                                    p.LastUpdated,
                                    p.SoftwareUsed,
                                    p.StatusId,
                                    Privacy = pr.Description,
                                    Status = s.Description,
                                    UserEmail = u.Email
                                }).FirstOrDefault();
                    if (project != null)
                    {
                        Id = project.Id;
                        Name = project.Name;
                        Location = project.Location;
                        Filepath = project.Filepath;
                        PrivacyId = project.PrivacyId;
                        Image = project.Image;
                        Description = project.Description;
                        UserId = project.UserId;
                        DateCreated = project.DateCreated;
                        Purpose = project.Purpose;
                        Environment = project.Environment;
                        Challenges = project.Challenges;
                        FuturePlans = project.FuturePlans;
                        Collaborators = project.Collaborators;
                        LastUpdated = project.LastUpdated;
                        SoftwareUsed = project.SoftwareUsed;
                        StatusId = project.StatusId;
                        UserEmail = project.UserEmail;
                        StatusDescription = project.Status;
                        PrivacyDescription = project.Privacy;
                    }
                    else throw new Exception("Project not found");

                    //Load the langauges on this project
                    this.LoadLanguages();
                }
            }
            catch (Exception ex) { throw ex; }
        }

    }

    public class ProjectList : List<Project>
    {
        public void Load()
        {
            try
            {
                Load(null);
            }
            catch (Exception ex) { throw ex; }
        }

        public void LoadbyUser(User user)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var projects = (from p in dc.tblProjects
                                    join pr in dc.tblPrivacies on p.PrivacyId equals pr.Id
                                    join u in dc.tblUsers on p.UserId equals u.Id
                                    join s in dc.tblStatuses on p.StatusId equals s.Id
                                    where p.UserId == user.Id || user.Id == null
                                    select new
                                    {
                                        p.Id,
                                        p.Name,
                                        p.Location,
                                        p.Filepath,
                                        p.PrivacyId,
                                        p.Image,
                                        p.Description,
                                        p.UserId,
                                        p.DateCreated,
                                        p.Purpose,
                                        p.Environment,
                                        p.Challenges,
                                        p.FuturePlans,
                                        p.Collaborators,
                                        p.LastUpdated,
                                        p.SoftwareUsed,
                                        p.StatusId,
                                        Privacy = pr.Description,
                                        Status = s.Description,
                                        UserEmail = u.Email
                                    }).OrderByDescending(p => p.LastUpdated).ToList();
                    foreach (var p in projects)
                    {
                        Project project = new Project(p.Id, p.Name, p.Location, p.Filepath, p.PrivacyId, p.Image, p.Description, p.UserId, p.DateCreated, p.Purpose,
                                                      p.Environment, p.Challenges, p.FuturePlans, p.Collaborators, p.LastUpdated, p.SoftwareUsed, p.StatusId, p.Privacy, p.Status, p.UserEmail);
                        project.LoadLanguages();
                        Add(project);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void LoadbyUserID(Guid userId)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var projects = (from p in dc.tblProjects
                                    join pr in dc.tblPrivacies on p.PrivacyId equals pr.Id
                                    join u in dc.tblUsers on p.UserId equals u.Id
                                    join s in dc.tblStatuses on p.StatusId equals s.Id
                                    where p.UserId == userId || userId == null
                                    select new
                                    {
                                        p.Id,
                                        p.Name,
                                        p.Location,
                                        p.Filepath,
                                        p.PrivacyId,
                                        p.Image,
                                        p.Description,
                                        p.UserId,
                                        p.DateCreated,
                                        p.Purpose,
                                        p.Environment,
                                        p.Challenges,
                                        p.FuturePlans,
                                        p.Collaborators,
                                        p.LastUpdated,
                                        p.SoftwareUsed,
                                        p.StatusId,
                                        Privacy = pr.Description,
                                        Status = s.Description,
                                        UserEmail = u.Email
                                    }).OrderByDescending(p => p.LastUpdated).ToList();
                    foreach (var p in projects)
                    {
                        Project project = new Project(p.Id, p.Name, p.Location, p.Filepath, p.PrivacyId, p.Image, p.Description, p.UserId, p.DateCreated, p.Purpose,
                                                      p.Environment, p.Challenges, p.FuturePlans, p.Collaborators, p.LastUpdated, p.SoftwareUsed, p.StatusId, p.Privacy, p.Status, p.UserEmail);
                        project.LoadLanguages();
                        Add(project);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Loads ProjectList by the exact name of project
        /// </summary>
        /// <param name="projectName"> The exact name of project as string </param>
        public void LoadByProjectName(string projectName)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var projects = (from p in dc.tblProjects
                                    join pr in dc.tblPrivacies on p.PrivacyId equals pr.Id
                                    join u in dc.tblUsers on p.UserId equals u.Id
                                    join s in dc.tblStatuses on p.StatusId equals s.Id
                                    where p.Name == projectName || projectName == null
                                    select new
                                    {
                                        p.Id,
                                        p.Name,
                                        p.Location,
                                        p.Filepath,
                                        p.PrivacyId,
                                        p.Image,
                                        p.Description,
                                        p.UserId,
                                        p.DateCreated,
                                        p.Purpose,
                                        p.Environment,
                                        p.Challenges,
                                        p.FuturePlans,
                                        p.Collaborators,
                                        p.LastUpdated,
                                        p.SoftwareUsed,
                                        p.StatusId,
                                        Privacy = pr.Description,
                                        Status = s.Description,
                                        UserEmail = u.Email
                                    }).OrderByDescending(p => p.LastUpdated).ToList();
                    foreach (var p in projects)
                    {
                        Project project = new Project(p.Id, p.Name, p.Location, p.Filepath, p.PrivacyId, p.Image, p.Description, p.UserId, p.DateCreated, p.Purpose,
                                                      p.Environment, p.Challenges, p.FuturePlans, p.Collaborators, p.LastUpdated, p.SoftwareUsed, p.StatusId, p.Privacy, p.Status, p.UserEmail);
                        project.LoadLanguages();
                        Add(project);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Loads ProjectList by a partial name of project
        /// </summary>
        /// <param name="PartialProjectName"> The partial name of project as string </param>
        public void LoadByPartialProjectName(string PartialProjectName)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var projects = (from p in dc.tblProjects
                                    join pr in dc.tblPrivacies on p.PrivacyId equals pr.Id
                                    join u in dc.tblUsers on p.UserId equals u.Id
                                    join s in dc.tblStatuses on p.StatusId equals s.Id
                                    where p.Name.Contains(PartialProjectName) || PartialProjectName == null
                                    select new
                                    {
                                        p.Id,
                                        p.Name,
                                        p.Location,
                                        p.Filepath,
                                        p.PrivacyId,
                                        p.Image,
                                        p.Description,
                                        p.UserId,
                                        p.DateCreated,
                                        p.Purpose,
                                        p.Environment,
                                        p.Challenges,
                                        p.FuturePlans,
                                        p.Collaborators,
                                        p.LastUpdated,
                                        p.SoftwareUsed,
                                        p.StatusId,
                                        Privacy = pr.Description,
                                        Status = s.Description,
                                        UserEmail = u.Email
                                    }).OrderByDescending(p => p.LastUpdated).ToList();
                    foreach (var p in projects)
                    {
                        Project project = new Project(p.Id, p.Name, p.Location, p.Filepath, p.PrivacyId, p.Image, p.Description, p.UserId, p.DateCreated, p.Purpose,
                                                      p.Environment, p.Challenges, p.FuturePlans, p.Collaborators, p.LastUpdated, p.SoftwareUsed, p.StatusId, p.Privacy, p.Status, p.UserEmail);
                        project.LoadLanguages();
                        Add(project);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void LoadbyPortfolioID(Guid id)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var projects = (from p in dc.tblProjects
                                    join pr in dc.tblPrivacies on p.PrivacyId equals pr.Id
                                    join u in dc.tblUsers on p.UserId equals u.Id
                                    join pp in dc.tblPortfolioProjects on p.Id equals pp.ProjectId
                                    join pf in dc.tblPortfolios on pp.PortfolioId equals pf.Id
                                    join s in dc.tblStatuses on p.StatusId equals s.Id
                                    where pf.Id == id || pf.Id == null
                                    select new
                                    {
                                        p.Id,
                                        p.Name,
                                        p.Location,
                                        p.Filepath,
                                        p.PrivacyId,
                                        p.Image,
                                        p.Description,
                                        p.UserId,
                                        p.DateCreated,
                                        p.Purpose,
                                        p.Environment,
                                        p.Challenges,
                                        p.FuturePlans,
                                        p.Collaborators,
                                        p.LastUpdated,
                                        p.SoftwareUsed,
                                        p.StatusId,
                                        Privacy = pr.Description,
                                        Status = s.Description,
                                        UserEmail = u.Email
                                    }).OrderByDescending(p => p.LastUpdated).ToList();
                    foreach (var p in projects)
                    {
                        Project project = new Project(p.Id, p.Name, p.Location, p.Filepath, p.PrivacyId, p.Image, p.Description, p.UserId, p.DateCreated, p.Purpose,
                                                      p.Environment, p.Challenges, p.FuturePlans, p.Collaborators, p.LastUpdated, p.SoftwareUsed, p.StatusId, p.Privacy, p.Status, p.UserEmail);
                        //Load languages on the project
                        project.LoadLanguages();
                        Add(project);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void Load(Guid? id)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var projects = (from p in dc.tblProjects
                                    join pr in dc.tblPrivacies on p.PrivacyId equals pr.Id
                                    join u in dc.tblUsers on p.UserId equals u.Id
                                    join s in dc.tblStatuses on p.StatusId equals s.Id
                                    where p.UserId == id || id == null
                                 select new
                                 {
                                     p.Id,
                                     p.Name,
                                     p.Location,
                                     p.Filepath,
                                     p.PrivacyId,
                                     p.Image,
                                     p.Description,
                                     p.UserId,
                                     p.DateCreated,
                                     p.Purpose,
                                     p.Environment,
                                     p.Challenges,
                                     p.FuturePlans,
                                     p.Collaborators,
                                     p.LastUpdated,
                                     p.SoftwareUsed,
                                     p.StatusId,
                                     Privacy = pr.Description,
                                     Status = s.Description,
                                     UserEmail = u.Email
                                 }).OrderByDescending(p => p.LastUpdated).ToList();
                    foreach (var p in projects)
                    {
                        Project project = new Project(p.Id, p.Name, p.Location, p.Filepath, p.PrivacyId, p.Image, p.Description, p.UserId, p.DateCreated, p.Purpose,
                                                      p.Environment, p.Challenges, p.FuturePlans, p.Collaborators, p.LastUpdated, p.SoftwareUsed, p.StatusId, p.Privacy, p.Status, p.UserEmail);
                        //Load languages on the project
                        project.LoadLanguages();
                        Add(project);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }


    }
}
