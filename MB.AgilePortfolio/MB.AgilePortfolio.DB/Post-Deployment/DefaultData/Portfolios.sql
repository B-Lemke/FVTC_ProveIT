BEGIN
	DECLARE @BrodyUserId UNIQUEIDENTIFIER 
	DECLARE @JoeUserId UNIQUEIDENTIFIER 

	DECLARE @PrivacyIdPublicPort UNIQUEIDENTIFIER 
	SELECT @PrivacyIdPublicPort = Id FROM [dbo].tblPrivacy WHERE Description = 'Public';

	SELECT @BrodyUserId = Id FROM [dbo].tblUser WHERE Email = 'blemke4@gmail.com';
	SELECT @JoeUserId = Id FROM [dbo].tblUser WHERE Email = 'joe@wetzel.com';

	INSERT INTO [dbo].tblPortfolio(Id, [Name], [Description], PortfolioImage, UserId, PrivacyId )
	VALUES

	(newid(), 'Brodys First Portfolio', 'A portfolio of my favorite projects',  null , @BrodyUserId, @PrivacyIdPublicPort),
	(newid(), 'Joes First Portfolio', 'An empty portfolio that I will fill later!',  null , @JoeUserId, @PrivacyIdPublicPort)
END