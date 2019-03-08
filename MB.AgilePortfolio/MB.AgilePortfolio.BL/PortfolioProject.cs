using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.AgilePortfolio.PL;

namespace MB.AgilePortfolio.BL
{
    public class PortfolioProject
    {
        public Guid Id { get; set; }
        public Guid PortfolioId { get; set; }
        public Guid ProjectId { get; set; }

        public PortfolioProject() { }
        
        public PortfolioProject(Guid id, Guid portfolioId, Guid projectId)
        {
            Id = id;
            PortfolioId = portfolioId;
            ProjectId = projectId;
        }

        public int Insert()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblPortfolioProject portfolioProject = new tblPortfolioProject()
                    {
                        Id = Guid.NewGuid(),
                        PortfolioId = PortfolioId,
                        ProjectId = ProjectId
                    };
                    //Save the Id
                    this.Id = portfolioProject.Id;

                    dc.tblPortfolioProjects.Add(portfolioProject);
                    return dc.SaveChanges();
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
                    tblPortfolioProject portfolioProject = dc.tblPortfolioProjects.Where(pp => pp.Id == Id).FirstOrDefault();
                    if (portfolioProject != null)
                    {
                        dc.tblPortfolioProjects.Remove(portfolioProject);
                        return dc.SaveChanges();
                    }
                    else throw new Exception("Portfolio Project not found");
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
                    tblPortfolioProject portfolioProject = dc.tblPortfolioProjects.Where(pp => pp.Id == Id).FirstOrDefault();
                    if (portfolioProject != null)
                    {
                        portfolioProject.PortfolioId = PortfolioId;
                        portfolioProject.ProjectId = ProjectId;
                        return dc.SaveChanges();
                    }
                    else throw new Exception("Portfolio Project not found");
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
                    var portfolioProject = (from pp in dc.tblPortfolioProjects
                                   //join p in dc.tblProjects on pp.ProjectId equals p.Id
                                   //join po in dc.tblPortfolios on pp.PortfolioId equals po.Id
                                   where pp.Id == id
                                   select new
                                   {
                                       pp.Id,
                                       pp.PortfolioId,
                                       pp.ProjectId
                                   }).FirstOrDefault();
                    if (portfolioProject != null)
                    {
                        Id = portfolioProject.Id;
                        PortfolioId = portfolioProject.PortfolioId;
                        ProjectId = portfolioProject.ProjectId;
                    }
                    else throw new Exception("Portfolio Project not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }

    public class PortfolioProjectList : List<PortfolioProject>
    {
        public void Load()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var portfolioProjects = (from p in dc.tblPortfolioProjects
                                             select new
                                       {
                                           p.Id,
                                           p.PortfolioId,
                                           p.ProjectId
                                       }).ToList();
                    foreach (var p in portfolioProjects)
                    {
                        PortfolioProject portfolioProject = new PortfolioProject(p.Id, p.PortfolioId, p.ProjectId);
                        Add(portfolioProject);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
