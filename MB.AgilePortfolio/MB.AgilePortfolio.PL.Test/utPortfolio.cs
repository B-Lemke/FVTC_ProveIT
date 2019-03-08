using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.PL;
using System.Linq;


namespace MB.AgilePortfolio.PL.Test
{
    [TestClass]
    public class utPortfolio
    {
        [TestMethod]
        public void LoadTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Ensure that at least 1 row is loaded from the database
                Assert.IsTrue(dc.tblPortfolios.Count() > 0);
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Create a portfolio
                tblPortfolio portfolio = new tblPortfolio
                {
                    //Portfolio a GUID for testing purposes
                    Id = Guid.Parse("11112222-3333-4444-5555-666677778888"),
                    Description = "Test",
                    Name = "Test",
                    PortfolioImage = "Test",
                    UserId = Guid.NewGuid()
                };

                //Add the portfolio to the database
                dc.tblPortfolios.Add(portfolio);

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
                //Retrieve test portfolio based on ID and update it
                Guid portfolioGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");

                tblPortfolio portfolio = dc.tblPortfolios.FirstOrDefault(p => p.Id == portfolioGuid);

                Guid userGuid = Guid.Parse("88887777-6666-5555-4444-333322221111");
                portfolio.UserId = userGuid;

                //Save changes and get it back out
                dc.SaveChanges();
                tblPortfolio updatedPortfolio = dc.tblPortfolios.FirstOrDefault(p => p.UserId == userGuid);
                //Make sure the Ids match
                Assert.AreEqual(portfolio.Id, updatedPortfolio.Id);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                Guid portfolioGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
                tblPortfolio portfolio = dc.tblPortfolios.FirstOrDefault(p => p.Id == portfolioGuid);

                dc.tblPortfolios.Remove(portfolio);

                dc.SaveChanges();

                tblPortfolio deletedPortfolio =  dc.tblPortfolios.FirstOrDefault(p => p.Id == portfolioGuid);

                Assert.IsNull(deletedPortfolio);
            }
        }

    }
}
