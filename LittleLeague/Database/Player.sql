CREATE TABLE [dbo].[Player]
(
	[PlayerId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [IsRegistered] BIT NOT NULL, 
    [TeamId] INT NOT NULL, 
    CONSTRAINT [FK_Player_Team] FOREIGN KEY ([TeamId]) REFERENCES [Team]([TeamId]) ON DELETE CASCADE
)
