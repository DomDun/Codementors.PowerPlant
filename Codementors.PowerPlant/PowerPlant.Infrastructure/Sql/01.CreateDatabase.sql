CREATE DATABASE [PowerPlantDB];

USE [PowerPlantDB];

CREATE TABLE [Members] (	
	[Login] VARCHAR(255)  PRIMARY KEY,
	[Password] VARCHAR(255) NOT NULL,
	[Role] VARCHAR(255) NOT NULL,
)

INSERT INTO [Members] ([Login], [Password], [Role]) VALUES
('admin', 'admin', 'Admin');