CREATE PROCEDURE [dbo].[Screenshots_ViewData]
AS
	SELECT s.Id, s.Filepath, p.[Name]
	FROM [dbo].tblScreenshot s
	JOIN [dbo].tblProject p
	ON p.Id = s.ProjectId
RETURN 0
