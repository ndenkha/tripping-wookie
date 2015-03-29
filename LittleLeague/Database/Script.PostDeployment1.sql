/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
use LittleLeague
go

declare @user varchar(50), @now datetime
set @user = 'Hydrator'
set @now = GETUTCDATE()

delete from dbo.Team where TeamId between 1 and 2

SET IDENTITY_INSERT dbo.Team ON

insert dbo.Team (TeamId, Name, CreatedBy, CreatedDate, LastUpdatedBy, LastUpdatedDate)
values
	(1, 'Falcons', @user, @now, @user, @now),
	(2, 'Saints', @user, @now, @user, @now)

SET IDENTITY_INSERT dbo.Team OFF

SET IDENTITY_INSERT dbo.Player ON

insert dbo.Player (PlayerId, FirstName, LastName, IsRegistered, TeamId, RegistrationDate, CreatedBy, CreatedDate, LastUpdatedBy, LastUpdatedDate)
values
	(1,  'Phillip',  'Adams',     0, 1, null, @user, @now, @user, @now),
	(2,  'Robert',   'Alford',    0, 1, null, @user, @now, @user, @now),
	(3,  'Ricardo',  'Allen',     0, 1, null, @user, @now, @user, @now),
	(4,  'Jon',      'Asamoah',   0, 1, null, @user, @now, @user, @now),
	(5,  'Jonathan', 'Babineaux', 0, 1, null, @user, @now, @user, @now),
	(6,  'Terron',   'Armstead',  0, 2, null, @user, @now, @user, @now),
	(7,  'Edwin',    'Baker',     0, 2, null, @user, @now, @user, @now),
	(8,  'Marcus',   'Ball',      0, 2, null, @user, @now, @user, @now),
	(9,  'Nick',     'Becton',    0, 2, null, @user, @now, @user, @now),
	(10, 'Delvin',   'Breaux',    0, 2, null, @user, @now, @user, @now)

SET IDENTITY_INSERT dbo.Player OFF
