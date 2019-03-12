using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.BL;
using System.Linq;

namespace MB.AgilePortfolio.BL.Test
{
    [TestClass]
    public class utStatus
    {
        

        [TestMethod]
        public void Load()
        {
            StatusList statuses = new StatusList();
            statuses.Load();

            Assert.IsTrue(statuses.Count > 0);
        }

        [TestMethod]
        public void Insert()
        {
            Status status = new Status()
            {
                Description = "Test"
            };

            int rowsInserted = status.Insert();

            Assert.IsTrue(rowsInserted == 1);

            
        }

        [TestMethod]
        public void LoadById()
        {
            StatusList statuses = new StatusList();
            statuses.Load();
            Status status = new Status();

            
            status.LoadById(statuses.FirstOrDefault(s=>s.Description == "Test").Id);

            Assert.AreEqual("Test", status.Description);
        }

        [TestMethod]
        public void Update()
        {
            StatusList statuses = new StatusList();
            statuses.Load();
            
            Status status = statuses.FirstOrDefault(s => s.Description == "Test");

            status.Description = "TestUpdate";
            int rowsAffected = status.Update();

            Assert.IsTrue(rowsAffected == 1);
        }

        [TestMethod]
        public void Delete()
        {
            StatusList statuses = new StatusList();
            statuses.Load();
            
            Status status = statuses.FirstOrDefault(s => s.Description == "TestUpdate");

            int rowsAffected = status.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }
    }
}
