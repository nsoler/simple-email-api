CREATE DATABASE Marketing;
GO

USE Marketing;
GO

CREATE TABLE dbo.Emails (
	EmailAddress nvarchar(255) NOT NULL,
	CreateDateTime datetime2 NOT NULL,
	IsConsumer tinyint NULL DEFAULT (NULL),
	IsBusiness tinyint NULL DEFAULT (NULL),
	IsInvestor tinyint NULL DEFAULT (NULL),
	UpdateDateTime datetime2 NULL DEFAULT (NULL)
);
GO

ALTER TABLE dbo.Emails ADD CONSTRAINT PK__Emails__49A1474195281F7F PRIMARY KEY (EmailAddress);
GO
