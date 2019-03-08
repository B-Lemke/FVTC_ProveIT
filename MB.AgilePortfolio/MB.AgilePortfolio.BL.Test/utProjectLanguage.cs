using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MB.AgilePortfolio.BL;

namespace MB.AgileProjectLanguage.BL.Test
{
    [TestClass]
    public class utProjectLanguage
    {


        [TestMethod]
        public void Load()
        {
            ProjectLanguageList projectLanguages = new ProjectLanguageList();
            projectLanguages.Load();

            int expected = 8;

            Assert.AreEqual(expected, projectLanguages.Count);
        }

        [TestMethod]
        public void Insert()
        {
            ProjectLanguage projectLanguage = new ProjectLanguage()
            {
                LanguageId = Guid.Parse("11112222-3333-4444-5555-666677778888"),
                ProjectId = Guid.Parse("11112222-3333-4444-5555-666677778888")
            };

            int rowsInserted = projectLanguage.Insert();

            Assert.IsTrue(rowsInserted == 1);


        }

        [TestMethod]
        public void LoadById()
        {
            ProjectLanguageList projectLanguages = new ProjectLanguageList();
            projectLanguages.Load();
            ProjectLanguage projectLanguage = new ProjectLanguage();

            Guid languageGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            projectLanguage.LoadById(projectLanguages.FirstOrDefault(p => p.LanguageId == languageGuid).Id);

            Assert.AreEqual(languageGuid, projectLanguage.LanguageId);
        }

        [TestMethod]
        public void Update()
        {
            ProjectLanguageList projectLanguages = new ProjectLanguageList();
            projectLanguages.Load();
            ProjectLanguage projectLanguage = new ProjectLanguage();

            Guid languageGuid = Guid.Parse("11112222-3333-4444-5555-666677778888");
            projectLanguage.LoadById(projectLanguages.FirstOrDefault(p => p.LanguageId == languageGuid).Id);

            projectLanguage.LanguageId = Guid.Parse("99999999-9999-9999-9999-999999999999");
            int rowsAffected = projectLanguage.Update();

            Assert.IsTrue(rowsAffected == 1);
        }

        [TestMethod]
        public void Delete()
        {
            ProjectLanguageList projectLanguages = new ProjectLanguageList();
            projectLanguages.Load();
            ProjectLanguage projectLanguage = new ProjectLanguage();

            Guid languageGuid = Guid.Parse("99999999-9999-9999-9999-999999999999");
            projectLanguage.LoadById(projectLanguages.FirstOrDefault(p => p.LanguageId == languageGuid).Id);

            int rowsAffected = projectLanguage.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }
    }
}
