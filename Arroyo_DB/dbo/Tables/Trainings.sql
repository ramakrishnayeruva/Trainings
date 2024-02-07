CREATE TABLE [dbo].[Trainings] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [TrainingName] VARCHAR (50) NOT NULL,
    [TrainingType] VARCHAR (50) NOT NULL,
    [Catagory]     VARCHAR (50) NULL,
    CONSTRAINT [PK_Trainings] PRIMARY KEY CLUSTERED ([Id] ASC)
);

