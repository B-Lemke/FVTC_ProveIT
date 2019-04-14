CREATE TABLE [dbo].[tblProject]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(255) NOT NULL, 
    [Location] VARCHAR(255) NULL, 
    [Filepath] VARCHAR(255) NULL, 
    [PrivacyId] UNIQUEIDENTIFIER NOT NULL, 
    [Image] VARCHAR(255) NULL, 
    [Description] TEXT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [DateCreated] DATE NOT NULL, 
    [Purpose] TEXT NULL, 
    [Environment] VARCHAR(255) NULL, 
    [Challenges] TEXT NULL, 
    [FuturePlans] TEXT NULL, 
    [Collaborators] VARCHAR(255) NULL, 
    [LastUpdated] DATE NOT NULL, 
    [SoftwareUsed] TEXT NULL, 
    [StatusId] UNIQUEIDENTIFIER NOT NULL
)
