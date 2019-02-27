/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--Default Data for tables that don't rely on other tables
:r DefaultData\UserTypes.sql
:r DefaultData\Languages.sql
:r DefaultData\Privacies.sql
:r DefaultData\Statuses.sql

--Relies on UserTypes
:r DefaultData\Users.sql

--Relies on users
:r DefaultData\Portfolios.sql

--Relies on users, privacies and statuses
:r DefaultData\Projects.sql

--Relies on projects and portfolios
:r DefaultData\PortfolioProjects.sql

--Relies on Projects and Languages
:r DefaultData\ProjectLanguages.sql

--Relies on Projects
:r DefaultData\Screenshots.sql