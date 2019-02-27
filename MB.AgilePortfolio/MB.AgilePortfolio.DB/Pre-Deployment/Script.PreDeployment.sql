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
DROP TABLE IF EXISTS [dbo].tblLanguage
DROP TABLE IF EXISTS [dbo].tblPortfolio
DROP TABLE IF EXISTS [dbo].tblPortfolioProject
DROP TABLE IF EXISTS [dbo].tblPrivacy
DROP TABLE IF EXISTS [dbo].tblProject
DROP TABLE IF EXISTS [dbo].tblProjectLanguage
DROP TABLE IF EXISTS [dbo].tblScreenshot
DROP TABLE IF EXISTS [dbo].tblUser
DROP TABLE IF EXISTS [dbo].tblUserType