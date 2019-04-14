using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.AgilePortfolio.PL;

namespace MB.AgilePortfolio.BL
{
    public class Portfolio
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription => Description == null ? "test" : new string(Description.Take(100).ToArray());
        public string Description { get; set; }
        [DisplayName("Portfolio Image")]
        public string PortfolioImage { get; set; }
        public Guid UserId { get; set; }
        [DisplayName("User")]
        public string UserEmail { get; set; }
        [DisplayName("Privacy")]
        public Guid PrivacyId { get; set; }
        [DisplayName("Privacy")]
        public string PrivacyDescription { get; set; }
        public ProjectList Projects { get; set; }
        public string CreatorUsername { get; set; }

        public Portfolio()
        {
            Projects = new ProjectList();
        }

        public Portfolio(Guid id, string name, string description, string portfolioImage, Guid userId, string email, string privacy, Guid privacyId)
        {
            Id = id;
            Name = name;
            Description = description;
            PortfolioImage = portfolioImage;
            UserId = userId;
            UserEmail = email;
            PrivacyDescription = privacy;
            PrivacyId = privacyId;
            Projects = new ProjectList();
        }

        public int Insert()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblPortfolio portfolio = new tblPortfolio()
                    {
                        Id = Guid.NewGuid(),
                        Name = Name,
                        Description = Description,
                        PortfolioImage = PortfolioImage,
                        PrivacyId = PrivacyId,
                        UserId = UserId

                    };
                    //Save the Id
                    this.Id = portfolio.Id;

                    dc.tblPortfolios.Add(portfolio);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// Insert project into portfolio
        /// </summary>
        /// <param name="projectId"> Id of Project to add to Portfolio object </param>
        /// <returns> bool for success status </returns>
        public bool AddProject(Guid projectId)
        {
            try
            {
                Project project = new Project();
                project.LoadById(projectId);
                Portfolio port = new Portfolio();
                ProjectList prjs = new ProjectList();
                prjs = port.LoadProjects(port.Id);
                foreach(Project prj in prjs)
                {
                    if(prj.Name == project.Name)
                    {
                        // Already exists in Portfolio
                        return false; //this should probably be a throw ex
                    }
                }

     
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblPortfolio portfolio = dc.tblPortfolios.Where(p => p.Id == Id).FirstOrDefault();
                    tblPortfolioProject portProj = new tblPortfolioProject()
                    {
                        Id = Guid.NewGuid(),
                        ProjectId = projectId,
                        PortfolioId = portfolio.Id
                    };
                    dc.tblPortfolioProjects.Add(portProj);
                    dc.SaveChanges();
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        /// Delete project from portfolio
        public void RemoveProject(Guid portProjId)
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

        /// <summary>
        /// Delete project from portfolio by passing projectid and portfolioid
        /// </summary>
        /// <param name="ProjectId"> The Id of Project to Remove</param>
        /// <param name="PortfolioId">The Id of Portfolio to remove Project from</param>
        public void RemoveProjectByPortProj(Guid ProjectId, Guid PortfolioId)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblPortfolioProject portProj = dc.tblPortfolioProjects.Where(pp => pp.ProjectId == ProjectId && pp.PortfolioId == PortfolioId).FirstOrDefault();
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

        // returns list of projects in given portfolio
        public ProjectList LoadProjects(Guid id)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var projects = (from pp in dc.tblPortfolioProjects
                                    join p in dc.tblProjects on pp.ProjectId equals p.Id
                                    join po in dc.tblPortfolios on pp.PortfolioId equals po.Id
                                    where pp.PortfolioId == id
                                    select new
                                    {
                                        pp.Id,
                                        pp.PortfolioId,
                                        pp.ProjectId,
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
                                        p.StatusId
                                    }).ToList();
                    foreach (var p in projects)
                    {
                        Project project = new Project(p.ProjectId, p.Name, p.Location, p.Filepath, p.PrivacyId, p.Image, p.Description, p.UserId, p.DateCreated, p.Purpose,
                            p.Environment, p.Challenges, p.FuturePlans, p.Collaborators, p.LastUpdated, p.SoftwareUsed, p.StatusId);
                        project.LoadLanguages();
                        Projects.Add(project);
                    }
                    return Projects;
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
                    tblPortfolio portfolio = dc.tblPortfolios.Where(p => p.Id == Id).FirstOrDefault();
                    if (portfolio != null)
                    {
                        dc.tblPortfolios.Remove(portfolio);
                        return dc.SaveChanges();
                    }
                    else throw new Exception("Portfolio not found");
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
                    tblPortfolio portfolio = dc.tblPortfolios.Where(p => p.Id == Id).FirstOrDefault();
                    if (portfolio != null)
                    {
                        portfolio.Name = Name;
                        portfolio.Description = Description;
                        portfolio.PrivacyId = PrivacyId;
                        portfolio.PortfolioImage = PortfolioImage;
                        portfolio.UserId = UserId;

                        return dc.SaveChanges();
                    }
                    else throw new Exception("Portfolio not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Load Portfolio by the Portfolio Id
        /// </summary>
        /// <param name="id"> The Id of the Portfolio </param>
        public void LoadById(Guid id)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var portfolio = (from p in dc.tblPortfolios
                                     join pr in dc.tblPrivacies on p.PrivacyId equals pr.Id
                                     join u in dc.tblUsers on p.UserId equals u.Id
                                where p.Id == id
                                select new
                                {
                                    p.Id,
                                    p.Name,
                                    p.Description,
                                    p.PortfolioImage,
                                    p.UserId,
                                    p.PrivacyId,
                                    Privacy = pr.Description,
                                    u.Email
                                }).FirstOrDefault();
                    if (portfolio != null)
                    {
                        Id = portfolio.Id;
                        Name = portfolio.Name;
                        Description = portfolio.Description;
                        PrivacyId = portfolio.PrivacyId;
                        PortfolioImage = portfolio.PortfolioImage;
                        UserId = portfolio.UserId;
                        UserEmail = portfolio.Email;
                        PrivacyDescription = portfolio.Privacy;
                    }
                    else throw new Exception("Portfolio not found");
                    //Load the Projects on this Portfolio
                    this.LoadProjectsInPortFolio();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Loads Projects in a Portfolio (should also load languages in Project)
        /// </summary>
        public void LoadProjectsInPortFolio()
        {
            //Load Projects for a portfolio with portfolio ojbects Id
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    //Instantiate the Projects list
                    this.Projects = new ProjectList();

                    var PortfolioProjects = (from pp in dc.tblPortfolioProjects
                                            join p in dc.tblProjects on pp.ProjectId equals p.Id
                                            where pp.PortfolioId == this.Id
                                            select new
                                            {
                                                p.Id
                                            }).ToList();

                    foreach (var project in PortfolioProjects)
                    {
                        //Instantiate Project
                        Project proj = new Project();
                        // Load Project (this should also populate languages in project then)
                        proj.LoadById(project.Id);
                        // Add Project to Projects
                        this.Projects.Add(proj);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    public class PortfolioList : List<Portfolio>
    {

        /// <summary>
        /// Loads PortfolioList by user object (THIS IS BEING TRANSISTIONED USE LoadByUserID INSTEAD!)
        /// </summary>
        /// <param name="user"> The User object </param>
        public void LoadbyUser(User user)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var portfolios = (from p in dc.tblPortfolios
                                      join pr in dc.tblPrivacies on p.PrivacyId equals pr.Id
                                      join u in dc.tblUsers on p.UserId equals u.Id
                                      where p.UserId == user.Id || user.Id == null
                                      select new
                                      {
                                          p.Id,
                                          p.Name,
                                          p.PrivacyId,
                                          p.Description,
                                          p.PortfolioImage,
                                          p.UserId,
                                          u.Email,
                                          Privacy = pr.Description
                                      }).OrderByDescending(p => p.Name).ToList();
                    foreach (var p in portfolios)
                    {
                        Portfolio portfolio = new Portfolio(p.Id, p.Name, p.Description, p.PortfolioImage, p.UserId, p.Email, p.Privacy, p.PrivacyId);
                        portfolio.LoadProjectsInPortFolio();
                        Add(portfolio);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Loads PortfolioList by User Id
        /// </summary>
        /// <param name="userId"> The User Id to load Portfolios for</param>
        public void LoadbyUserID(Guid userId)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var portfolios = (from p in dc.tblPortfolios
                                      join pr in dc.tblPrivacies on p.PrivacyId equals pr.Id
                                      join u in dc.tblUsers on p.UserId equals u.Id
                                      where p.UserId == userId || userId == null
                                      select new
                                      {
                                          p.Id,
                                          p.Name,
                                          p.PrivacyId,
                                          p.Description,
                                          p.PortfolioImage,
                                          p.UserId,
                                          u.Email,
                                          Privacy = pr.Description
                                      }).OrderByDescending(p => p.Name).ToList();
                    foreach (var p in portfolios)
                    {
                        Portfolio portfolio = new Portfolio(p.Id, p.Name, p.Description, p.PortfolioImage, p.UserId, p.Email, p.Privacy, p.PrivacyId);
                        portfolio.LoadProjectsInPortFolio();
                        Add(portfolio);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Loads PortfolioList by exact Name of Portfolio
        /// </summary>
        /// <param name="PortfolioName"> The Name of the Portfolio as string</param>
        public void LoadByPortfolioName(string PortfolioName)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var portfolios = (from p in dc.tblPortfolios
                                      join pr in dc.tblPrivacies on p.PrivacyId equals pr.Id
                                      join u in dc.tblUsers on p.UserId equals u.Id
                                      where p.Name == PortfolioName || PortfolioName == null
                                      select new
                                      {
                                          p.Id,
                                          p.Name,
                                          p.PrivacyId,
                                          p.Description,
                                          p.PortfolioImage,
                                          p.UserId,
                                          u.Email,
                                          Privacy = pr.Description
                                      }).OrderByDescending(p => p.Name).ToList();
                    foreach (var p in portfolios)
                    {
                        Portfolio portfolio = new Portfolio(p.Id, p.Name, p.Description, p.PortfolioImage, p.UserId, p.Email, p.Privacy, p.PrivacyId);
                        portfolio.LoadProjectsInPortFolio();
                        Add(portfolio);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Loads PortfolioList by partial Name of Portfolio
        /// </summary>
        /// <param name="PartialPortfolioName"> The Partial Name of the Portfolio as string</param>
        public void LoadByPartialPortfolioName(string PartialPortfolioName)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var portfolios = (from p in dc.tblPortfolios
                                      join pr in dc.tblPrivacies on p.PrivacyId equals pr.Id
                                      join u in dc.tblUsers on p.UserId equals u.Id
                                      where p.Name.Contains(PartialPortfolioName) || PartialPortfolioName == null
                                      select new
                                      {
                                          p.Id,
                                          p.Name,
                                          p.PrivacyId,
                                          p.Description,
                                          p.PortfolioImage,
                                          p.UserId,
                                          u.Email,
                                          Privacy = pr.Description
                                      }).OrderByDescending(p => p.Name).ToList();
                    foreach (var p in portfolios)
                    {
                        Portfolio portfolio = new Portfolio(p.Id, p.Name, p.Description, p.PortfolioImage, p.UserId, p.Email, p.Privacy, p.PrivacyId);
                        portfolio.LoadProjectsInPortFolio();
                        Add(portfolio);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void Load()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var portfolios = (from p in dc.tblPortfolios
                                      join pr in dc.tblPrivacies on p.PrivacyId equals pr.Id
                                      join u in dc.tblUsers on p.UserId equals u.Id
                                 //where p.UserId == id || id == null
                                 select new
                                 {
                                     p.Id,
                                     p.Name,
                                     p.Description,
                                     p.PortfolioImage,
                                     p.UserId,
                                     p.PrivacyId,
                                     Privacy = pr.Description,
                                     u.Email
                                 }).OrderByDescending(p => p.Name).ToList();
                    foreach (var p in portfolios)
                    {
                        Portfolio portfolio = new Portfolio(p.Id, p.Name, p.Description, p.PortfolioImage, p.UserId, p.Email, p.Privacy, p.PrivacyId);
                        portfolio.LoadProjectsInPortFolio();
                        Add(portfolio);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
