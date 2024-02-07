CREATE TABLE [dbo].[Contacts] (
    [Id]      UNIQUEIDENTIFIER NOT NULL,
    [Name]    NVARCHAR (MAX)   NOT NULL,
    [email]   NVARCHAR (MAX)   NOT NULL,
    [phone]   BIGINT           NOT NULL,
    [Address] NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED ([Id] ASC)
);

