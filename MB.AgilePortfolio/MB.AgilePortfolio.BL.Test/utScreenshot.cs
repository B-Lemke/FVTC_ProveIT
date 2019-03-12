using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.BL;
using System.Linq;

namespace MB.AgilePortfolio.BL.Test
{
    [TestClass]
    public class utScreenshot
    {


        [TestMethod]
        public void Load()
        {
            ScreenshotList screenshots = new ScreenshotList();
            screenshots.Load();

            Assert.IsTrue(screenshots.Count > 0);
        }

        [TestMethod]
        public void Insert()
        {
            Project project = LoadProject();

            Screenshot screenshot = new Screenshot()
            {
                Filepath = "Test",
                ProjectId = project.Id
            };

            int rowsInserted = screenshot.Insert();

            Assert.IsTrue(rowsInserted == 1);


        }

        [TestMethod]
        public void LoadById()
        {
            ScreenshotList screenshots = new ScreenshotList();
            screenshots.Load();
            Screenshot screenshot = new Screenshot();

            screenshot.LoadById(screenshots.FirstOrDefault(s => s.Filepath == "Test").Id);

            Assert.IsNotNull(screenshot);
        }

        [TestMethod]
        public void Update()
        {

            ScreenshotList screenshots = new ScreenshotList();
            screenshots.Load();
            Screenshot screenshot = new Screenshot();

            screenshot.LoadById(screenshots.FirstOrDefault(s => s.Filepath == "Test").Id);

            screenshot.Filepath = "UpdateTest";
            int rowsAffected = screenshot.Update();

            Assert.IsTrue(rowsAffected == 1);
        }

        [TestMethod]
        public void Delete()
        {
            ScreenshotList screenshots = new ScreenshotList();
            screenshots.Load();
            Screenshot screenshot = new Screenshot();

            screenshot.LoadById(screenshots.FirstOrDefault(s => s.Filepath == "UpdateTest").Id);

            int rowsAffected = screenshot.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }



        private static Project LoadProject()
        {
            ProjectList projects = new ProjectList();
            projects.Load();
            Project project = projects.FirstOrDefault(p => p.Name == "ProveIT");
            return project;
        }
    }
}
