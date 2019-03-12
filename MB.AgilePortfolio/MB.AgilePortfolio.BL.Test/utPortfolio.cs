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
 
            Assert.IsTrue(portfolios.Count > 0);
        }

        [TestMethod]
        public void Insert()
        {

            UserList us = new UserList();
            us.Load();
            User user = us.FirstOrDefault(u => u.Email == "blemke4@gmail.com");

            Portfolio portfolio = new Portfolio()
            {

                Description = "Test",
                Name = "Test",
                PortfolioImage = "Test",
                UserId = user.Id
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

            portfolio.LoadById(portfolios.FirstOrDefault(p => p.Description == "Test").Id);

            Assert.AreEqual("Test", portfolio.Name);
        }

        [TestMethod]
        public void Update()
        {
            PortfolioList portfolios = new PortfolioList();
            portfolios.Load();
            Portfolio portfolio = new Portfolio();

            portfolio.LoadById(portfolios.FirstOrDefault(p => p.Description == "Test").Id);

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

            portfolio.LoadById(portfolios.FirstOrDefault(p => p.Description == "Test").Id);

            int rowsAffected = portfolio.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }
    }
}
