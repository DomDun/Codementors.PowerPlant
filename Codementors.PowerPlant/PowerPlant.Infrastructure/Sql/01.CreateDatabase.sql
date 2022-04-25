CREATE DATABASE [PowerPlantDB];

USE [PowerPlantDB];

CREATE TABLE [Members] (	
	[Login] VARCHAR(255)  PRIMARY KEY,
	[Password] VARCHAR(255) NOT NULL,
	[Role] VARCHAR(255) NOT NULL,
)

INSERT INTO [Members] ([Login], [Password], [Role]) VALUES
('admin', 'admin', 'Admin');

CREATE TABLE [Errors] (	
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[PlantName] VARCHAR(255) NOT NULL,
	[MachineName] VARCHAR(255) NOT NULL,
	[MachineValue] FLOAT (8),
	[Unit] VARCHAR(255) NOT NULL,
	[Date] DateTime2,
)