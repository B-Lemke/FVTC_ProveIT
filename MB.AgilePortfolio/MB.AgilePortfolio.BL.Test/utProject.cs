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

            Assert.IsTrue(projects.Count > 0);
        }

        [TestMethod]
        public void Insert()
        {
            //Get guids for data in the database so that things load properly
            Privacy privacy = LoadPrivacy();
            Status status = LoadStatus();
            User user = LoadUser();

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
                PrivacyId = privacy.Id,
                StatusId = status.Id,
                UserId = user.Id,
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

            //DEFAULTS
            //project.LoadById(projects.FirstOrDefault(p => p.Name == "Test").Id);

            // Assert.AreEqual("Test", project.Description);

            //TESTING CREATE NEW PROJECT INSERT
            project.LoadById(Guid.Parse("797c8d2b-d1ba-4bba-825d-eb28bab26cf8"));

             Assert.AreEqual("111", project.Description);
        }

        [TestMethod]
        public void Update()
        {
            ProjectList projects = new ProjectList();
            projects.Load();
            Project project = new Project();

            project.LoadById(projects.FirstOrDefault(p => p.Name == "Test").Id);

            project.Description = "TestUpdate";
            int rowsAffected = project.Update();

            Assert.IsTrue(rowsAffected == 1);
        }

        [TestMethod]
        public void Delete()
        {
            ProjectList projects = new ProjectList();
            projects.Load();
            Project project = new Project();

            project.LoadById(projects.FirstOrDefault(p => p.Name == "Test").Id);

            int rowsAffected = project.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }

        private static User LoadUser()
        {
            UserList users = new UserList();
            users.Load();
            User user = users.FirstOrDefault(p => p.Email == "blemke4@gmail.com");
            return user;
        }

        private static Status LoadStatus()
        {
            StatusList statuses = new StatusList();
            statuses.Load();
            Status status = statuses.FirstOrDefault(p => p.Description == "Completed");
            return status;
        }

        private static Privacy LoadPrivacy()
        {
            PrivacyList privacies = new PrivacyList();
            privacies.Load();
            Privacy privacy = privacies.FirstOrDefault(p => p.Description == "Public");
            return privacy;
        }
    }






}
