using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string Location { get; set; }
        public string Filepath { get; set; }
        [DisplayName("Privacy")]
        public Guid PrivacyId { get; set; }
        [DisplayName("Privacy")]
        public string PrivacyDescription { get; set; }
        public string Image { get; set; }
        public string ShortDescription => Description == null ? string.Empty : new string(Description.Take(100).ToArray());
        public string Description { get; set; }
        [DisplayName("User")]
        public Guid UserId { get; set; }
        [DisplayName("User")]
        public string UserEmail { get; set; }
        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }
        public string Purpose { get; set; }
        public string Environment { get; set; }
        public string Challenges { get; set; }
        [DisplayName("Future Plans")]
        public string FuturePlans { get; set; }
        public string Collaborators { get; set; }
        [DisplayName("Last Updated")]
        public DateTime LastUpdated { get; set; }
        [DisplayName("Software Used")]
        public string SoftwareUsed { get; set; }
        [DisplayName("Status")]
        public Guid StatusId { get; set; }
        [DisplayName("Status")]
        public string StatusDescription { get; set; }
        public LanguageList Languages { get; set; }

        public string CreatorUserName { get; set; }

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
