USE OviConf;
GO
DROP TABLE tbl_NavItems;
GO
CREATE TABLE tbl_NavItems
	(
		Id INT IDENTITY(1,1) PRIMARY KEY,
		ConferenceId INT,
		Title NVARCHAR(255),
		Content NVARCHAR(MAX),
		[Order] INT
	);
GO