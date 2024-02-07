CREATE TABLE [dbo].[Resources1] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [AssociateName] VARCHAR (50) NULL,
    [TrainingName]  VARCHAR (50) NULL,
    CONSTRAINT [PK_Resources1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

