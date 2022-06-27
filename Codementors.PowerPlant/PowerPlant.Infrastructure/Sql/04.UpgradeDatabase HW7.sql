ALTER TABLE [Inspections]
ADD [State] VARCHAR(255);

ALTER TABLE [Inspections]
ADD [Engineer] VARCHAR(255) FOREIGN KEY ([Engineer]) REFERENCES [Members]([Login]);