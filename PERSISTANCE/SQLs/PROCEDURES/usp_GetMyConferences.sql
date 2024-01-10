USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetMyConferences
	@p_userId INT
AS
BEGIN
	SELECT DISTINCT c.Id, c.Name, c.Place, c.RegistrationTill, c.DateStart as StartDate, c.DateEnd as EndDate
	FROM tbl_Roles r 
	INNER JOIN tbl_Conferences c 
	ON r.ConferenceId = c.Id
	WHERE r.UserId = @p_userId
	AND DateEnd > GETDATE()
	AND IsActive = 1
	ORDER BY DateStart
END;
