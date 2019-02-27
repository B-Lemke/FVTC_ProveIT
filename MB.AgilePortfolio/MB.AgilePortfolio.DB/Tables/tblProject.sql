CREATE TABLE [dbo].[tblProject]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(255) NOT NULL, 
    [Location] VARCHAR(255) NULL, 
    [Filepath] VARCHAR(255) NULL, 
    [PrivacyId] UNIQUEIDENTIFIER NOT NULL, 
    [Image] VARCHAR(255) NULL, 
    [Description] VARCHAR(255) NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [DateCreated] DATE NOT NULL, 
    [Purpose] VARCHAR(255) NULL, 
    [Environment] VARCHAR(255) NULL, 
    [Challenges] VARCHAR(255) NULL, 
    [FuturePlans] VARCHAR(255) NULL, 
    [Collaborators] VARCHAR(255) NULL, 
    [LastUpdated] DATE NOT NULL, 
    [SoftwareUsed] VARCHAR(255) NULL, 
    [StatusId] UNIQUEIDENTIFIER NOT NULL
)
