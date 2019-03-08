using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.BL;
using System.Linq;

namespace MB.AgilePortfolio.BL.Test
{
    [TestClass]
    public class utPortfolioProject
    {
        

        [TestMethod]
        public void Load()
        {
            PortfolioProjectList portfolioProjects = new PortfolioProjectList();
            portfolioProjects.Load();

            int expected = 8;

            Assert.AreEqual(expected, portfolioProjects.Count);
        }

        [TestMethod]
        public void Insert()
        {
            PortfolioProject portfolioProject = new PortfolioProject()
            {
                ProjectId = Guid.Parse("11112222-3333-4444-5555-666677778888"),
                PortfolioId = Guid.Parse("11112222-3333-4444-5555-666677778888")
            };

            int rowsInserted = portfolioProject.Insert();

            Assert.IsTrue(rowsInserted == 1);

            
        }

        [TestMethod]
        public void LoadById()
        {
            PortfolioProjectList portfolioProjects = new PortfolioProjectList();
            portfolioProjects.Load();
            PortfolioProject portfolioProject = new PortfolioProject();

            Guid projectGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            portfolioProject.LoadById(portfolioProjects.FirstOrDefault(p=>p.ProjectId == projectGuid).Id);

            Assert.AreEqual(projectGuid, portfolioProject.PortfolioId);
        }

        [TestMethod]
        public void Update()
        {
            PortfolioProjectList portfolioProjects = new PortfolioProjectList();
            portfolioProjects.Load();
            PortfolioProject portfolioProject = new PortfolioProject();

            Guid projectGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            portfolioProject.LoadById(portfolioProjects.FirstOrDefault(p => p.ProjectId == projectGuid).Id);

            portfolioProject.ProjectId = Guid.Parse("99999999-9999-9999-9999-999999999999");
            int rowsAffected = portfolioProject.Update();

            Assert.IsTrue(rowsAffected == 1);
        }

        [TestMethod]
        public void Delete()
        {
            PortfolioProjectList portfolioProjects = new PortfolioProjectList();
            portfolioProjects.Load();
            Guid projectGuid = Guid.Parse("99999999-9999-9999-9999-999999999999");
            PortfolioProject portfolioProject = portfolioProjects.FirstOrDefault(p => p.ProjectId == projectGuid);

            int rowsAffected = portfolioProject.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }
    }
}
