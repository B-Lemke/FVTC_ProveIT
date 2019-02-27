BEGIN
	DECLARE @UserTypeIdUser UNIQUEIDENTIFIER 
	DECLARE @UserTypeIdEmployer UNIQUEIDENTIFIER 

	SELECT @UserTypeIdUser = Id FROM [dbo].tblUserType WHERE [Description] = 'User';
	SELECT @UserTypeIdEmployer = Id FROM [dbo].tblUserType WHERE [Description] = 'Employer';

	INSERT INTO [dbo].tblUser (Id, Email, [Password], FirstName, LastName, ProfileImage, UserTypeId)
	VALUES
	(newid(), 'sample@gmail.com', 'pa$$word', 'Sample', 'User', 'sampleimage.png', @UserTypeIdUser),
	(newid(), 'joe@wetzel.com', 'pa$$word', 'Joe', 'Wetzel', 'JoeWetzel.png', @UserTypeIdUser),
	(newid(), 'blemke4@gmail.com', 'pa$$word', 'Broderick', 'Lemke', 'broderickLemke.png', @UserTypeIdUser),
	(newid(), 'Employer@fvtc.edu', 'pa$$word', 'Employer', 'Smith', 'FVTC.png', @UserTypeIdEmployer)
END