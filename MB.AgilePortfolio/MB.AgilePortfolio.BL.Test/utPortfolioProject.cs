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

            Assert.IsTrue(portfolioProjects.Count > 1);
        }

        [TestMethod]
        public void Insert()
        {
            ProjectList projects = new ProjectList();
            projects.Load();
            Project project = projects.FirstOrDefault(p => p.Name == "ProveIT");

            PortfolioList portfolios = new PortfolioList();
            portfolios.Load();
            Portfolio portfolio = portfolios.FirstOrDefault(p => p.Name == "Brodys First Portfolio");


            PortfolioProject portfolioProject = new PortfolioProject()
            {
                ProjectId = project.Id,
                PortfolioId = portfolio.Id
            };

            int rowsInserted = portfolioProject.Insert();

            Assert.IsTrue(rowsInserted == 1);

            
        }

        [TestMethod]
        public void LoadById()
        {
            ProjectList projects = new ProjectList();
            projects.Load();
            Project project = projects.FirstOrDefault(p => p.Name == "ProveIT");

            PortfolioList portfolios = new PortfolioList();
            portfolios.Load();
            Portfolio portfolio = portfolios.FirstOrDefault(p => p.Name == "Brodys First Portfolio");


            PortfolioProjectList portfolioProjects = new PortfolioProjectList();
            portfolioProjects.Load();
            PortfolioProject portfolioProject = new PortfolioProject();
            
            portfolioProject.LoadById(portfolioProjects.FirstOrDefault(p=>p.ProjectId == project.Id && p.PortfolioId == portfolio.Id).Id);
            
            Assert.AreEqual(portfolioProject.PortfolioId, portfolioProject.PortfolioId);
        }

        [TestMethod]
        public void Update()
        {
            ProjectList projects = new ProjectList();
            projects.Load();
            Project project = projects.FirstOrDefault(p => p.Name == "ProveIT");
            PortfolioList portfolios = new PortfolioList();
            portfolios.Load();
            Portfolio portfolio = portfolios.FirstOrDefault(p => p.Name == "Brodys First Portfolio");
            Portfolio portfolioUpdate = portfolios.FirstOrDefault(p => p.Name == "Joes First Portfolio");


            PortfolioProjectList portfolioProjects = new PortfolioProjectList();
            portfolioProjects.Load();
            PortfolioProject portfolioProject = new PortfolioProject();
            portfolioProject.LoadById(portfolioProjects.FirstOrDefault(p => p.ProjectId == project.Id && p.PortfolioId == portfolio.Id).Id);

            portfolioProject.PortfolioId = portfolioUpdate.Id;
            int rowsAffected = portfolioProject.Update();

            Assert.IsTrue(rowsAffected == 1);
        }

        [TestMethod]
        public void Delete()
        {
            ProjectList projects = new ProjectList();
            projects.Load();
            Project project = projects.FirstOrDefault(p => p.Name == "ProveIT");
            PortfolioList portfolios = new PortfolioList();
            portfolios.Load();
            Portfolio portfolio = portfolios.FirstOrDefault(p => p.Name == "Joes First Portfolio");

            PortfolioProjectList portfolioProjects = new PortfolioProjectList();
            portfolioProjects.Load();
            PortfolioProject portfolioProject = new PortfolioProject();
            portfolioProject.LoadById(portfolioProjects.FirstOrDefault(p => p.ProjectId == project.Id && p.PortfolioId == portfolio.Id).Id);

            int rowsAffected = portfolioProject.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }
    }
}
