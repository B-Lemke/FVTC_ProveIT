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

        // Insert project into portfolio
        public void AddProject(Guid projectId)
        {
            try
            {
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
            }
            catch (Exception ex) { throw ex; }
        }

        // Delete project from portfolio
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
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }

    public class PortfolioList : List<Portfolio>
    {

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
                        Add(portfolio);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
