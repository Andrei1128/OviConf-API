USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_RegisterAtConference 
	@p_userId INT,
	@p_conferenceId INT
AS
BEGIN
	INSERT INTO tbl_UserConference (UserId,ConferenceId)
		VALUES(@p_userId,@p_conferenceId);
END;