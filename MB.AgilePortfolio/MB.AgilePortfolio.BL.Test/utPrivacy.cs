using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.BL;
using System.Linq;

namespace MB.AgilePortfolio.BL.Test
{
    [TestClass]
    public class utPrivacy
    {
        

        [TestMethod]
        public void Load()
        {
            PrivacyList privacys = new PrivacyList();
            privacys.Load();

            Assert.IsTrue(privacys.Count > 0);
        }

        [TestMethod]
        public void Insert()
        {
            Privacy privacy = new Privacy()
            {
                Description = "Test"
            };

            int rowsInserted = privacy.Insert();

            Assert.IsTrue(rowsInserted == 1);

            
        }

        [TestMethod]
        public void LoadById()
        {
            PrivacyList privacys = new PrivacyList();
            privacys.Load();
            Privacy privacy = new Privacy();

            
            privacy.LoadById(privacys.FirstOrDefault(p=>p.Description == "Test").Id);

            Assert.AreEqual("Test", privacy.Description);
        }

        [TestMethod]
        public void Update()
        {
            PrivacyList privacys = new PrivacyList();
            privacys.Load();
            
            Privacy privacy = privacys.FirstOrDefault(p => p.Description == "Test");

            privacy.Description = "TestUpdate";
            int rowsAffected = privacy.Update();

            Assert.IsTrue(rowsAffected == 1);
        }

        [TestMethod]
        public void Delete()
        {
            PrivacyList privacys = new PrivacyList();
            privacys.Load();

            Privacy privacy = privacys.FirstOrDefault(p => p.Description == "TestUpdate");

            int rowsAffected = privacy.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }
    }
}
