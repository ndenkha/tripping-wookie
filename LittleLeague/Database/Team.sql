CREATE TABLE [dbo].[Team]
(
	[TeamId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [CreatedBy] VARCHAR(50) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [LastUpdatedBy] VARCHAR(50) NOT NULL, 
    [LastUpdatedDate] DATETIME NOT NULL
)
