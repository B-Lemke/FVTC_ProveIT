﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.AgilePortfolio.PL;

namespace MB.AgilePortfolio.BL
{
    public class Privacy
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public Privacy() { }

        public Privacy(Guid id, string description)
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
                    tblPrivacy privacy = new tblPrivacy()
                    {
                        Id = Guid.NewGuid(),
                        Description = Description
                    };
                    //Save the Id
                    this.Id = privacy.Id;

                    dc.tblPrivacies.Add(privacy);
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
                    tblPrivacy privacy = dc.tblPrivacies.Where(p => p.Id == Id).FirstOrDefault();
                    if (privacy != null)
                    {
                        dc.tblPrivacies.Remove(privacy);
                        return dc.SaveChanges();
                    }
                    else throw new Exception("Privacy not found");
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
                    tblPrivacy privacy = dc.tblPrivacies.Where(p => p.Id == Id).FirstOrDefault();
                    if (privacy != null)
                    {
                        privacy.Description = Description;
                        return dc.SaveChanges();
                    }
                    else throw new Exception("Privacy not found");
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
                    tblPrivacy privacy = dc.tblPrivacies.Where(p => p.Id == id).FirstOrDefault();
                    if (privacy != null)
                    {
                        Id = privacy.Id;
                        Description = privacy.Description;
                    }
                    else throw new Exception("Privacy not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }

    public class PrivacyList : List<Privacy>
    {
        public void Load()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var privacies = dc.tblPrivacies.OrderBy(p => p.Description);
                    foreach (var p in privacies)

                    {
                        Privacy privacy = new Privacy(p.Id, p.Description);
                        Add(privacy);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
