USE OviConf;
GO
DROP TABLE tbl_UserConference;
GO
CREATE TABLE tbl_UserConference
    (
        UserId INT NOT NULL,
        ConferenceId INT NOT NULL,
        CONSTRAINT uq_Registration UNIQUE(UserId, ConferenceId)
    );
GO