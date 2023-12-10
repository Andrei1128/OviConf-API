USE OviConf;
GO
DROP TABLE tbl_Users;
GO
CREATE TABLE tbl_Users
	(
		Id INT IDENTITY(1,1) PRIMARY KEY,
		Name VARCHAR(255),
		Email VARCHAR(255) UNIQUE,
		Password VARCHAR(255)
	);
GO
INSERT INTO tbl_Users (Name,Email,Password)
	VALUES('admin','admin','$2a$11$PdE31JFDLSQCutnnyX2JQupT8gCApOq/qtC2EZ4lNGsX4SHC68Dre');
GO