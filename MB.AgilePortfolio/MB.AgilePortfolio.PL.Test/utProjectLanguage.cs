using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.PL;
using System.Linq;


namespace MB.AgilePortfolio.PL.Test
{
    [TestClass]
    public class utProjectLanguage
    {
        [TestMethod]
        public void LoadTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Ensure that at least 1 row is loaded from the database
                Assert.IsTrue(dc.tblProjectLanguages.Count() > 0);
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Create a user
                tblProjectLanguage projectLanguage = new tblProjectLanguage
                {
                    //User a GUID for testing purposes
                    Id = Guid.Parse("11112222-3333-4444-5555-666677778888"),
                    LanguageId = Guid.NewGuid(),
                    ProjectId = Guid.NewGuid()
            };

                //Add the user to the database
                dc.tblProjectLanguages.Add(projectLanguage);

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
                Guid projectLanguageGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");

                tblProjectLanguage projectLanguage = dc.tblProjectLanguages.FirstOrDefault(p => p.Id == projectLanguageGuid);

                Guid languageGuid = Guid.Parse("88887777-6666-5555-4444-333322221111");
                projectLanguage.LanguageId = languageGuid;

                //Save changes and get it back out
                dc.SaveChanges();
                tblProjectLanguage updatedProjectLanguage = dc.tblProjectLanguages.FirstOrDefault(p => p.LanguageId == languageGuid);
                //Make sure the Ids match
                Assert.AreEqual(projectLanguage.Id, updatedProjectLanguage.Id);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                Guid projectLanguageGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
                tblProjectLanguage projectLanguage = dc.tblProjectLanguages.FirstOrDefault(p => p.Id == projectLanguageGuid);

                dc.tblProjectLanguages.Remove(projectLanguage);

                dc.SaveChanges();

                tblProjectLanguage deletedProjectLanguage =  dc.tblProjectLanguages.FirstOrDefault(p => p.Id == projectLanguageGuid);

                Assert.IsNull(deletedProjectLanguage);
            }
        }

    }
}
