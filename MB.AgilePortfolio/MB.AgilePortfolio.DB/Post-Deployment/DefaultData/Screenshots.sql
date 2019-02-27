BEGIN
	DECLARE @OfficeProjectId UNIQUEIDENTIFIER 
	DECLARE @ProveITProjectId UNIQUEIDENTIFIER 

	SELECT @OfficeProjectId = Id FROM [dbo].tblProject WHERE [Name] = 'Office Quote Daily Emailer';
	SELECT @ProveITProjectId = Id FROM [dbo].tblProject WHERE [Name] = 'ProveIT';

	INSERT INTO [dbo].tblScreenshot(Id, Filepath, ProjectId )
	VALUES
	(newid(), 'Images/Screenshots/User213/Project123/img1.jpg', @OfficeProjectId),
	(newid(), 'Images/Screenshots/User213/Project123/img2.jpg', @OfficeProjectId),
	(newid(), 'Images/Screenshots/User213/Project123/img3.jpg', @OfficeProjectId),
	(newid(), 'Images/Screenshots/User213/Project123/img4.jpg', @OfficeProjectId),
	(newid(), 'Images/Screenshots/User213/Project124/img1.jpg', @ProveITProjectId)
END;