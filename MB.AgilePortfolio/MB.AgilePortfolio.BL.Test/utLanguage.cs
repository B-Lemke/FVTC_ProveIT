using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.AgilePortfolio.BL;
using System.Linq;

namespace MB.AgilePortfolio.BL.Test
{
    [TestClass]
    public class utLanguage
    {
        

        [TestMethod]
        public void Load()
        {
            LanguageList languages = new LanguageList();
            languages.Load();

            Assert.IsTrue(languages.Count > 0);
        }

        [TestMethod]
        public void Insert()
        {
            Language language = new Language()
            {
                Description = "Test"
            };

            int rowsInserted = language.Insert();

            Assert.IsTrue(rowsInserted == 1);

            
        }

        [TestMethod]
        public void LoadById()
        {
            LanguageList languages = new LanguageList();
            languages.Load();

            Language language = new Language();
            language.LoadById(languages.FirstOrDefault(l=>l.Description == "Test").Id);

            Assert.AreEqual("Test", language.Description);
        }

        [TestMethod]
        public void Update()
        {
            LanguageList languages = new LanguageList();
            languages.Load();

            Language language = languages.FirstOrDefault(l => l.Description == "Test");

            language.Description = "TestUpdate";
            int rowsAffected = language.Update();

            Assert.IsTrue(rowsAffected == 1);
        }

        [TestMethod]
        public void Delete()
        {
            LanguageList languages = new LanguageList();
            languages.Load();

            Language language = languages.FirstOrDefault(l => l.Description == "TestUpdate");

            int rowsAffected = language.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }
    }
}
