﻿CREATE TABLE [dbo].[tblUser]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Email] VARCHAR(255) NOT NULL, 
    [Password] VARCHAR(40) NOT NULL, 
    [FirstName] VARCHAR(255) NOT NULL, 
    [LastName] VARCHAR(255) NOT NULL, 
    [ProfileImage] VARCHAR(255) NULL, 
    [UserTypeId] UNIQUEIDENTIFIER NOT NULL, 
    [Username] VARCHAR(50) NOT NULL, 
    [Bio] TEXT NULL
)
