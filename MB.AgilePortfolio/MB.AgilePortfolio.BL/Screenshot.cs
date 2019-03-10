using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.AgilePortfolio.PL;

namespace MB.AgilePortfolio.BL
{
    public class Screenshot
    {
        public Guid Id { get; set; }
        public string Filepath { get; set; }
        [DisplayName("Project")]
        public Guid ProjectId { get; set; }
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        public Screenshot() { }

        public Screenshot(Guid id, string filepath, Guid projectId, string projectName)
        {
            Id = id;
            Filepath = filepath;
            ProjectId = projectId;
            ProjectName = projectName;
        }

        public int Insert()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblScreenshot screenshot = new tblScreenshot()
                    {
                        Id = Guid.NewGuid(),
                        Filepath = Filepath,
                        ProjectId = ProjectId
                    };
                    //Save the Id
                    this.Id = screenshot.Id;

                    dc.tblScreenshots.Add(screenshot);
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
                    tblScreenshot screenshot = dc.tblScreenshots.Where(s => s.Id == Id).FirstOrDefault();
                    if (screenshot != null)
                    {
                        dc.tblScreenshots.Remove(screenshot);
                        return dc.SaveChanges();
                    }
                    else throw new Exception("Screenshot not found");
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
                    tblScreenshot screenshot = dc.tblScreenshots.Where(s => s.Id == Id).FirstOrDefault();
                    if (screenshot != null)
                    {
                        screenshot.Filepath = Filepath;
                        screenshot.ProjectId = ProjectId;
                        return dc.SaveChanges();
                    }
                    else throw new Exception("Screenshot not found");
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
                    var screenshot = (from s in dc.tblScreenshots
                                      join p in dc.tblProjects on s.ProjectId equals p.Id
                                     where s.Id == id
                                     select new
                                     {
                                         s.Id,
                                         s.Filepath,
                                         s.ProjectId,
                                         p.Name
                                     }).FirstOrDefault();
                    if (screenshot != null)
                    {
                        Id = screenshot.Id;
                        Filepath = screenshot.Filepath;
                        ProjectId = screenshot.ProjectId;
                        ProjectName = screenshot.Name;
                    }
                    else throw new Exception("Screenshot not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }

    public class ScreenshotList : List<Screenshot>
    {

        public void Load()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var screenshots = (from s in dc.tblScreenshots
                                      join p in dc.tblProjects on s.ProjectId equals p.Id
                                      //where s.ProjectId == id || id == null
                                      select new
                                      {
                                          s.Id,
                                          s.Filepath,
                                          s.ProjectId,
                                          p.Name
                                      }).OrderByDescending(p => p.Filepath).ToList();
                    foreach (var s in screenshots)
                    {
                        Screenshot screenshot = new Screenshot(s.Id, s.Filepath, s.ProjectId, s.Name);
                        Add(screenshot);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
