CREATE PROCEDURE [dbo].[User_ViewData]
AS
	SELECT u.Id, u.Email, u.FirstName, u.LastName, u.[Password], u.ProfileImage, ut.[Description] 
	FROM [dbo].tblUser u
	JOIN [dbo].tblUserType ut
	ON ut.Id = u.UserTypeId;
RETURN 0
