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
        [DataType(DataType.MultilineText)]
        public string Bio { get; set; }

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
        public string UrlFriendlyName
        {
            get
            {
                return URLFriendly(this.Username);
            }
        }
        public string ShortBio
        {
            get
            {
                if (Bio == null)
                {
                    return String.Empty;
                }
                else if (Bio.Length < 100)
                {
                    return Bio;
                }
                else
                {
                    StringBuilder output = new StringBuilder();
                    int i = 100;
                    bool flag = true;
                    output.Append(Bio.Substring(0, 100));
                    while (flag)
                    {
                        if (i <= 125)
                        {
                            if (Bio[i] != ' ')
                            {
                                output.Append(Bio[i]);
                            }
                            else
                            {
                                output.Append("...");
                                flag = !flag;
                            }
                        }
                        else
                        {
                            output.Append("...");
                            flag = !flag;
                        }
                        i++;
                    }
                    return output.ToString();
                }
            }
        }


        #region URL Cleaning 
        //-----------------------START URL CLEANING METHODS------------------------------------

        /// <summary>
        /// Produces optional, URL-friendly version of a title, "like-this-one". 
        /// hand-tuned for speed, reflects performance refactoring contributed
        /// by John Gietzen (user otac0n) 
        /// </summary>
        private static string URLFriendly(string title)
        {
            if (title == null) return "";

            const int maxlen = 80;
            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }

        // Remaping International Charactors to ASCII Method for URLFriendly Method
        private static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();
            if ("àåáâäãåą".Contains(s))
            {
                return "a";
            }
            else if ("èéêëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'Þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else
            {
                return "";
            }
        }
        //----------------------- END URL CLEANING METHODS------------------------------------
        #endregion URL Cleaning

        #region Constructors
        //------------------- START CONSTRUCTORS ---------------------

        //  Empty Constructor
        public User() { }

        //  Standard Constructor
        public User(Guid id, string email, string password, string firstName, string lastName, string profileImage, Guid userTypeId, string userTypeDescription, string username, string bio)
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
            Bio = bio;
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
        //------------------- END CONSTRUCTORS ---------------------
        #endregion Constructors

        #region User Password
        //------------------- START PASSWORD METHODS ---------------------

        // Update Password
        public int UpdatePassword(string password, string oldpassword, Guid userId)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblUser user = dc.tblUsers.Where(u => u.Id == Id).FirstOrDefault();
                    if (user != null)
                    {
                        if (user.Password == GetHash(oldpassword, user.Id))
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

        // Password Hash Method
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
        //------------------- END PASSWORD METHODS ---------------------
        #endregion User Password

        #region Password Recovery
        //------------------- START PASSWORD RECOVERY METHODS ---------------------

        // Generate ForgotPasswordKey
        public Guid ForgotPasswordKeyGen(string email)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    //Create a new row in the table for this forgotten password request
                    tblUser user = dc.tblUsers.Where(u => u.Email == email).FirstOrDefault();
                    tblForgotPassword fp = new tblForgotPassword();
                    ForgotPasswordList previouslinks = new ForgotPasswordList();

                    // Clears previous reset links associated with user
                    previouslinks.ClearForgottenPasswordsByUser(user);

                    try
                    {
                        // Inserting new reset link in DB (User currently has none in DB here)
                        fp.Id = Guid.NewGuid();
                        fp.UserId = dc.tblUsers.FirstOrDefault(u => u.Email == email).Id;

                        // Expiration of reset link set to 2 hours from link creation
                        fp.ExpirationDate = DateTime.Now.AddHours(2);
                        dc.tblForgotPasswords.Add(fp);
                        dc.SaveChanges();
                        return fp.Id;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Change Password from Forgot password key
        public int ChangeForgottenPassword(string password)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    tblForgotPassword forgottenpass = dc.tblForgotPasswords.Where(u => u.UserId == Id).FirstOrDefault();
                    if ((DateTime.Now) < (forgottenpass.ExpirationDate))
                    {
                        // Current time is earlier than expiration date of reset link
                        tblUser user = dc.tblUsers.Where(u => u.Id == Id).FirstOrDefault();
                        if (user != null)
                        {
                            // User exists
                            user.Password = GetHash(password, user.Id);
                            return dc.SaveChanges();
                        }
                        else throw new Exception("User not found");
                    }
                    else throw new Exception("Password Reset Link Expired");
                }
            }
            catch (Exception ex) { throw ex; }

        }

        // Load Forgotten password by ID
        public void LoadByForgottenPassRequestId(Guid forgottenPassId)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var forgotPasswordRow = dc.tblForgotPasswords.FirstOrDefault(fp => fp.Id == forgottenPassId);
                    if (forgotPasswordRow != null)
                    {
                        this.Id = forgotPasswordRow.UserId;
                    }
                    else
                    {
                        //No user requested a password reset with this ID.
                        this.Id = Guid.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                { throw ex; }
            }
        }

        // Send Email to user (currently only used for Password Recovery)
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
                //Hardcoded Credentials.... IDK where to start with not hardcoding this
                var credential = new NetworkCredential
                {
                    UserName = "ProveITConfirmation@gmail.com", //Hardcoded
                    Password = "Pa$$word1" //Hardcoded
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp-relay.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.SendMailAsync(msg);
            }
        }
        //------------------- END PASSWORD RECOVERY METHODS ---------------------
        #endregion Password Recovery

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
                                        u.Bio,
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
                                Bio = user.Bio;
                                return true;
                            }
                            else { return false; }
                        }
                        else { return false; }
                    }

                    else
                    {
                        return false;
                    }
                }
                else if (Username != null && Username != String.Empty)
                {
                    if (Password != null && Password != string.Empty)
                    {
                        PortfolioEntities dc = new PortfolioEntities();

                        //tblUser user = dc.tblUsers.FirstOrDefault(u => u.Email == Email);

                        var user = (from u in dc.tblUsers
                                    join ut in dc.tblUserTypes on u.UserTypeId equals ut.Id
                                    where u.Username == Username
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
                                        u.Bio,
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
                                Bio = user.Bio;
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

        #region Standard Insert Update Delete
        //------------------- START STANDARD INSERT UPDATE DELETE METHODS ---------------------

        // Standard Insert Method
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
                        Username = Username,
                        Bio = Bio
                    };

                    //Save the Id
                    this.Id = user.Id;
                    dc.tblUsers.Add(user);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        // Standard Update Method
        public int Update()
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    // Password is done in seperate Passwordupdate Method
                    tblUser user = dc.tblUsers.Where(u => u.Id == Id).FirstOrDefault();
                    if (user != null)
                    {
                        user.Email = Email;
                        user.FirstName = FirstName;
                        user.LastName = LastName;
                        user.ProfileImage = ProfileImage;
                        user.UserTypeId = UserTypeId;
                        user.Username = Username;
                        user.Bio = Bio;
                        return dc.SaveChanges();
                    }
                    else throw new Exception("User not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }

        // Standard Delete Method
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
        //------------------- END STANDARD INSERT UPDATE DELETE METHODS ---------------------
        #endregion Standard Insert Update Delete

        #region Checking Existance 
        //------------------- START CHECKING EXISTANCE METHODS METHODS ---------------------
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
                        // User Doesnt Exist
                        return Guid.Empty;
                    }
                    else
                    {
                        // User Exists
                        return user.Id;
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion Checking Existance

        #region User Loads
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
                                    u.Bio,
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
                        Bio = user.Bio;
                    }
                    else throw new Exception("User not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion User Loads
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
                                     u.Bio,
                                     ut.Description
                                 }).OrderByDescending(u => u.LastName).ToList();
                    foreach (var u in users)
                    {
                        User user = new User(u.Id, u.Email, u.Password, u.FirstName, u.LastName, u.ProfileImage, u.UserTypeId, u.Description, u.Username, u.Bio);
                        Add(user);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Loads UserList by a User's Username as a string
        /// 
        /// DOESNT LOAD  PROJECTS OR PORTFOLIOS CURRENTLY FOR EACH USER
        /// 
        /// </summary>
        /// <param name="UserName"> The Username of the User as string </param>
        public void LoadByUserName(string UserName)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var users = (from u in dc.tblUsers
                                 join ut in dc.tblUserTypes on u.UserTypeId equals ut.Id
                                 where u.Username == UserName || UserName == null
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
                                     u.Bio,
                                     ut.Description
                                 }).OrderByDescending(u => u.LastName).ToList();
                    foreach (var u in users)
                    {
                        User user = new User(u.Id, u.Email, u.Password, u.FirstName, u.LastName, u.ProfileImage, u.UserTypeId, u.Description, u.Username, u.Bio);
                        //POSSIBLY ADD LOAD PROJECTS AND LOAD PORTFOLIOS HERE
                        Add(user);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Loads UserList by a partial User's Username as a string
        /// 
        /// DOESNT LOAD  PROJECTS OR PORTFOLIOS CURRENTLY FOR EACH USER
        /// 
        /// </summary>
        /// <param name="PartialUserName"> The partial Username of the User as string </param>
        public void LoadByPartialUserName(string PartialUserName)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var users = (from u in dc.tblUsers
                                 join ut in dc.tblUserTypes on u.UserTypeId equals ut.Id
                                 where u.Username.Contains(PartialUserName) || PartialUserName == null
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
                                     u.Bio,
                                     ut.Description
                                 }).OrderByDescending(u => u.LastName).ToList();
                    foreach (var u in users)
                    {
                        User user = new User(u.Id, u.Email, u.Password, u.FirstName, u.LastName, u.ProfileImage, u.UserTypeId, u.Description, u.Username, u.Bio);
                        //POSSIBLY ADD LOAD PROJECTS AND LOAD PORTFOLIOS HERE
                        Add(user);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}

