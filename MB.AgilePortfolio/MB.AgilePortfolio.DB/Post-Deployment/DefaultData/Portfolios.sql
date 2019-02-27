BEGIN
	DECLARE @BrodyUserId UNIQUEIDENTIFIER 
	DECLARE @JoeUserId UNIQUEIDENTIFIER 

	SELECT @BrodyUserId = Id FROM [dbo].tblUser WHERE Email = 'blemke4@gmail.com';
	SELECT @JoeUserId = Id FROM [dbo].tblUser WHERE Email = 'joe@wetzel.com';

	INSERT INTO [dbo].tblPortfolio(Id, [Name], [Description], PortfolioImage, UserId )
	VALUES

	(newid(), 'Brodys First Portfolio', 'A portfolio of my favorite projects',  null , @BrodyUserId),
	(newid(), 'Joes First Portfolio', 'An empty portfolio that I will fill later!',  null , @JoeUserId)
END