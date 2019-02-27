BEGIN
	INSERT INTO [dbo].tblUserType (Id, [Description])
		VALUES 
		(NEWID(), 'User'),
		(NEWID(), 'Employer')
END