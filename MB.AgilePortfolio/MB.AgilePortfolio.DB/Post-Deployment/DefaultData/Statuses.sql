BEGIN
	INSERT INTO [dbo].tblStatus(Id, [Description])
		VALUES 
		(NEWID(), 'In Progress'),
		(NEWID(), 'Cancelled'),
		(NEWID(), 'Bugged'),
		(NEWID(), 'Completed')
END