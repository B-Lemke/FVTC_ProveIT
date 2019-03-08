using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.BL;
using System.Linq;

namespace MB.AgilePortfolio.BL.Test
{
    [TestClass]
    public class utProject
    {


        [TestMethod]
        public void Load()
        {
            ProjectList projects = new ProjectList();
            projects.Load();

            int expected = 8;

            Assert.AreEqual(expected, projects.Count);
        }

        [TestMethod]
        public void Insert()
        {
            Project project = new Project()
            {
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
                PrivacyId = Guid.Parse("11112222-3333-4444-5555-666677778888"),
                StatusId = Guid.Parse("11112222-3333-4444-5555-666677778888"),
                UserId = Guid.Parse("11112222-3333-4444-5555-666677778888"),
            };

            int rowsInserted = project.Insert();

            Assert.IsTrue(rowsInserted == 1);


        }

        [TestMethod]
        public void LoadById()
        {
            ProjectList projects = new ProjectList();
            projects.Load();
            Project project = new Project();

            Guid userGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            project.LoadById(projects.FirstOrDefault(p => p.UserId == userGuid).Id);

            Assert.AreEqual("Test", project.Name);
        }

        [TestMethod]
        public void Update()
        {
            ProjectList projects = new ProjectList();
            projects.Load();
            Project project = new Project();

            Guid userGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            project.LoadById(projects.FirstOrDefault(p => p.UserId == userGuid).Id);

            project.Name = "TestUpdate";
            int rowsAffected = project.Update();

            Assert.IsTrue(rowsAffected == 1);
        }

        [TestMethod]
        public void Delete()
        {
            ProjectList projects = new ProjectList();
            projects.Load();
            Project project = new Project();

            Guid userGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            project.LoadById(projects.FirstOrDefault(p => p.UserId == userGuid).Id);

            int rowsAffected = project.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }
    }
}
