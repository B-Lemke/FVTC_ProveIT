using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.AgilePortfolio.PL;

namespace MB.AgilePortfolio.BL
{
    public class ProjectLanguage
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid LanguageId { get; set; }

        public ProjectLanguage() { }

        public ProjectLanguage(Guid id, Guid projectId, Guid languageId)
        {
            Id = id;
            ProjectId = projectId;
            LanguageId = languageId;
        }

        public int Insert()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblProjectLanguage projectlanguage = new tblProjectLanguage();

                    projectlanguage.Id = Guid.NewGuid();
                    projectlanguage.ProjectId = this.ProjectId;
                    projectlanguage.LanguageId = this.LanguageId;
                    this.Id = projectlanguage.Id;

                    dc.tblProjectLanguages.Add(projectlanguage);
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
                    tblProjectLanguage projectlanguage = dc.tblProjectLanguages.Where(pl => pl.Id == this.Id).FirstOrDefault();
                    if (projectlanguage != null)
                    {
                        dc.tblProjectLanguages.Remove(projectlanguage);
                        return dc.SaveChanges();
                    }
                    else throw new Exception("ProjectLanguage not found");
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
                    tblProjectLanguage projectlanguage = dc.tblProjectLanguages.Where(pl => pl.Id == Id).FirstOrDefault();
                    if (projectlanguage != null)
                    {
                        projectlanguage.ProjectId = this.ProjectId;
                        projectlanguage.LanguageId = this.LanguageId;
                        return dc.SaveChanges();
                    }
                    else throw new Exception("ProjectLanguage not found");
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
                    var projectlanguage = (from pl in dc.tblProjectLanguages
                                               //join lang in dc.tblLanguages on pl.LanguageId equals lang.Id
                                               //join p in dc.tblProjects on pl.ProjectId equals p.Id
                                           where pl.Id == id
                                           select new
                                           {
                                               pl.Id,
                                               pl.ProjectId,
                                               pl.LanguageId
                                           }).FirstOrDefault();
                    if (projectlanguage != null)
                    {
                        Id = projectlanguage.Id;
                        ProjectId = projectlanguage.ProjectId;
                        LanguageId = projectlanguage.LanguageId;
                    }
                    else throw new Exception("ProjectLanguage not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public Guid? CheckIfExists()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblProjectLanguage projectlanguage = dc.tblProjectLanguages.Where(pl => pl.ProjectId == this.ProjectId && pl.LanguageId == this.LanguageId).FirstOrDefault();
                    if (projectlanguage != null)
                    {
                        return projectlanguage.Id;
                    }
                    else
                    {
                        return Guid.Empty;
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
    public class ProjectLanguageList : List<ProjectLanguage>
    {
        public void Load()
        {
            try
            {
                Load(null);
            }
            catch (Exception ex) { throw ex; }
        }

        public void Load(Guid? id)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var projectlanguages = (from pl in dc.tblProjectLanguages
                                                //join lang in dc.tblLanguages on pl.LanguageId equals lang.Id
                                                //join p in dc.tblProjects on pl.ProjectId equals p.Id
                                            where pl.Id == id || id == null
                                            select new
                                            {
                                                pl.Id,
                                                pl.ProjectId,
                                                pl.LanguageId
                                            }).OrderByDescending(pl => pl.Id).ToList();
                    foreach (var pl in projectlanguages)
                    {
                        ProjectLanguage projectlanguage = new ProjectLanguage(pl.Id, pl.ProjectId, pl.LanguageId);
                        Add(projectlanguage);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void LoadByProjectId(Guid? id)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var projectlanguages = (from pl in dc.tblProjectLanguages
                                                //join lang in dc.tblLanguages on pl.LanguageId equals lang.Id
                                                //join p in dc.tblProjects on pl.ProjectId equals p.Id
                                            where pl.ProjectId == id || id == null
                                            select new
                                            {
                                                pl.Id,
                                                pl.ProjectId,
                                                pl.LanguageId
                                            }).OrderByDescending(pl => pl.Id).ToList();
                    foreach (var pl in projectlanguages)
                    {
                        ProjectLanguage projectlanguage = new ProjectLanguage(pl.Id, pl.ProjectId, pl.LanguageId);
                        Add(projectlanguage);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Deletes all records in ProjectLanguage Table by the Project Id THIS IS CURRENTLY NOT NESSCARY OR USED
        /// </summary>
        /// <param name="id">The Guid Id of the Project to delete all language associations for </param>
        public void DeleteByProjectId(Guid id)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var projectlanguages = (from pl in dc.tblProjectLanguages
                                            where pl.ProjectId == id || id == null
                                            select new
                                            {
                                                pl.Id,
                                                pl.ProjectId,
                                                pl.LanguageId
                                            }).OrderByDescending(pl => pl.Id).ToList();
                    foreach (var pl in projectlanguages)
                    {
                        ProjectLanguage projectlanguage = new ProjectLanguage(pl.Id, pl.ProjectId, pl.LanguageId);
                        projectlanguage.Delete();
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void LoadByLanguageID(Guid? id)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var projectlanguages = (from pl in dc.tblProjectLanguages
                                            join lang in dc.tblLanguages on pl.LanguageId equals lang.Id
                                            join p in dc.tblProjects on pl.ProjectId equals p.Id
                                            where pl.LanguageId == id || id == null
                                            select new
                                            {
                                                pl.Id,
                                                pl.ProjectId,
                                                pl.LanguageId
                                            }).OrderByDescending(pl => pl.Id).ToList();
                    foreach (var pl in projectlanguages)
                    {
                        ProjectLanguage projectlanguage = new ProjectLanguage(pl.Id, pl.ProjectId, pl.LanguageId);
                        Add(projectlanguage);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Loads ProjectLanguageList by a Language Description (name)
        /// </summary>
        /// <param name="LanguageName"> The description (name) of the language as a string </param>
        public void LoadByLanguageName(string LanguageName)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var projectlanguages = (from pl in dc.tblProjectLanguages
                                            join lang in dc.tblLanguages on pl.LanguageId equals lang.Id
                                            join p in dc.tblProjects on pl.ProjectId equals p.Id
                                            where lang.Description == LanguageName || LanguageName == null
                                            select new
                                            {
                                                pl.Id,
                                                pl.ProjectId,
                                                pl.LanguageId
                                            }).OrderByDescending(pl => pl.Id).ToList();
                    foreach (var pl in projectlanguages)
                    {
                        ProjectLanguage projectlanguage = new ProjectLanguage(pl.Id, pl.ProjectId, pl.LanguageId);
                        Add(projectlanguage);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Loads ProjectLanguageList by a partial Language description (name)
        /// </summary>
        /// <param name="PartialLanguageName"> The description (name) of the Language as string</param>
        public void LoadByPartialLanguageName(string PartialLanguageName)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var projectlanguages = (from pl in dc.tblProjectLanguages
                                            join lang in dc.tblLanguages on pl.LanguageId equals lang.Id
                                            join p in dc.tblProjects on pl.ProjectId equals p.Id
                                            where lang.Description.Contains(PartialLanguageName) || PartialLanguageName == null
                                            select new
                                            {
                                                pl.Id,
                                                pl.ProjectId,
                                                pl.LanguageId
                                            }).OrderByDescending(pl => pl.Id).ToList();
                    foreach (var pl in projectlanguages)
                    {
                        ProjectLanguage projectlanguage = new ProjectLanguage(pl.Id, pl.ProjectId, pl.LanguageId);
                        Add(projectlanguage);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

    }
}

