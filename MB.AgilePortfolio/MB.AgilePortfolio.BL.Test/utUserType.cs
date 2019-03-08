using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.BL;
using System.Linq;

namespace MB.AgilePortfolio.BL.Test
{
    [TestClass]
    public class utUserType
    {
        

        [TestMethod]
        public void Load()
        {
            UserTypeList userTypes = new UserTypeList();
            userTypes.Load();

            int expected = 8;

            Assert.AreEqual(expected, userTypes.Count);
        }

        [TestMethod]
        public void Insert()
        {
            UserType userTypeType = new UserType()
            {
                Description = "Test"
            };

            int rowsInserted = userTypeType.Insert();

            Assert.IsTrue(rowsInserted == 1);

            
        }

        [TestMethod]
        public void LoadById()
        {
            UserTypeList userTypes = new UserTypeList();
            userTypes.Load();
            UserType userTypeType = new UserType();

            userTypeType.LoadById(userTypes.FirstOrDefault(u=>u.Description == "Test").Id);

            Assert.AreEqual("Test", userTypeType.Description);
        }

        [TestMethod]
        public void Update()
        {
            UserTypeList userTypes = new UserTypeList();
            userTypes.Load();

            UserType userTypeType = userTypes.FirstOrDefault(u => u.Description == "Test");

            userTypeType.Description = "TestUpdate";
            int rowsAffected = userTypeType.Update();

            Assert.IsTrue(rowsAffected == 1);
        }

        [TestMethod]
        public void Delete()
        {
            UserTypeList userTypes = new UserTypeList();
            userTypes.Load();

            UserType userTypeType = userTypes.FirstOrDefault(u => u.Description == "TestUpdate");

            int rowsAffected = userTypeType.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }
    }
}
