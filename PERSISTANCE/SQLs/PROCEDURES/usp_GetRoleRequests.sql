USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetRoleRequests
	@p_role nvarchar(50),
	@p_conferenceId INT = null
AS
BEGIN
	SELECT r.Id ,u.Name as UserName, r.RequestedAt, r.RequestedRole, r.ConferenceId 
	FROM tbl_RoleRequests as r INNER JOIN tbl_Users as u
	ON r.UserId = u.Id
	WHERE r.RequestedRole = @p_role AND r.ConferenceId = @p_conferenceId;
END;