﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.AgilePortfolio.PL;

namespace MB.AgilePortfolio.BL
{
    public class Status
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public Status() { }

        public Status(Guid id, string description)
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
                    tblStatus status = new tblStatus()
                    {
                        Id = Guid.NewGuid(),
                        Description = Description
                    };
                    //Save the Id
                    this.Id = status.Id;

                    dc.tblStatuses.Add(status);
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
                    tblStatus status = dc.tblStatuses.Where(s => s.Id == Id).FirstOrDefault();
                    if (status != null)
                    {
                        dc.tblStatuses.Remove(status);
                        return dc.SaveChanges();
                    }
                    else throw new Exception("Status not found");
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
                    tblStatus status = dc.tblStatuses.Where(s => s.Id == Id).FirstOrDefault();
                    if (status != null)
                    {
                        status.Description = Description;
                        return dc.SaveChanges();
                    }
                    else throw new Exception("Status not found");
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
                    tblStatus status = dc.tblStatuses.Where(s => s.Id == id).FirstOrDefault();
                    if (status != null)
                    {
                        Id = status.Id;
                        Description = status.Description;
                    }
                    else throw new Exception("Status not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }

    public class StatusList : List<Status>
    {
        public void Load()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var statuses = dc.tblStatuses.OrderBy(s => s.Description);
                    foreach (var s in statuses)

                    {
                        Status status = new Status(s.Id, s.Description);
                        Add(status);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
