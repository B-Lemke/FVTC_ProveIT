using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MB.AgilePortfolio.PL;
using System.Web;
using System.Net.Mime;

namespace MB.AgilePortfolio.BL
{
    public class ForgotPassword
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpirationDate { get; set; }


        public ForgotPassword() { }

        public ForgotPassword(Guid id, Guid userid, DateTime expirationdate)
        {
            Id = id;
            UserId = userid;
            ExpirationDate = expirationdate;
        }

        public void LoadByUserId(Guid id)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var fg = (from f in dc.tblForgotPasswords
                                join u in dc.tblUsers on f.UserId equals u.Id
                                where u.Id == id
                                select new
                                {
                                    f.Id,
                                    f.ExpirationDate,
                                    f.UserId,
                                }).FirstOrDefault();
                    if (fg != null)
                    {
                        Id = fg.Id;
                        ExpirationDate = fg.ExpirationDate;
                        UserId= fg.UserId;
                    }
                    else throw new Exception("Reset Link not found");
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
                    tblForgotPassword fg = dc.tblForgotPasswords.Where(u => u.Id == Id).FirstOrDefault();
                    if (fg != null)
                    {
                        dc.tblForgotPasswords.Remove(fg);
                        return dc.SaveChanges();
                    }
                    else throw new Exception("User not found");
                }
            }
            catch (Exception ex) { throw ex; }
        }


    }
    public class ForgotPasswordList : List<ForgotPassword>
    {
        public int ClearForgottenPasswordsByUser(tblUser user)
        {
            try
            {
                using (PortfolioEntities dc = new PortfolioEntities())
                {
                    var links = (from link in dc.tblForgotPasswords
                                 join u in dc.tblUsers on link.UserId equals u.Id
                                 where link.UserId == user.Id
                                 select new
                                 {
                                     link.Id,
                                     link.UserId,
                                     link.ExpirationDate

                                 }).ToList();

                    foreach (var link in links)
                    {
                        tblForgotPassword forgottenpass = dc.tblForgotPasswords.Where(p => p.Id == link.Id).FirstOrDefault();
                        forgottenpass.Id = link.Id;
                        forgottenpass.ExpirationDate = link.ExpirationDate;
                        forgottenpass.UserId = link.UserId;
                        dc.tblForgotPasswords.Remove(forgottenpass);
                    }
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
