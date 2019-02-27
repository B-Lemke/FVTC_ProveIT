BEGIN
	DECLARE @UserIdBrody UNIQUEIDENTIFIER 
	DECLARE @PrivacyIdPublic UNIQUEIDENTIFIER 
	DECLARE @StatusIdCompleted UNIQUEIDENTIFIER 
	DECLARE @StatusIdInProgress UNIQUEIDENTIFIER 

	SELECT @UserIdBrody = Id FROM [dbo].tblUser WHERE Email = 'blemke4@gmail.com';
	SELECT @PrivacyIdPublic = Id FROM [dbo].tblPrivacy WHERE Description = 'Public';
	SELECT @StatusIdCompleted = Id FROM [dbo].tblStatus WHERE Description = 'Completed';
	SELECT @StatusIdInProgress = Id FROM [dbo].tblStatus WHERE Description = 'In Progress';

	INSERT INTO [dbo].tblProject(Id, [Name], [Location], Filepath, [Image], [Description], 
								UserId, PrivacyId, DateCreated, Purpose, Environment, Challenges, 
								FuturePlans, Collaborators, LastUpdated, SoftwareUsed,StatusId )
		VALUES 
		(NEWID(), 'Office Quote Daily Emailer', 'https://github.com/B-Lemke/DailyOfficeQuoteEmailer', null, null, 'This tool was created as a joke to send a daily email to my friends. It taught me how to set up a web client in python.',
		@UserIdBrody, @PrivacyIdPublic, '2019-1-2', 'A script to email office quotes', 'Made to run daily on RaspberryPi', 'I had to learn how to write emails in Python',
		'None at the moment', 'None', '2019-1-3', 'Visual Studio 2017', @StatusIdCompleted),
		
		(NEWID(), 'ProveIT', 'https://github.com/B-Lemke/ProveIt', null, null, 'This ASP.Net website was created for the Spring 2019 section of Agile Development at FVTC.',
		@UserIdBrody, @PrivacyIdPublic, '2019-1-27', 'A portfolio website for IT students to showcase their work.', 'Made to run in browser, hosted on Microsoft Azure.', 'Learning the Agile work process',
		'Develop the site', 'Mason Brouchard, Lauren Sassi and Brenton Wienkes', '2019-2-27', 'Visual Studio 2017', @StatusIdInProgress);
END