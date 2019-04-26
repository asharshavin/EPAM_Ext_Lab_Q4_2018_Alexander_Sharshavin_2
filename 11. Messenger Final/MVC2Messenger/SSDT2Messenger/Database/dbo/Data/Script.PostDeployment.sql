/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
/*
:r ".\Roles.data.sql"
*/
DELETE [dbo].[Users]
DELETE [dbo].[Roles]
DELETE [dbo].[Chats]
DELETE [dbo].[Messages]
GO

SET IDENTITY_INSERT [dbo].[Roles] ON
INSERT INTO [dbo].[Roles]([RoleID],[Name]) VALUES (1, N'Admin')
INSERT INTO [dbo].[Roles]([RoleID],[Name]) VALUES (2, N'Bio')
SET IDENTITY_INSERT [dbo].[Roles] OFF

SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users]([UserID],[Username],[Role],DateOfCreation, [Password]) VALUES (1, N'Hren', 1, CONVERT(DATETIME, '19980506', 101), N'123')
INSERT INTO [dbo].[Users]([UserID],[Username],[Role],DateOfCreation, [Password]) VALUES (2, N'Myata', 2, CONVERT(DATETIME, '19980506', 101), N'123')
INSERT INTO [dbo].[Users]([UserID],[Username],[Role],DateOfCreation, [Password]) VALUES (3, N'Beer', 2, CONVERT(DATETIME, '19980506', 101), N'123')
SET IDENTITY_INSERT [dbo].[Users] OFF

SET IDENTITY_INSERT [dbo].[Chats] ON
INSERT dbo.Chats ( [ChatId], [Name] ) VALUES ( 1, N'vegamega')
INSERT dbo.Chats ( [ChatId], [Name] ) VALUES ( 2, N'meat&beer')
SET IDENTITY_INSERT [dbo].[Chats] OFF

SET IDENTITY_INSERT [dbo].[Messages] ON
INSERT dbo.[Messages] ([MessageId], [Text], [UserID], [ChatID], [Date]) VALUES ( 1, N'Всем прив в этом чате!', 1, 1, CONVERT(DATETIME, '20000101', 101))
INSERT dbo.[Messages] ([MessageId], [Text], [UserID], [ChatID], [Date]) VALUES ( 2, N'Как дела?', 2, 2, CONVERT(DATETIME, '20000101', 101))
SET IDENTITY_INSERT [dbo].[Messages] OFF

INSERT dbo.ChatUsers([UserID], [ChatID]) VALUES ( 1, 1 )
INSERT dbo.ChatUsers([UserID], [ChatID]) VALUES ( 3, 2 )
