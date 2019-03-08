using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.PL;
using System.Linq;


namespace MB.AgilePortfolio.PL.Test
{
    [TestClass]
    public class utProject
    {
        [TestMethod]
        public void LoadTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Ensure that at least 1 row is loaded from the database
                Assert.IsTrue(dc.tblProjects.Count() > 0);
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                //Create a user
                tblProject project = new tblProject
                {
                    //User a GUID for testing purposes
                    Id = Guid.Parse("11112222-3333-4444-5555-666677778888"),
                    Name = "Test",
                    Challenges = "Test",
                    Collaborators = "Test",
                    DateCreated = DateTime.Now,
                    Description = "Test",
                    Environment = "Test",
                    Filepath = "Test",
                    FuturePlans = "Test",
                    Image = "Test",
                    LastUpdated = DateTime.Now,
                    Location = "Test",
                    Purpose = "Test",
                    SoftwareUsed = "Test",
                    PrivacyId = Guid.NewGuid(),
                    StatusId = Guid.NewGuid(),
                    UserId = Guid.NewGuid()
            };

                //Add the user to the database
                dc.tblProjects.Add(project);

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
                Guid projectGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");

                tblProject project = dc.tblProjects.FirstOrDefault(p => p.Id == projectGuid);

                Guid privacyGuid = Guid.Parse("88887777-6666-5555-4444-333322221111");
                project.PrivacyId = privacyGuid;

                //Save changes and get it back out
                dc.SaveChanges();
                tblProject updatedProject = dc.tblProjects.FirstOrDefault(p => p.PrivacyId == privacyGuid);
                //Make sure the Ids match
                Assert.AreEqual(project.Id, updatedProject.Id);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (PortfolioEntities dc = new PortfolioEntities())
            {
                Guid projectGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
                tblProject project = dc.tblProjects.FirstOrDefault(p => p.Id == projectGuid);

                dc.tblProjects.Remove(project);

                dc.SaveChanges();

                tblProject deletedProject =  dc.tblProjects.FirstOrDefault(p => p.Id == projectGuid);

                Assert.IsNull(deletedProject);
            }
        }

    }
}
