using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.PL;
using System.Linq;


namespace MB.AgilePortfolio.PL.Test
{
    [TestClass]
    public class utScreenshot
    {
        [TestMethod]
        public void LoadTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Ensure that at least 1 row is loaded from the database
                Assert.IsTrue(dc.tblScreenshots.Count() > 0);
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Create a screenshot
                tblScreenshot screenshot = new tblScreenshot
                {
                    //Screenshot a GUID for testing purposes
                    Id = Guid.Parse("11112222-3333-4444-5555-666677778888"),
                    Filepath = "Test",
                    ProjectId = Guid.NewGuid()
                };

                //Add the screenshot to the database
                dc.tblScreenshots.Add(screenshot);

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
                //Retrieve test screenshot based on ID and update it
                Guid screenshotGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");

                tblScreenshot screenshot = dc.tblScreenshots.FirstOrDefault(s => s.Id == screenshotGuid);

                Guid projectGuid = Guid.Parse("88887777-6666-5555-4444-333322221111");
                screenshot.ProjectId = projectGuid;


                //Save changes and get it back out
                dc.SaveChanges();
                tblScreenshot updatedScreenshot = dc.tblScreenshots.FirstOrDefault(s => s.ProjectId == projectGuid);
                //Make sure the Ids match
                Assert.AreEqual(screenshot.Id, updatedScreenshot.Id);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                Guid screenshotGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
                tblScreenshot screenshot = dc.tblScreenshots.FirstOrDefault(s => s.Id == screenshotGuid);

                dc.tblScreenshots.Remove(screenshot);

                dc.SaveChanges();

                tblScreenshot deletedScreenshot =  dc.tblScreenshots.FirstOrDefault(s => s.Id == screenshotGuid);

                Assert.IsNull(deletedScreenshot);
            }
        }

    }
}
