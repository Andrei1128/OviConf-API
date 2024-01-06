USE OviConf;
GO
DROP TABLE tbl_Conferences;
GO
CREATE TABLE tbl_Conferences
	(
		Id INT IDENTITY(1,1) PRIMARY KEY,
		Name VARCHAR(255),
		Place VARCHAR(255),
		Description VARCHAR(MAX),
		RegistrationTill DateTime,
		DateStart DateTime,
		DateEnd DateTime,
		IsActive BIT,
		CreatedAt DateTime,
		ModifiedAt DateTime
	);
GO