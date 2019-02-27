CREATE PROCEDURE [dbo].[ProjectLanguages_ViewData]

AS
	SELECT pl.Id, p.[Name] AS 'Project', l.[Description] AS 'Language'
	FROM [dbo].tblProjectLanguage pl
	JOIN [dbo].tblProject p
	ON p.Id = pl.ProjectId
	JOIN [dbo].tblLanguage l
	ON l.Id = pl.LanguageId
RETURN 0
