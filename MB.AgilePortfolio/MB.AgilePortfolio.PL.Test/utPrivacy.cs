using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.PL;
using System.Linq;


namespace MB.AgilePortfolio.PL.Test
{
    [TestClass]
    public class utPrivacy
    {
        [TestMethod]
        public void LoadTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Ensure that at least 1 row is loaded from the database
                Assert.IsTrue(dc.tblPrivacies.Count() > 0);
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Create a privacy
                tblPrivacy privacy = new tblPrivacy
                {
                    //Privacy a GUID for testing purposes
                    Id = Guid.Parse("11112222-3333-4444-5555-666677778888"),
                    Description = "Test"
                };

                //Add the privacy to the database
                dc.tblPrivacies.Add(privacy);

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
                //Retrieve test privacy based on ID and update it
                Guid privacyGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");

                tblPrivacy privacy = dc.tblPrivacies.FirstOrDefault(u => u.Id == privacyGuid);

                privacy.Description = "UpdatedPrivacy";

                //Save changes and get it back out
                dc.SaveChanges();
                tblPrivacy updatedPrivacy = dc.tblPrivacies.FirstOrDefault(u => u.Description == "UpdatedPrivacy");
                //Make sure the Ids match
                Assert.AreEqual(privacy.Id, updatedPrivacy.Id);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                Guid privacyGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
                tblPrivacy privacy = dc.tblPrivacies.FirstOrDefault(u => u.Id == privacyGuid);

                dc.tblPrivacies.Remove(privacy);

                dc.SaveChanges();

                tblPrivacy deletedPrivacy =  dc.tblPrivacies.FirstOrDefault(u => u.Id == privacyGuid);

                Assert.IsNull(deletedPrivacy);
            }
        }

    }
}
