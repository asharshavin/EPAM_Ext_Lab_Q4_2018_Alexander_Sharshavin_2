CREATE TABLE [dbo].[Messages] (
    [MessageID] INT         IDENTITY (1, 1)    NOT NULL,
    [ChatID]    INT            NOT NULL,
    [UserID]    INT            NOT NULL,
    [Text]      NVARCHAR (MAX) NOT NULL,
    [Date] DATETIME NOT NULL, 
    CONSTRAINT [PK_ChatMessage] PRIMARY KEY CLUSTERED ([MessageID] ASC),
    CONSTRAINT [FK_ChatMessage_Chats] FOREIGN KEY ([ChatID]) REFERENCES [dbo].[Chats] ([ChatID]),
    CONSTRAINT [FK_ChatMessage_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
);

