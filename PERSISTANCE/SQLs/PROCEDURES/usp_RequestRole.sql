USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_RequestRole 
	@p_userId INT,
	@p_role nvarchar(50),
	@p_conferenceId INT = null
AS
BEGIN
	INSERT INTO tbl_RoleRequests (UserId, RequestedRole,ConferenceId, RequestedAt)
	VALUES (@p_userId, @p_role, @p_conferenceId, GETDATE())
END;