declare @UserTypeId uniqueidentifier select @UserTypeId = Id from tblUserType where Description = 'Default User'

insert into tblUser (Id, Email, Password, FirstName, LastName, ProfileImage, UserTypeId)
values
(newid(),'sample@gmail.com','pa$$word','Sample','User','sampleimage.png',@UserTypeId)