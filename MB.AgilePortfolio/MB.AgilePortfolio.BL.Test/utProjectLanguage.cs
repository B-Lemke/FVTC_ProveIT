using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MB.AgilePortfolio.BL;

namespace MB.AgilePortfolio.BL.Test
{
    [TestClass]
    public class utProjectLanguage
    {

        [TestMethod]
        public void Load()
        {
            ProjectLanguageList projectLanguages = new ProjectLanguageList();
            projectLanguages.Load();

            Assert.IsTrue(projectLanguages.Count > 0);
        }

        [TestMethod]
        public void Insert()
        {
            Language language = LoadLanguage("ABAP");
            Project project = LoadProject();

            ProjectLanguage projectLanguage = new ProjectLanguage()
            {
                LanguageId = language.Id,
                ProjectId = project.Id
            };

            int rowsInserted = projectLanguage.Insert();

            Assert.IsTrue(rowsInserted == 1);


        }



        [TestMethod]
        public void LoadById()
        {
            Language language = LoadLanguage("ABAP");
            Project project = LoadProject();


            ProjectLanguageList projectLanguages = new ProjectLanguageList();
            projectLanguages.Load();
            ProjectLanguage projectLanguage = new ProjectLanguage();

            projectLanguage.LoadById(projectLanguages.FirstOrDefault(p => p.LanguageId == language.Id && p.ProjectId == project.Id).Id);

            Assert.IsNotNull(projectLanguage);
        }

        [TestMethod]
        public void Update()
        {
            Language language = LoadLanguage("ABAP");
            Language languageUpdate = LoadLanguage("MATLAB");
            Project project = LoadProject();

            ProjectLanguageList projectLanguages = new ProjectLanguageList();
            projectLanguages.Load();
            ProjectLanguage projectLanguage = new ProjectLanguage();


            projectLanguage.LoadById(projectLanguages.FirstOrDefault(p => p.LanguageId == language.Id && p.ProjectId == project.Id).Id);

            projectLanguage.LanguageId = languageUpdate.Id;
            int rowsAffected = projectLanguage.Update();

            Assert.IsTrue(rowsAffected == 1);
        }

        [TestMethod]
        public void Delete()
        {
            Language language = LoadLanguage("MATLAB");
            Project project = LoadProject();


            ProjectLanguageList projectLanguages = new ProjectLanguageList();
            projectLanguages.Load();
            ProjectLanguage projectLanguage = new ProjectLanguage();

            projectLanguage.LoadById(projectLanguages.FirstOrDefault(p => p.LanguageId == language.Id && p.ProjectId == project.Id).Id);

            int rowsAffected = projectLanguage.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }




        private static Project LoadProject()
        {
            ProjectList projects = new ProjectList();
            projects.Load();
            Project project = projects.FirstOrDefault(p => p.Name == "ProveIT");
            return project;
        }

        private Language LoadLanguage(string langName)
        {
            LanguageList languages = new LanguageList();
            languages.Load();
            Language language = languages.FirstOrDefault(l => l.Description == langName);
            return language;
        }
    }
}
