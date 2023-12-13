USE OviConf;
GO
DROP TABLE tbl_Roles;
GO
CREATE TABLE tbl_Roles
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        ConferenceId INT,
        Role VARCHAR(50) NOT NULL,
        RequestedAt DATETIME,
        OperatedAt DATETIME,
        OperatedBy INT,
        IsAccepted BIT,
        CONSTRAINT uq_Role UNIQUE(UserId, ConferenceId, Role)
    );
GO
INSERT INTO tbl_Roles (UserId,Role,RequestedAt,OperatedAt,OperatedBy,IsAccepted)
	VALUES(2,'Admin',GETDATE(),GETDATE(),1,1);
GO