using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.PL;
using System.Linq;


namespace MB.AgilePortfolio.PL.Test
{
    [TestClass]
    public class utPortfolioProject
    {
        [TestMethod]
        public void LoadTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Ensure that at least 1 row is loaded from the database
                Assert.IsTrue(dc.tblPortfolioProjects.Count() > 0);
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Create a portfolioProject
                tblPortfolioProject portfolioProject = new tblPortfolioProject
                {
                    //PortfolioProject a GUID for testing purposes
                    Id = Guid.Parse("11112222-3333-4444-5555-666677778888"),
                    PortfolioId = Guid.NewGuid(),
                    ProjectId = Guid.NewGuid()
                };

                //Add the portfolioProject to the database
                dc.tblPortfolioProjects.Add(portfolioProject);

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
                //Retrieve test portfolioProject based on ID and update it
                Guid portfolioProjectGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");

                tblPortfolioProject portfolioProject = dc.tblPortfolioProjects.FirstOrDefault(p => p.Id == portfolioProjectGuid);

                Guid projectGuid = Guid.Parse("88887777-6666-5555-4444-333322221111");
                portfolioProject.ProjectId = projectGuid;

                //Save changes and get it back out
                dc.SaveChanges();
                tblPortfolioProject updatedPortfolioProject = dc.tblPortfolioProjects.FirstOrDefault(p => p.ProjectId == projectGuid);
                //Make sure the Ids match
                Assert.AreEqual(portfolioProject.Id, updatedPortfolioProject.Id);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                Guid portfolioProjectGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
                tblPortfolioProject portfolioProject = dc.tblPortfolioProjects.FirstOrDefault(p => p.Id == portfolioProjectGuid);

                dc.tblPortfolioProjects.Remove(portfolioProject);

                dc.SaveChanges();

                tblPortfolioProject deletedPortfolioProject =  dc.tblPortfolioProjects.FirstOrDefault(p => p.Id == portfolioProjectGuid);

                Assert.IsNull(deletedPortfolioProject);
            }
        }

    }
}
