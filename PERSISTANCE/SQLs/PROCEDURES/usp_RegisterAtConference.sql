USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_RegisterAtConference 
	@p_userId INT,
	@p_conferenceId INT
AS
BEGIN
	INSERT INTO tbl_Roles (UserId, Role,ConferenceId, RequestedAt, OperatedAt, IsAccepted, OperatedBy)
	VALUES (@p_userId, 'Participant', @p_conferenceId, GETDATE(), GETDATE(), 1, 1)
END;