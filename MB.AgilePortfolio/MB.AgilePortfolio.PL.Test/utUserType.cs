using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.PL;
using System.Linq;


namespace MB.AgilePortfolio.PL.Test
{
    [TestClass]
    public class utUserType
    {
        [TestMethod]
        public void LoadTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Ensure that at least 1 row is loaded from the database
                Assert.IsTrue(dc.tblUserTypes.Count() > 0);
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Create a userType
                tblUserType userType = new tblUserType
                {
                    //UserType a GUID for testing purposes
                    Id = Guid.Parse("11112222-3333-4444-5555-666677778888"),
                    Description = "Test"
                };

                //Add the userType to the database
                dc.tblUserTypes.Add(userType);

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
                //Retrieve test userType based on ID and update it
                Guid userTypeGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");

                tblUserType userType = dc.tblUserTypes.FirstOrDefault(u => u.Id == userTypeGuid);

                userType.Description = "UpdatedUserType";

                //Save changes and get it back out
                dc.SaveChanges();
                tblUserType updatedUserType = dc.tblUserTypes.FirstOrDefault(u => u.Description == "UpdatedUserType");
                //Make sure the Ids match
                Assert.AreEqual(userType.Id, updatedUserType.Id);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                Guid userTypeGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
                tblUserType userType = dc.tblUserTypes.FirstOrDefault(u => u.Id == userTypeGuid);

                dc.tblUserTypes.Remove(userType);

                dc.SaveChanges();

                tblUserType deletedUserType =  dc.tblUserTypes.FirstOrDefault(u => u.Id == userTypeGuid);

                Assert.IsNull(deletedUserType);
            }
        }

    }
}
