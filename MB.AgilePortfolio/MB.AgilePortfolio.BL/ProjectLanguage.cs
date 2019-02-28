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

        public void Insert()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblProjectLanguage projectlanguage = new tblProjectLanguage();

                    /* 
                        TODO: Compare code
                    --------------------------------------------------------------------------------------
                      Which works (or is preferred) Commented Code or uncommented code:
                        +   SIDENOTE: personally ProjectID = ProjectID looks ambigously confusing to me

                    {                
                        Id = Guid.NewGuid();
                        ProjectId = ProjectId;
                        LanguageId = LanguageId;
                    };
                     
                    */

                    projectlanguage.Id = Guid.NewGuid();
                    projectlanguage.ProjectId = this.ProjectId;
                    projectlanguage.LanguageId = this.LanguageId;

                    /*
                    --------------------------------------------------------------------------------------
                        END TODO: Compare code
                    */

                    dc.tblProjectLanguages.Add(projectlanguage);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void Delete()
        {
            try
            {
                Delete(null);
            }
            catch (Exception ex) { throw ex; }
        }

        public void Delete(Guid? id)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblProjectLanguage projectlanguage = dc.tblProjectLanguages.Where(pl => pl.Id == id).FirstOrDefault();
                    if (projectlanguage != null)
                    {
                        dc.tblProjectLanguages.Remove(projectlanguage);
                        dc.SaveChanges();
                    }
                    else throw new Exception("ProjectLanguage not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void Update()
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
                        dc.SaveChanges();
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

                    /*
                        TODO: Doublecheck Code
                    --------------------------------------------------------------------------------------
                           THIS NEEDED TO BE DOUBLECHECKED, AS I AM UNSURE THIS IS CORRECT
                    --------------------------------------------------------------------------------------
                        END TODO: Doublecheck Code
                    */

                    var projectlanguage = (from pl in dc.tblProjectLanguages
                                           join lang in dc.tblLanguages on pl.LanguageId equals lang.Id
                                           join p in dc.tblProjects on pl.ProjectId equals p.Id
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

                    /*TODO:
                            WHERE clause needs FIXING (or doublechecking)
                            Orderby needed?
                            In general checking needed (I'm rusty at BL SQL)
                    */

                    var projectlanguages = (from pl in dc.tblProjectLanguages 
                                 join lang in dc.tblLanguages on pl.LanguageId equals lang.Id
                                 join p in dc.tblProjects on pl.ProjectId equals p.Id
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
    }
}

