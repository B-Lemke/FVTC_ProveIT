using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.AgilePortfolio.PL;

namespace MB.AgilePortfolio.BL
{
    public class PortfolioProject
    {
        public Guid Id { get; set; }
        [DisplayName("Portfolio")]
        public Guid PortfolioId { get; set; }
        [DisplayName("Project")]
        public Guid ProjectId { get; set; }
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }
        [DisplayName("Portfolio Name")]
        public string PortfolioName { get; set; }
        public readonly string UrlFriendlyProjectName;
        public readonly string UrlFriendlyPortfolioName;


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


        public PortfolioProject() { }
        
        public PortfolioProject(Guid id, Guid portfolioId, Guid projectId, string portfolioName, string projectName)
        {
            Id = id;
            PortfolioId = portfolioId;
            ProjectId = projectId;
            PortfolioName = portfolioName;
            ProjectName = projectName;
            UrlFriendlyProjectName = URLFriendly(projectName);
            UrlFriendlyPortfolioName = URLFriendly(portfolioName);
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
                                   join pr in dc.tblProjects on pp.ProjectId equals pr.Id
                                   join po in dc.tblPortfolios on pp.PortfolioId equals po.Id
                                   where pp.Id == id
                                   select new
                                   {
                                       pp.Id,
                                       pp.PortfolioId,
                                       pp.ProjectId,
                                       ProjectName = pr.Name,
                                       PortfolioName = po.Name
                                   }).FirstOrDefault();
                    if (portfolioProject != null)
                    {
                        Id = portfolioProject.Id;
                        PortfolioId = portfolioProject.PortfolioId;
                        ProjectId = portfolioProject.ProjectId;
                        ProjectName = portfolioProject.ProjectName;
                        PortfolioName = portfolioProject.PortfolioName;
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
                                             join pr in dc.tblProjects on p.ProjectId equals pr.Id
                                             join po in dc.tblPortfolios on p.PortfolioId equals po.Id
                                             select new
                                       {
                                           p.Id,
                                           p.PortfolioId,
                                           p.ProjectId,
                                           ProjectName = pr.Name,
                                           PortfolioName = po.Name
                                             }).ToList();
                    foreach (var p in portfolioProjects)
                    {
                        PortfolioProject portfolioProject = new PortfolioProject(p.Id, p.PortfolioId, p.ProjectId, p.PortfolioName, p.ProjectName);
                        Add(portfolioProject);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
