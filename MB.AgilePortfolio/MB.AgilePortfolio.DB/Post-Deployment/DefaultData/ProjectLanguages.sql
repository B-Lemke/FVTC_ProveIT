BEGIN
	DECLARE @PythonLanguageId UNIQUEIDENTIFIER 
	DECLARE @HTMLLanguageId UNIQUEIDENTIFIER
	DECLARE @CSSLanguageId UNIQUEIDENTIFIER
	DECLARE @SQLLanguageId UNIQUEIDENTIFIER
	DECLARE @CSharpLanguageId UNIQUEIDENTIFIER

	DECLARE @OfficeProjectId UNIQUEIDENTIFIER 
	DECLARE @ProveITProjectId UNIQUEIDENTIFIER 

	SELECT @PythonLanguageId = Id FROM [dbo].tblLanguage WHERE [Description] = 'Python';
	SELECT @HTMLLanguageId = Id FROM [dbo].tblLanguage WHERE [Description] = 'HTML';
	SELECT @CSSLanguageId = Id FROM [dbo].tblLanguage WHERE [Description] = 'CSS';
	SELECT @SQLLanguageId = Id FROM [dbo].tblLanguage WHERE [Description] = 'SQL';
	SELECT @CSharpLanguageId = Id FROM [dbo].tblLanguage WHERE [Description] = 'C#';


	SELECT @OfficeProjectId = Id FROM [dbo].tblProject WHERE [Name] = 'Office Quote Daily Emailer';
	SELECT @ProveITProjectId = Id FROM [dbo].tblProject WHERE [Name] = 'ProveIT';

	

	INSERT INTO [dbo].tblProjectLanguage(Id, LanguageId, ProjectId)
		VALUES 
		(NEWID(), @PythonLanguageId, @OfficeProjectId),
		(NEWID(), @HTMLLanguageId, @ProveITProjectId),
		(NEWID(), @CSSLanguageId, @ProveITProjectId),
		(NEWID(), @SQLLanguageId, @ProveITProjectId),
		(NEWID(), @CSharpLanguageId, @ProveITProjectId)
END
GO