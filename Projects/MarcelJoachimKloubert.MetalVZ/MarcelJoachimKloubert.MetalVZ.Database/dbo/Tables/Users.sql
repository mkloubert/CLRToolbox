CREATE TABLE [dbo].[Users] (
    [UserID]       BIGINT           IDENTITY (1, 1) NOT NULL,
    [UserExportID] UNIQUEIDENTIFIER CONSTRAINT [DF_Users_UserExportID] DEFAULT (newid()) NOT NULL,
    [Email]        NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users]
    ON [dbo].[Users]([UserID] ASC);

