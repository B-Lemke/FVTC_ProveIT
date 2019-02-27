BEGIN
	INSERT INTO [dbo].tblPrivacy(Id, [Description])
		VALUES 
		(NEWID(), 'Public'),
		(NEWID(), 'Sign In Required'),
		(NEWID(), 'Individual Access'),
		(NEWID(), 'Password Protection')
END