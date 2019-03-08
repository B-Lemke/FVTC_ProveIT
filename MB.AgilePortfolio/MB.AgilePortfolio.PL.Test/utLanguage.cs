using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.PL;
using System.Linq;


namespace MB.AgilePortfolio.PL.Test
{
    [TestClass]
    public class utLanguage
    {
        [TestMethod]
        public void LoadTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Ensure that at least 1 row is loaded from the database
                Assert.IsTrue(dc.tblLanguages.Count() > 0);
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Create a language
                tblLanguage language = new tblLanguage
                {
                    //Language a GUID for testing purposes
                    Id = Guid.Parse("11112222-3333-4444-5555-666677778888"),
                    Description = "Test"
                };

                //Add the language to the database
                dc.tblLanguages.Add(language);

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
                //Retrieve test language based on ID and update it
                Guid languageGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");

                tblLanguage language = dc.tblLanguages.FirstOrDefault(l => l.Id == languageGuid);

                language.Description = "UpdatedLang";

                //Save changes and get it back out
                dc.SaveChanges();
                tblLanguage updatedLanguage = dc.tblLanguages.FirstOrDefault(l => l.Description == "UpdatedLang");
                //Make sure the Ids match
                Assert.AreEqual(language.Id, updatedLanguage.Id);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                Guid languageGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
                tblLanguage language = dc.tblLanguages.FirstOrDefault(l => l.Id == languageGuid);

                dc.tblLanguages.Remove(language);

                dc.SaveChanges();

                tblLanguage deletedLanguage =  dc.tblLanguages.FirstOrDefault(l => l.Id == languageGuid);

                Assert.IsNull(deletedLanguage);
            }
        }

    }
}
