using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using MB.AgilePortfolio.BL;
using MB.AgilePortfolio.MVCUI.Models;
using MB.AgilePortfolio.MVCUI.ViewModels;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace MB.AgilePortfolio.MVCUI.Controllers
{
    public class SearchController : Controller
    {

        public ActionResult Index(string option, string search)
        {
            ProjectLanguages pl = new ProjectLanguages
            {
                User = new User(),
                Projects = new ProjectList(),
                Languages = new LanguageList(),
                projectLanguages = new ProjectLanguageList(),
                Portfolios = new PortfolioList(),
                Users = new UserList()
            };
            //if a user choose the radio button option as Subject  
            if (option == "Language")
            {
                // Currently searching by language only returns projects
                // THIS MAY CHANGE IF ADVANCED SEARCH OPTIONS ARE ADDED (IE: search portfolios by languages used [languages in all projects in a Portfolio])
                ViewBag.ReturnObject = "Projects";

                //Load Projects by input search string exact matches
                pl.projectLanguages.LoadByLanguageName(search);
                if(pl.projectLanguages.Count > 0)
                {
                    // Found at least one match

                    foreach(ProjectLanguage projlang in pl.projectLanguages)
                    {
                        Project proj = new Project();
                        proj.LoadById(projlang.ProjectId);
                        pl.User.LoadById(proj.UserId);
                        proj.CreatorUserName = pl.User.Username;
                        pl.Projects.Add(proj);
                    }
                }
                else
                {
                    //No exact matches found

                    //Load Projects by input search string partial matches
                    pl.projectLanguages.LoadByPartialLanguageName(search);
                    foreach (ProjectLanguage projlang in pl.projectLanguages)
                    {
                        Project proj = new Project();
                        proj.LoadById(projlang.ProjectId);
                        pl.Projects.Add(proj);
                    }
                }
                if(pl.Projects.Count < 1)
                {
                    //Throw Error Message for no projects loaded
                    ViewBag.ErrorMessage = "No Projects Found";
                }
                return View(pl);
            }
            else if (option == "Project")
            {
                // Currently searching by Project only returns projects
                // THIS MAY CHANGE IF ADVANCED SEARCH OPTIONS ARE ADDED (IE: search portfolios by languages used [languages in all projects in a Portfolio])
                ViewBag.ReturnObject = "Projects";

                //Load Projects by input search string exact matches
                pl.Projects.LoadByProjectName(search);
                if (pl.Projects.Count > 0)
                {
                    // Found at least one match
                }
                else
                {
                    //No exact matches found

                    //Load Projects by input search string partial matches
                    pl.Projects.LoadByPartialProjectName(search);
                }

                if (pl.Projects.Count < 1)
                {
                    //Throw Error Message for no projects loaded
                    ViewBag.ErrorMessage = "No Projects Found";
                }
                return View(pl);
            }
            else if (option == "Portfolio")
            {
                // Currently searching by Portfolio only returns Portfolios
                // THIS MAY CHANGE IF ADVANCED SEARCH OPTIONS ARE ADDED (IE: search portfolios by languages used [languages in all projects in a Portfolio])
                ViewBag.ReturnObject = "Portfolios";

                //Load Portfolios by input search string exact matches
                pl.Portfolios.LoadByPortfolioName(search);
                if (pl.Portfolios.Count > 0)
                {
                    // Found at least one match
                }
                else
                {
                    //No exact matches found

                    //Load Portfolios by input search string partial matches
                    pl.Portfolios.LoadByPartialPortfolioName(search);
                }

                if (pl.Portfolios.Count < 1)
                {
                    //Throw Error Message for no portfolios loaded
                    ViewBag.ErrorMessage = "No Portfolios Found";
                }
                else
                {
                    // Portfolios returning logic
                }
                return View(pl);
            }
            else if (option == "Profile")
            {
                // Currently searching by UserProfiles only returns UserProfiles
                // THIS MAY CHANGE IF ADVANCED SEARCH OPTIONS ARE ADDED (IE: search portfolios by languages used [languages in all projects in a Portfolio])
                ViewBag.ReturnObject = "Profiles";

                //Load UserProfiles by input search string exact matches
                pl.Users.LoadByUserName(search);
                if (pl.Users.Count > 0)
                {
                    // Found at least one match
                }
                else
                {
                    //No exact matches found

                    //Load UserProfiles by input search string partial matches
                    pl.Users.LoadByPartialUserName(search);
                }

                if (pl.Users.Count < 1)
                {
                    //Throw Error Message for no UserProfiles loaded
                    ViewBag.ErrorMessage = "No Profiles Found";
                }
                return View(pl);
            }
            else
            {
                return View(pl);
            }
        }


    }
}
