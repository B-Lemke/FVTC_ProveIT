BEGIN
	DECLARE @PortfolioId UNIQUEIDENTIFIER 
	DECLARE @OfficeProjectId UNIQUEIDENTIFIER 
	DECLARE @ProveITProjectId UNIQUEIDENTIFIER 

	SELECT @PortfolioId = Id FROM [dbo].tblPortfolio WHERE [Name] = 'Brodys First Portfolio';
	SELECT @OfficeProjectId = Id FROM [dbo].tblProject WHERE [Name] = 'Office Quote Daily Emailer';
	SELECT @ProveITProjectId = Id FROM [dbo].tblProject WHERE [Name] = 'ProveIT';

	INSERT INTO [dbo].tblPortfolioProject(Id, PortfolioId, ProjectId )
	VALUES
	(newid(), @PortfolioId, @OfficeProjectId),
	(newid(), @PortfolioId, @ProveITProjectId)
END
GO