USE OviConf;
GO
DROP TABLE tbl_RoleRequests;
GO
CREATE TABLE tbl_RoleRequests
	(
		Id INT IDENTITY(1,1) PRIMARY KEY,
		UserId INT NOT NULL,
		RequestedRole VARCHAR(50),
		ConferenceId INT,
		RequestedAt DATETIME,
		IsAccepted BIT,
	);
GO