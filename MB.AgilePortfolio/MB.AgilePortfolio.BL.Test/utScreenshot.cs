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

            int expected = 8;

            Assert.AreEqual(expected, screenshots.Count);
        }

        [TestMethod]
        public void Insert()
        {
            Screenshot screenshot = new Screenshot()
            {
                Filepath = "Test",
                ProjectId = Guid.Parse("11112222-3333-4444-5555-666677778888")
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

            Guid projectGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            screenshot.LoadById(screenshots.FirstOrDefault(s => s.ProjectId == projectGuid).Id);

            Assert.AreEqual("Test", screenshot.Filepath);
        }

        [TestMethod]
        public void Update()
        {
            ScreenshotList screenshots = new ScreenshotList();
            screenshots.Load();
            Screenshot screenshot = new Screenshot();

            Guid projectGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            screenshot.LoadById(screenshots.FirstOrDefault(s => s.ProjectId == projectGuid).Id);

            screenshot.Filepath = "NewPath";
            int rowsAffected = screenshot.Update();

            Assert.IsTrue(rowsAffected == 1);
        }

        [TestMethod]
        public void Delete()
        {
            ScreenshotList screenshots = new ScreenshotList();
            screenshots.Load();
            Screenshot screenshot = new Screenshot();

            Guid projectGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            screenshot.LoadById(screenshots.FirstOrDefault(s => s.ProjectId == projectGuid).Id);

            int rowsAffected = screenshot.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }
    }
}
