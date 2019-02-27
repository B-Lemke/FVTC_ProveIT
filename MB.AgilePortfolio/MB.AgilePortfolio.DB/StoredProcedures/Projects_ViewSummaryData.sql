CREATE PROCEDURE [dbo].[Projects_ViewSummaryData]
AS
	SELECT p.Id, p.[Name], p.[Description],	u.Email, priv.[Description] AS 'Privacy', s.[Description] AS 'Status'
	FROM [dbo].tblProject p
	JOIN [dbo].tblUser u
	ON u.Id = p.UserId
	JOIN [dbo].tblPrivacy priv
	ON priv.Id = p.PrivacyId
	JOIN [dbo].tblStatus s
	ON s.Id = p.StatusId
RETURN 0
