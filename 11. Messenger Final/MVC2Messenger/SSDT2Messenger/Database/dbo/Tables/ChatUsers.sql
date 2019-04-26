CREATE TABLE [dbo].ChatUsers (
    [ChatID]    INT            NOT NULL,
    [UserID]    INT            NOT NULL
    CONSTRAINT [FK_ChatUser_Chats] FOREIGN KEY ([ChatID]) REFERENCES [dbo].[Chats] ([ChatID]),
    CONSTRAINT [FK_ChatUser_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
);

