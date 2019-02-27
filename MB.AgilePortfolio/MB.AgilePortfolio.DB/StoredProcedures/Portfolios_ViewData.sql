CREATE PROCEDURE [dbo].[Portfolio_ViewData]
	@ProjectId UNIQUEIDENTIFIER = null
AS
	IF (@ProjectId = null)
		SELECT p.Id, p.[Name], p.[Description], p.PortfolioImage, u.Email, u.FirstName, u.LastName 
		FROM [dbo].tblPortfolio p
		JOIN [dbo].tblUser u
		ON u.Id = p.UserId;

	--Optional functionality to retrieve a specific portfolio's information when supplied with an Id
	ELSE 
		SELECT p.Id, p.[Name], p.[Description], p.PortfolioImage, u.Email, u.FirstName, u.LastName 
		FROM [dbo].tblPortfolio p
		JOIN [dbo].tblUser u
		ON u.Id = p.UserId
		WHERE p.Id = @ProjectId;
RETURN 0
