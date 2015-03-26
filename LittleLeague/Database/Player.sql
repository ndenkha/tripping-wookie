CREATE TABLE [dbo].[Player]
(
	[PlayerId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [IsRegistered] BIT NOT NULL, 
    [TeamId] INT NOT NULL, 
    [RegistrationDate] DATETIME NULL, 
    [CreatedBy] VARCHAR(50) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [LastUpdatedBy] VARCHAR(50) NOT NULL, 
    [LastUpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [FK_Player_Team] FOREIGN KEY ([TeamId]) REFERENCES [Team]([TeamId]) ON DELETE CASCADE
)
