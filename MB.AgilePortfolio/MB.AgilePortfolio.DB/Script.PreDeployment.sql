/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
drop table if exists tblLanguage
drop table if exists tblPortfolio
drop table if exists tblPortfolioProject
drop table if exists tblPrivacy
drop table if exists tblProject
drop table if exists tblProjectLanguage
drop table if exists tblScreenshot
drop table if exists tblUser
drop table if exists tblUserType