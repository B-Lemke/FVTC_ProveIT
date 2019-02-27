CREATE PROCEDURE [dbo].[Portfolio_ViewData]

AS
	SELECT p.Id, p.[Name], p.[Description], p.PortfolioImage, u.Email, u.FirstName, u.LastName 
	FROM [dbo].tblPortfolio p
	JOIN [dbo].tblUser u
	ON u.Id = p.UserId;
RETURN 0
