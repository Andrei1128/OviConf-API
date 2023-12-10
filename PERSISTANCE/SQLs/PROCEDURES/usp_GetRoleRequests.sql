USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetRoleRequests
	@p_role nvarchar(50),
	@p_conferenceId INT = null
AS
BEGIN
	SELECT r.Id ,u.Name as UserName, r.RequestedAt, r.Role as RequestedRole, r.ConferenceId 
	FROM tbl_Roles as r INNER JOIN tbl_Users as u
	ON r.UserId = u.Id
	WHERE r.Role = @p_role 
	AND (@p_conferenceId IS NULL OR r.ConferenceId = @p_conferenceId)
	AND r.IsAccepted is null;
END;