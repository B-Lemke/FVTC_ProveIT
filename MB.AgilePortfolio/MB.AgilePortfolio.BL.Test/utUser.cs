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

            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        public void Insert()
        {
            UserType userType = LoadUserType();

            User user = new User()
            {
                Email = "Test@test.test",
                FirstName = "Test",
                LastName = "Test",
                Password = "Test",
                ProfileImage = "Test",
                UserTypeId = userType.Id
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

            user.LoadById(users.FirstOrDefault(u=>u.Email == "Test@test.test").Id);

            Assert.AreEqual("Test", user.FirstName);
        }

        [TestMethod]
        public void Update()
        {
            UserList users = new UserList();
            users.Load();


            User user = new User();
            user.LoadById(users.FirstOrDefault(u => u.Email == "Test@test.test").Id);

            user.FirstName = "TestUpdate";
            int rowsAffected = user.Update();

            Assert.IsTrue(rowsAffected == 1);
        }

        [TestMethod]
        public void Delete()
        {
            UserList users = new UserList();
            users.Load();

            User user = new User();
            user.LoadById(users.FirstOrDefault(u => u.Email == "Test@test.test").Id);

            int rowsAffected = user.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }



        private static UserType LoadUserType()
        {
            UserTypeList uts = new UserTypeList();
            uts.Load();
            UserType ut = uts.FirstOrDefault(p => p.Description == "User");
            return ut;
        }
    }
}
