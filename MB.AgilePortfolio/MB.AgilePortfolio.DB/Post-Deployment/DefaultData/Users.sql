BEGIN
	DECLARE @UserTypeIdUser UNIQUEIDENTIFIER 
	DECLARE @UserTypeIdEmployer UNIQUEIDENTIFIER 

	SELECT @UserTypeIdUser = Id FROM [dbo].tblUserType WHERE [Description] = 'User';
	SELECT @UserTypeIdEmployer = Id FROM [dbo].tblUserType WHERE [Description] = 'Employer';

	INSERT INTO [dbo].tblUser (Id, Email, [Password], FirstName, LastName, ProfileImage, UserTypeId)
	VALUES
	(newid(), 'sample@gmail.com', CONVERT(VARCHAR(40), HashBytes('SHA1', 'pa$$word'), 2), 'Sample', 'User', 'sampleimage.png', @UserTypeIdUser),
	(newid(), 'joe@wetzel.com', CONVERT(VARCHAR(40), HashBytes('SHA1', 'pa$$word'), 2), 'Joe', 'Wetzel', 'JoeWetzel.png', @UserTypeIdUser),
	(newid(), 'blemke4@gmail.com', CONVERT(VARCHAR(40), HashBytes('SHA1', 'pa$$word'), 2), 'Broderick', 'Lemke', 'broderickLemke.png', @UserTypeIdUser),
	(newid(), 'Employer@fvtc.edu', CONVERT(VARCHAR(40), HashBytes('SHA1', 'pa$$word'), 2), 'Employer', 'Smith', 'FVTC.png', @UserTypeIdEmployer)
END