using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.PL;
using System.Linq;


namespace MB.AgilePortfolio.PL.Test
{
    [TestClass]
    public class utUser
    {
        [TestMethod]
        public void LoadTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Ensure that at least 1 row is loaded from the database
                Assert.IsTrue(dc.tblUsers.Count() > 0);
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Create a user
                tblUser user = new tblUser
                {
                    //User a GUID for testing purposes
                    Id = Guid.Parse("11112222-3333-4444-5555-666677778888"),
                    Email = "Test@Test.Test",
                    Password = "Test",
                    FirstName = "Test",
                    LastName = "Test",
                    ProfileImage = "Test.Test",
                    UserTypeId = Guid.NewGuid()
                };

                //Add the user to the database
                dc.tblUsers.Add(user);

                //Commit changes
                int rowsInserted = dc.SaveChanges();

                Assert.IsTrue(rowsInserted == 1);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Retrieve test user based on ID and update it
                Guid userGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");

                tblUser user = dc.tblUsers.FirstOrDefault(u => u.Id == userGuid);

                Guid userTypeGuid = Guid.Parse("88887777-6666-5555-4444-333322221111");
                user.UserTypeId = userTypeGuid;

                //Save changes and get it back out
                dc.SaveChanges();
                tblUser updatedUser = dc.tblUsers.FirstOrDefault(u => u.UserTypeId == userTypeGuid);
                //Make sure the Ids match
                Assert.AreEqual(user.Id, updatedUser.Id);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                Guid userGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
                tblUser user = dc.tblUsers.FirstOrDefault(u => u.Id == userGuid);

                dc.tblUsers.Remove(user);

                dc.SaveChanges();

                tblUser deletedUser =  dc.tblUsers.FirstOrDefault(u => u.Id == userGuid);

                Assert.IsNull(deletedUser);
            }
        }

    }
}
