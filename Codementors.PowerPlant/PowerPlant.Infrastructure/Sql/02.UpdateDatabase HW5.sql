﻿DROP TABLE [Errors]

CREATE TABLE [Errors] (	
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[PlantName] VARCHAR(255) NOT NULL,
	[MachineName] VARCHAR(255) NOT NULL,
	[Parameter] VARCHAR(255) NOT NULL,
	[ErrorTime] DateTime2,
	[LoggedUser] VARCHAR(255) NOT NULL,
	[MaxValue] FLOAT (8),
	[MinValue] FLOAT (8)
)