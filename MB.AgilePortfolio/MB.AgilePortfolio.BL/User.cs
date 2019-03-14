﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        [DisplayName("User Type")]
        public Guid UserTypeId { get; set; }
        [DisplayName("User Type")]
        public string UserTypeDescription { get; set; }

        public User() { }

        public User(Guid id, string email, string password, string firstName, string lastName, string profileImage, Guid userTypeId, string userTypeDescription)
        {
            Id = id;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            ProfileImage = profileImage;
            UserTypeId = userTypeId;
            UserTypeDescription = userTypeDescription;
        }

        // To use when adding a user
        public User(string email, string password, string firstName, string lastName)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        // To use when updating a user
        public User(Guid id, string email, string password, string firstName, string lastName)
        {
            Id = id;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        // To use when a user logs in
        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        private string GetHash()
        {
            using (var hash = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = Encoding.UTF8.GetBytes(Password);
                return Convert.ToBase64String(hash.ComputeHash(hashbytes));
            }
        }

        public bool Login()
        {
            try
            {
                if (Email != null && Email != string.Empty)
                {
                    if (Password != null && Password != string.Empty)
                    {
                        PortfolioEntities dc = new PortfolioEntities();
                        //tblUser user = dc.tblUsers.FirstOrDefault(u => u.Email == Email);
                        var user = (from u in dc.tblUsers
                                    join ut in dc.tblUserTypes on u.UserTypeId equals ut.Id
                                    where u.Email == Email
                                    select new
                                    {
                                        u.Id,
                                        u.Email,
                                        u.Password,
                                        u.FirstName,
                                        u.LastName,
                                        u.ProfileImage,
                                        u.UserTypeId,
                                        ut.Description
                                    }).FirstOrDefault();
                        if (user != null)
                        {
                            if (user.Password == GetHash())
                            {
                                // Login successful
                                FirstName = user.FirstName;
                                LastName = user.LastName;
                                Email = user.Email;
                                Password = user.Password;
                                Id = user.Id;
                                ProfileImage = user.ProfileImage;
                                UserTypeId = user.UserTypeId;
                                UserTypeDescription = user.Description;
                                return true;
                            }
                            else { return false; }
                        }
                        else { return false; }
                    }
                    else { return false; }
                }
                else { return false; }
            }
            catch (Exception ex) { throw ex; }
        }

        private void Map(tblUser user)
        {
            // Set the properties on the datarow from the database
            user.Id = Id;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Email = Email;
            user.Password = Password;
            user.ProfileImage = ProfileImage;
            user.UserTypeId = UserTypeId;
        }

        public void Seed()
        {
            try
            {
                User user = new User("", "", "", "");
                user.Password = user.GetHash();
                tblUser datarow = new tblUser();
                user.Map(datarow);
                user.Insert();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Insert()
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
                    //Save the Id
                    this.Id = user.Id;

                    dc.tblUsers.Add(user);
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
                    tblUser user = dc.tblUsers.Where(u => u.Id == Id).FirstOrDefault();
                    if (user != null)
                    {
                        dc.tblUsers.Remove(user);
                        return dc.SaveChanges();
                    }
                    else throw new Exception("User not found");
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
                    tblUser user = dc.tblUsers.Where(u => u.Id == Id).FirstOrDefault();
                    if (user != null)
                    {
                        user.Email = Email;
                        user.Password = Password;
                        user.FirstName = FirstName;
                        user.LastName = LastName;
                        user.ProfileImage = ProfileImage;
                        user.UserTypeId = UserTypeId;
                        return dc.SaveChanges();
                    }
                    else throw new Exception("User not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public bool CheckIfEmailExists(string email)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    bool exists = dc.tblUsers.Any(u => u.Email == email);
                        return exists;
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
                                      u.UserTypeId,
                                      ut.Description
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
                        UserTypeDescription = user.Description;
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
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var users = (from u in dc.tblUsers
                                   join ut in dc.tblUserTypes on u.UserTypeId equals ut.Id
                                   select new
                                   {
                                       u.Id,
                                       u.Email,
                                       u.Password,
                                       u.FirstName,
                                       u.LastName,
                                       u.ProfileImage,
                                       u.UserTypeId,
                                       ut.Description
                                   }).OrderByDescending(u => u.LastName).ToList();
                    foreach (var u in users)
                    {
                        User user = new User(u.Id, u.Email, u.Password, u.FirstName, u.LastName, u.ProfileImage, u.UserTypeId, u.Description);
                        Add(user);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
