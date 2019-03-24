using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using MB.AgilePortfolio.PL;
using System.Web;
using System.Net.Mime;
using System.Net;

namespace MB.AgilePortfolio.BL
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
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
        public string Username { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public User() { }

        public User(Guid id, string email, string password, string firstName, string lastName, string profileImage, Guid userTypeId, string userTypeDescription, string username)
        {
            Id = id;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            ProfileImage = profileImage;
            UserTypeId = userTypeId;
            UserTypeDescription = userTypeDescription;
            Username = username;
        }

        // To use when adding a user
        public User(string email, string password, string firstName, string lastName, Guid userTypeId, string username)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            UserTypeId = userTypeId;
            Username = username;
        }

        // To use when a user logs in
        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        private string GetHash(string pass, Guid userId)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                //Take the userId and make it an uppercase string and concatenate it. This matches the HashBytes function in TransactSQL in our default data.
                string concatPass = pass + userId.ToString().ToUpper();
                byte[] saltedPass = Encoding.UTF8.GetBytes(concatPass);
                var hash = sha1.ComputeHash(saltedPass);
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public void SendMail(string Email, string Subject, string ResetLink)
        {
            string text = string.Format("Please click on this link to {0}: {1}", Subject, ResetLink);
            string html = "Please confirm your account by clicking this link: <a href=\"" + ResetLink + "\">link</a><br/>";
            html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + ResetLink);
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("ProveITConfirmation@gmail.com", "ProveIT");
            msg.To.Add(new MailAddress(Email));
            msg.Subject = Subject;
            msg.Body = string.Format(html, msg.From.DisplayName, msg.From.Address, msg);
            msg.IsBodyHtml = true;


            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "ProveITConfirmation@gmail.com",
                    Password = "Pa$$word1"
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp-relay.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.SendMailAsync(msg);
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
                                        u.Username,
                                        ut.Description
                                    }).FirstOrDefault();
                        if (user != null)
                        {
                            if (user.Password == GetHash(Password, user.Id))
                            {
                                // Login successful
                                FirstName = user.FirstName;
                                LastName = user.LastName;
                                Email = user.Email;
                                Password = user.Password;
                                Id = user.Id;
                                ProfileImage = user.ProfileImage;
                                UserTypeId = user.UserTypeId;
                                Username = user.Username;
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

        public int Insert()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    Guid userId = Guid.NewGuid();
                    tblUser user = new tblUser()
                    {
                        Id = userId,
                        Email = Email,
                        Password = GetHash(this.Password, userId),
                        FirstName = FirstName,
                        LastName = LastName,
                        ProfileImage = ProfileImage,
                        UserTypeId = UserTypeId,
                        Username = Username
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
                    //Removed password from the update...? That should be its own thing I think.
                    tblUser user = dc.tblUsers.Where(u => u.Id == Id).FirstOrDefault();
                    if (user != null)
                    {
                        user.Email = Email;
                        user.FirstName = FirstName;
                        user.LastName = LastName;
                        user.ProfileImage = ProfileImage;
                        user.UserTypeId = UserTypeId;
                        user.Username = Username;
                        return dc.SaveChanges();
                    }
                    else throw new Exception("User not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public int UpdatePassword(string password, string oldpassword, Guid userId)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblUser user = dc.tblUsers.Where(u => u.Id == Id).FirstOrDefault();
                    if (user != null)
                    {
                        if(user.Password == GetHash(oldpassword, user.Id))
                        {
                            user.Password = GetHash(password, user.Id);
                            return dc.SaveChanges();
                        }
                        else throw new Exception("Incorrect Password");

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

        public Guid CheckIfUsernameExists(string username)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var user = dc.tblUsers.FirstOrDefault(u => u.Username == username);
                    if (user == null)
                    {
                        return Guid.Empty;
                    }
                    else return user.Id;
         
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
                                    u.Username,
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
                        Username = user.Username;
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
                                     u.Username,
                                     ut.Description
                                 }).OrderByDescending(u => u.LastName).ToList();
                    foreach (var u in users)
                    {
                        User user = new User(u.Id, u.Email, u.Password, u.FirstName, u.LastName, u.ProfileImage, u.UserTypeId, u.Description, u.Username);
                        Add(user);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
