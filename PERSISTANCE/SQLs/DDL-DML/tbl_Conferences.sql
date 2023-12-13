USE OviConf;
GO
DROP TABLE tbl_Conferences;
GO
CREATE TABLE tbl_Conferences
	(
		Id INT IDENTITY(1,1) PRIMARY KEY,
		Name VARCHAR(255),
		DateStart DateTime,
		DateEnd DateTime
	);
GO