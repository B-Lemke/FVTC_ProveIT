using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.PL;
using System.Linq;


namespace MB.AgilePortfolio.PL.Test
{
    [TestClass]
    public class utStatus
    {
        [TestMethod]
        public void LoadTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Ensure that at least 1 row is loaded from the database
                Assert.IsTrue(dc.tblStatuses.Count() > 0);
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Create a status
                tblStatus status = new tblStatus
                {
                    //Status a GUID for testing purposes
                    Id = Guid.Parse("11112222-3333-4444-5555-666677778888"),
                    Description = "Test"
                };

                //Add the status to the database
                dc.tblStatuses.Add(status);

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
                //Retrieve test status based on ID and update it
                Guid statusGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");

                tblStatus status = dc.tblStatuses.FirstOrDefault(s => s.Id == statusGuid);

                status.Description = "UpdatedDescription";

                //Save changes and get it back out
                dc.SaveChanges();
                tblStatus updatedStatus = dc.tblStatuses.FirstOrDefault(s => s.Description == "UpdatedDescription");
                //Make sure the Ids match
                Assert.AreEqual(status.Id, updatedStatus.Id);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                Guid statusGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
                tblStatus status = dc.tblStatuses.FirstOrDefault(s => s.Id == statusGuid);

                dc.tblStatuses.Remove(status);

                dc.SaveChanges();

                tblStatus deletedStatus =  dc.tblStatuses.FirstOrDefault(s => s.Id == statusGuid);

                Assert.IsNull(deletedStatus);
            }
        }

    }
}
