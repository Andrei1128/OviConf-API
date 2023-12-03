USE OviConf;
GO
DROP TABLE tbl_RoleRanks;
GO
CREATE TABLE tbl_RoleRanks
	(
		Name VARCHAR(50),
		Rank INT
	);
GO
INSERT INTO tbl_RoleRanks (Name, Rank) VALUES ('Admin', 100);
GO
INSERT INTO tbl_RoleRanks (Name, Rank) VALUES ('Helper', 75);
GO
INSERT INTO tbl_RoleRanks (Name, Rank) VALUES ('Manager', 50);
GO
INSERT INTO tbl_RoleRanks (Name, Rank) VALUES ('Speaker', 25);
GO
INSERT INTO tbl_RoleRanks (Name, Rank) VALUES ('User', 10);
GO