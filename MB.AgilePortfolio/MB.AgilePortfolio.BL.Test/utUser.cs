using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.BL;
using System.Linq;

namespace MB.AgilePortfolio.BL.Test
{
    [TestClass]
    public class utUser
    {
        

        [TestMethod]
        public void Load()
        {
            UserList users = new UserList();
            users.Load();

            int expected = 8;

            Assert.AreEqual(expected, users.Count);
        }

        [TestMethod]
        public void Insert()
        {
            User user = new User()
            {
                Email = "Test@test.test",
                FirstName = "Test",
                LastName = "Test",
                Password = "Test",
                ProfileImage = "Test",
                UserTypeId = Guid.Parse("11112222-3333-4444-5555-666677778888")
            };

            int rowsInserted = user.Insert();

            Assert.IsTrue(rowsInserted == 1);

            
        }

        [TestMethod]
        public void LoadById()
        {
            UserList users = new UserList();
            users.Load();
            User user = new User();

            Guid userTypeGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            user.LoadById(users.FirstOrDefault(u=>u.UserTypeId == userTypeGuid).Id);

            Assert.AreEqual("Test", user.FirstName);
        }

        [TestMethod]
        public void Update()
        {
            UserList users = new UserList();
            users.Load();
            Guid userTypeGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            User user = users.FirstOrDefault(u => u.UserTypeId == userTypeGuid);

            user.FirstName = "TestUpdate";
            int rowsAffected = user.Update();

            Assert.IsTrue(rowsAffected == 1);
        }

        [TestMethod]
        public void Delete()
        {
            UserList users = new UserList();
            users.Load();
            Guid userTypeGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            User user = users.FirstOrDefault(u => u.UserTypeId == userTypeGuid);

            int rowsAffected = user.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }
    }
}
