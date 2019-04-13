using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.AgilePortfolio.PL;

namespace MB.AgilePortfolio.BL
{
    public class Language
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        //Returns a cleaned up version of the language name for css class when making badges.
        public string BadgeName
        {
            get
            {
                var name = Description.ToLower();
                if(name.Contains("#") || name.Contains(".") || name.Contains(" ") || name.Contains("+") || name.Contains("/"))
                {
                    var cleanedName = name.Replace("#", "sharp").Replace(".", "dot").Replace(" ", "").Replace("+", "plus").Replace("/", "slash");
                    return cleanedName;
                }
                else
                {
                    return name;
                }
            }
        }


        public Language() { }

        public Language(Guid id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Insert()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblLanguage language = new tblLanguage()
                    {
                        Id = Guid.NewGuid(),
                        Description = Description
                    };
                    //Save the Id
                    this.Id = language.Id;

                    dc.tblLanguages.Add(language);
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
                    tblLanguage language = dc.tblLanguages.Where(l => l.Id == Id).FirstOrDefault();
                    if (language != null)
                    {
                        dc.tblLanguages.Remove(language);
                        return dc.SaveChanges();
                    }
                    else throw new Exception("Language not found");
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
                    tblLanguage language = dc.tblLanguages.Where(l => l.Id == Id).FirstOrDefault();
                    if (language != null)
                    {
                        language.Description = Description;
                        return dc.SaveChanges();
                    }
                    else throw new Exception("Language not found");
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
                    tblLanguage language = dc.tblLanguages.Where(l => l.Id == id).FirstOrDefault();
                    if (language != null)
                    {
                        Id = language.Id;
                        Description = language.Description;
                    }
                    else throw new Exception("Language not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Loads Language by the Description string
        /// </summary>
        /// <param name="Description"> The description as a string </param>
        public void LoadByDescription(string Description)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblLanguage language = dc.tblLanguages.Where(l => l.Description == Description).FirstOrDefault();
                    if (language != null)
                    {
                        Id = language.Id;
                        Description = language.Description;
                    }
                    else
                    {
                        // not found
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
    public class LanguageList : List<Language>
    {
        public void Load()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var languages = dc.tblLanguages.OrderBy(l => l.Description);
                    foreach (var l in languages)

                    {
                        Language language = new Language(l.Id, l.Description);
                        Add(language);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}

