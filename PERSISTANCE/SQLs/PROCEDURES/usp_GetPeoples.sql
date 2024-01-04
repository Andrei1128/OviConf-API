USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetPeoples
	@p_conferenceId INT,
	@p_role nvarchar(50)
AS
BEGIN
	SELECT u.Id ,u.Name INTO #TEMP
	FROM tbl_Users as u 
	INNER JOIN tbl_UserConference as uc
	ON u.Id = uc.UserId and uc.ConferenceId = @p_conferenceId

	IF(@p_role = 'User')
		SELECT Id,Name 
		FROM #TEMP
	ELSE 
		SELECT t.Id, t.Name 
		FROM #TEMP as t
		INNER JOIN tbl_Roles as r 
		ON t.Id = r.UserId
		WHERE r.Role = @p_role
END;