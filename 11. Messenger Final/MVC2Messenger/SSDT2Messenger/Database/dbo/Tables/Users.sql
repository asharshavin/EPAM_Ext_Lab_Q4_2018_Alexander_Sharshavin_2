CREATE TABLE [dbo].[Users] (
    [UserID]         INT      IDENTITY (1, 1)      NOT NULL,
    [Username]       NVARCHAR (50) NOT NULL,
    [DateOfCreation] DATETIME      NULL,
    [Role]           INT           NOT NULL,
    [Password] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_Users_Roles] FOREIGN KEY ([Role]) REFERENCES [dbo].[Roles] ([RoleID])
);

