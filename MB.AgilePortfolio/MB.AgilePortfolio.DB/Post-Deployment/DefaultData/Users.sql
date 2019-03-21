BEGIN
	DECLARE @UserId1 UNIQUEIDENTIFIER
	DECLARE @UserId2 UNIQUEIDENTIFIER
	DECLARE @UserId3 UNIQUEIDENTIFIER
	DECLARE @UserId4 UNIQUEIDENTIFIER
	DECLARE @UserId5 UNIQUEIDENTIFIER

	SELECT @UserId1 = newid()
	SELECT @UserId2 = newid()
	SELECT @UserId3 = newid()
	SELECT @UserId4 = newid()
	SELECT @UserId5 = newid()

	DECLARE @UserTypeIdUser UNIQUEIDENTIFIER 
	DECLARE @UserTypeIdEmployer UNIQUEIDENTIFIER 
	DECLARE @UserTypeIdAdmin UNIQUEIDENTIFIER 

	SELECT @UserTypeIdUser = Id FROM [dbo].tblUserType WHERE [Description] = 'User';
	SELECT @UserTypeIdEmployer = Id FROM [dbo].tblUserType WHERE [Description] = 'Employer';
	SELECT @UserTypeIdAdmin = Id FROM [dbo].tblUserType WHERE [Description] = 'Admin';

	INSERT INTO [dbo].tblUser (Id, Email, [Password], FirstName, LastName, ProfileImage, UserTypeId)
	VALUES
	(@UserId1, 'sample@gmail.com', CONVERT(VARCHAR(40), HashBytes('SHA1', Concat('pa$$word',@UserId1)), 2), 'Sample', 'User', 'sampleimage.png', @UserTypeIdUser),
	(@UserId2, 'joe@wetzel.com', CONVERT(VARCHAR(40), HashBytes('SHA1', Concat('pa$$word',@UserId2)), 2), 'Joe', 'Wetzel', 'JoeWetzel.png', @UserTypeIdUser),
	(@UserId3, 'blemke4@gmail.com', CONVERT(VARCHAR(40), HashBytes('SHA1', Concat('pa$$word',@UserId3)), 2), 'Broderick', 'Lemke', 'broderickLemke.png', @UserTypeIdUser),
	(@UserId4, 'Employer@fvtc.edu', CONVERT(VARCHAR(40), HashBytes('SHA1', Concat('pa$$word',@UserId4)), 2), 'Employer', 'Smith', 'FVTC.png', @UserTypeIdEmployer),
	(@UserId5, 'admin', CONVERT(VARCHAR(40), HashBytes('SHA1', Concat('pa$$word',@UserId5)), 2), 'Dead', 'Pen', 'FVTC.png', @UserTypeIdAdmin)
END