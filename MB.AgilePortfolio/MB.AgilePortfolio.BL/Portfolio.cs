using System;
using System.Collections.Generic;
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
        public string Description { get; set; }
        public string PortfolioImage { get; set; }
        public Guid UserId { get; set; }

        public Portfolio() { }

        public Portfolio(Guid id, string name, string description, string portfolioImage, Guid userId)
        {
            Id = id;
            Name = name;
            Description = description;
            PortfolioImage = portfolioImage;
            UserId = userId;
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
                                //join u in dc.tblUsers on p.UserId equals u.Id
                                where p.Id == id
                                select new
                                {
                                    p.Id,
                                    p.Name,
                                    p.Description,
                                    p.PortfolioImage,
                                    p.UserId
                                }).FirstOrDefault();
                    if (portfolio != null)
                    {
                        Id = portfolio.Id;
                        Name = portfolio.Name;
                        Description = portfolio.Description;
                        PortfolioImage = portfolio.PortfolioImage;
                        UserId = portfolio.UserId;
                    }
                    else throw new Exception("Portfolio not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }

    public class PortfolioList : List<Portfolio>
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
                    var portfolios = (from p in dc.tblPortfolios
                                 //join u in dc.tblUsers on p.UserId equals u.Id
                                 //where p.UserId == id || id == null
                                 select new
                                 {
                                     p.Id,
                                     p.Name,
                                     p.Description,
                                     p.PortfolioImage,
                                     p.UserId
                                 }).OrderByDescending(p => p.Name).ToList();
                    foreach (var p in portfolios)
                    {
                        Portfolio portfolio = new Portfolio(p.Id, p.Name, p.Description, p.PortfolioImage, p.UserId);
                        Add(portfolio);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
