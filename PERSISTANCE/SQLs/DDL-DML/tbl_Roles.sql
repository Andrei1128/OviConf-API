USE OviConf;
GO
DROP TABLE tbl_Roles;
GO
CREATE TABLE tbl_Roles
	(
		UserId INT NOT NULL,
		ConferenceId INT,
		Role VARCHAR(50) NOT NULL,
		CONSTRAINT uq_Role UNIQUE(UserId, ConferenceId, Role)
	);
GO