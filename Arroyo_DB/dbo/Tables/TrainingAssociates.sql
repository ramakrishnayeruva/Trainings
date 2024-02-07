CREATE TABLE [dbo].[TrainingAssociates] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [TrainingId]  INT NOT NULL,
    [AssociateId] INT NOT NULL,
    CONSTRAINT [PK_TrainingAssociates] PRIMARY KEY CLUSTERED ([Id] ASC)
);

