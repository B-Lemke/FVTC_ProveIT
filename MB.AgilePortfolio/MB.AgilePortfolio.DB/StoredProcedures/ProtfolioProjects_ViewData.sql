CREATE PROCEDURE [dbo].[ProtfolioProjects_ViewData]
AS
	SELECT pp.Id, proj.[Name], po.[Name] 
	FROM [dbo].tblPortfolioProject pp
	JOIN [dbo].tblPortfolio po
	ON po.Id = pp.PortfolioId
	JOIN [dbo].tblProject proj
	ON proj.Id = pp.ProjectId
RETURN 0
