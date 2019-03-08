using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.BL;
using System.Linq;

namespace MB.AgilePortfolio.BL.Test
{
    [TestClass]
    public class utPortfolio
    {


        [TestMethod]
        public void Load()
        {
            PortfolioList portfolios = new PortfolioList();
            portfolios.Load();

            int expected = 8;

            Assert.AreEqual(expected, portfolios.Count);
        }

        [TestMethod]
        public void Insert()
        {
            Portfolio portfolio = new Portfolio()
            {
                
                Description = "Test",
                Name = "Test",
                PortfolioImage = "Test",
                UserId = Guid.Parse("11112222-3333-4444-5555-666677778888")
        };

            int rowsInserted = portfolio.Insert();

            Assert.IsTrue(rowsInserted == 1);


        }

        [TestMethod]
        public void LoadById()
        {
            PortfolioList portfolios = new PortfolioList();
            portfolios.Load();
            Portfolio portfolio = new Portfolio();

            Guid userGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            portfolio.LoadById(portfolios.FirstOrDefault(p => p.UserId == userGuid).Id);

            Assert.AreEqual("Test", portfolio.Name);
        }

        [TestMethod]
        public void Update()
        {
            PortfolioList portfolios = new PortfolioList();
            portfolios.Load();
            Portfolio portfolio = new Portfolio();

            Guid userGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            portfolio.LoadById(portfolios.FirstOrDefault(p => p.UserId == userGuid).Id);

            portfolio.Name = "TestUpdate";
            int rowsAffected = portfolio.Update();

            Assert.IsTrue(rowsAffected == 1);
        }

        [TestMethod]
        public void Delete()
        {
            PortfolioList portfolios = new PortfolioList();
            portfolios.Load();
            Portfolio portfolio = new Portfolio();

            Guid userGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            portfolio.LoadById(portfolios.FirstOrDefault(p => p.UserId == userGuid).Id);

            int rowsAffected = portfolio.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }
    }
}
