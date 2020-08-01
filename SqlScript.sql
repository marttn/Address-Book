USE [master]
GO

CREATE DATABASE [phone-book]
GO

USE [phone-book]
GO


---------Users---------

CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[Address] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


---------PhoneNumbers---------

CREATE TABLE [dbo].[PhoneNumbers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Number] [nvarchar](10) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_PhoneNumbers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PhoneNumbers]  WITH CHECK ADD  CONSTRAINT [FK_PhoneNumbers_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PhoneNumbers] CHECK CONSTRAINT [FK_PhoneNumbers_Users]
GO


---------Procedures---------

CREATE PROCEDURE [dbo].[AddNumber]
	@UserId int,
	@Number nvarchar(10),
	@IsActive bit
AS 
BEGIN
INSERT INTO [dbo].[PhoneNumbers]
           ([UserID]
           ,[Number]
           ,[IsActive])
     VALUES
           (@UserId,@Number,@IsActive)
END
GO

CREATE PROCEDURE [dbo].[AddUser] 
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Address int
AS
BEGIN
   INSERT INTO Users (FirstName,LastName, Address) VALUES (@FirstName,@LastName,@Address)
END
GO

CREATE PROCEDURE [dbo].[DeleteNumber]
	@Id int
AS 
BEGIN
DELETE FROM [dbo].[PhoneNumbers]
           WHERE ID = @Id
END
GO

CREATE PROCEDURE [dbo].[DeleteUser] 
	@Id int
AS
BEGIN
   DELETE FROM Users WHERE ID = @Id
END
GO

CREATE PROCEDURE [dbo].[DeleteUserNumbers]
	@UserId int
AS 
BEGIN
DELETE FROM [dbo].[PhoneNumbers]
           WHERE UserID = @UserId
END
GO

CREATE PROCEDURE [dbo].[EditNumber]
@Id int,
	@UserId int,
	@Number nvarchar(10),
	@IsActive bit
AS 
BEGIN
UPDATE [dbo].[PhoneNumbers]
           SET [UserID] = @UserId
           ,[Number] = @Number
           ,[IsActive] = @IsActive
		   WHERE ID =@Id
END
GO

CREATE PROCEDURE [dbo].[EditUser] 
	@Id int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50)
AS
BEGIN
   UPDATE Users SET FirstName = @FirstName, LastName = @LastName WHERE ID = @Id
END
GO

Create PROCEDURE [dbo].[GetNumberById]
@Id int
AS
BEGIN
	SELECT * FROM PhoneNumbers WHERE ID = @Id
END
GO

CREATE PROCEDURE [dbo].[GetUserById]
@Id int
AS
BEGIN
	SELECT * FROM Users WHERE ID = @Id
END
GO

CREATE PROCEDURE [dbo].[GetUserNumbers]
	@UserId int
AS 
BEGIN
SELECT * FROM PhoneNumbers
           WHERE UserID = @UserId
END
GO

CREATE PROCEDURE [dbo].[GetUsers]
@AddressNum int
AS
BEGIN
	SELECT * FROM Users WHERE Address = @AddressNum
END
GO


---------Insert test data---------

DECLARE @cnt INT = 1, @currentId int = -1;

WHILE @cnt <= 15
BEGIN
   insert into users (FirstName, LastName, Address) values ('Test', FORMATMESSAGE('User%i',@cnt), @cnt%2 + 1)
   set @currentId = SCOPE_IDENTITY();
   insert into PhoneNumbers (UserID, Number, IsActive) values (@currentId, FORMATMESSAGE('066111222%i',@cnt/2), @cnt%2)
   insert into PhoneNumbers (UserID, Number, IsActive) values (@currentId, FORMATMESSAGE('093222444%i',@cnt%3), @cnt%2)
   insert into PhoneNumbers (UserID, Number, IsActive) values (@currentId, FORMATMESSAGE('097333000%i',@cnt/4), @cnt%2)
   SET @cnt = @cnt + 1;
END;