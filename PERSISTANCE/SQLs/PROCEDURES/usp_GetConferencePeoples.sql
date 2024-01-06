USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetConferencePeoples
	@p_conferenceId INT,
	@p_role nvarchar(50)
AS
BEGIN
	SELECT u.Id ,u.Name, u.Email
	FROM tbl_Users as u 
	INNER JOIN tbl_Roles as r
	ON u.Id = r.UserId
	WHERE r.Role = @p_role
	AND r.ConferenceId = @p_conferenceId
	AND r.IsAccepted = 1
END;