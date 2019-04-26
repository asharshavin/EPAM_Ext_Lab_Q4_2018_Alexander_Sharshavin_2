CREATE TABLE [dbo].[Chats] (
    [ChatID]         INT       IDENTITY (1, 1)      NOT NULL,
    [Name]       NVARCHAR (MAX) NOT NULL,
    [DateOfCreation] DATETIME       NULL,
    CONSTRAINT [PK_Chats] PRIMARY KEY CLUSTERED ([ChatID] ASC)
);

