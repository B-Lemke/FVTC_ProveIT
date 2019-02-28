using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.AgilePortfolio.PL;

namespace MB.AgilePortfolio.BL
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public Guid UserTypeId { get; set; }


        public User() { }

        public User(Guid id, string email, string password, string firstName, string lastName, string profileImage, Guid userTypeId)
        {
            Id = id;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            ProfileImage = profileImage;
            UserTypeId = userTypeId;
        }

        public void Insert()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblUser user = new tblUser()
                    {
                        Id = Guid.NewGuid(),
                        Email = Email,
                        Password = Password,
                        FirstName = FirstName,
                        LastName = LastName,
                        ProfileImage = ProfileImage,
                        UserTypeId = UserTypeId
                    };
                    dc.tblUsers.Add(user);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void Delete()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblUser user = dc.tblUsers.Where(u => u.Id == Id).FirstOrDefault();
                    if (user != null)
                    {
                        dc.tblUsers.Remove(user);
                        dc.SaveChanges();
                    }
                    else throw new Exception("User not found");
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
                    tblUser user = dc.tblUsers.Where(u => u.Id == Id).FirstOrDefault();
                    if (user != null)
                    {
                        user.Email = Email;
                        user.Password = Password;
                        user.FirstName = FirstName;
                        user.LastName = LastName;
                        user.ProfileImage = ProfileImage;
                        user.UserTypeId = UserTypeId;
                        dc.SaveChanges();
                    }
                    else throw new Exception("User not found");
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
                    var user = (from u in dc.tblUsers
                                join ut in dc.tblUserTypes on u.UserTypeId equals ut.Id
                                  where u.Id == id
                                  select new
                                  {
                                      u.Id,
                                      u.Email,
                                      u.Password,
                                      u.FirstName,
                                      u.LastName,
                                      u.ProfileImage,
                                      u.UserTypeId
                                  }).FirstOrDefault();
                    if (user != null)
                    {
                        Id = user.Id;
                        Email = user.Email;
                        Password = user.Password;
                        FirstName = user.FirstName;
                        LastName = user.LastName;
                        ProfileImage = user.ProfileImage;
                        UserTypeId = user.UserTypeId;
                    }
                    else throw new Exception("User not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
    public class UserList : List<User>
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
                    var users = (from u in dc.tblUsers
                                   join ut in dc.tblUserTypes on u.UserTypeId equals ut.Id
                                   where u.UserTypeId == id || id == null
                                   select new
                                   {
                                       u.Id,
                                       u.Email,
                                       u.Password,
                                       u.FirstName,
                                       u.LastName,
                                       u.ProfileImage,
                                       u.UserTypeId
                                   }).OrderByDescending(u => u.LastName).ToList();
                    foreach (var u in users)
                    {
                        User user = new User(u.Id, u.Email, u.Password, u.FirstName, u.LastName, u.ProfileImage, u.UserTypeId);
                        Add(user);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
