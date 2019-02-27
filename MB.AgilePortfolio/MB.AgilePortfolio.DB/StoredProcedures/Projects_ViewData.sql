CREATE PROCEDURE [dbo].[Projects_ViewData]
AS
	SELECT p.Id, p.[Name], p.[Description], p.[Location], p.Filepath, p.[Image], 
			u.Email, u.FirstName, u.LastName, priv.[Description] AS 'Privacy', p.DateCreated, 
			p.Purpose, p.Environment, p.Challenges, p.FuturePlans, p.Collaborators, p.LastUpdated, 
			p.SoftwareUsed, s.[Description] AS 'Status'
	FROM [dbo].tblProject p
	JOIN [dbo].tblUser u
	ON u.Id = p.UserId
	JOIN [dbo].tblPrivacy priv
	ON priv.Id = p.PrivacyId
	JOIN [dbo].tblStatus s
	ON s.Id = p.StatusId
RETURN 0
