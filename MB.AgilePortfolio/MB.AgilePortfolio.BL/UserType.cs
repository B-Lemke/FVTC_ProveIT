using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.AgilePortfolio.PL;

namespace MB.AgilePortfolio.BL
{
    public class UserType
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public UserType() { }
        
        public UserType(Guid id, string description)
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
                    tblUserType userType = new tblUserType()
                    {
                        Id = Guid.NewGuid(),
                        Description = Description
                    };
                    //Save the Id
                    this.Id = userType.Id;

                    dc.tblUserTypes.Add(userType);
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
                    tblUserType userType = dc.tblUserTypes.Where(ut => ut.Id == Id).FirstOrDefault();
                    if (userType != null)
                    {
                        dc.tblUserTypes.Remove(userType);
                        return dc.SaveChanges();
                    }
                    else throw new Exception("User Type not found");
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
                    tblUserType userType = dc.tblUserTypes.Where(ut => ut.Id == Id).FirstOrDefault();
                    if (userType != null)
                    {
                        userType.Description = Description;
                        return dc.SaveChanges();
                    }
                    else throw new Exception("User Type not found");
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
                    tblUserType userType = dc.tblUserTypes.Where(ut => ut.Id == id).FirstOrDefault();
                    if (userType != null)
                    {
                        Id = userType.Id;
                        Description = userType.Description;
                    }
                    else throw new Exception("User Type not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
    
    public class UserTypeList : List<UserType>
    {
        public void Load()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var userTypes = dc.tblUserTypes.OrderBy(ut => ut.Description);
                    foreach (var ut in userTypes)
                    {
                        UserType userType = new UserType(ut.Id, ut.Description);
                        Add(userType);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
        public void LoadNonAdmin()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var userTypes = dc.tblUserTypes.OrderBy(ut => ut.Description);
                    foreach (var ut in userTypes)
                    {
                        UserType userType = new UserType(ut.Id, ut.Description);
                        if(ut.Description != "Admin")
                        {
                            Add(userType);
                        }
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
