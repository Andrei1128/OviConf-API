USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetMyConferences
	@p_userId INT
AS
BEGIN
	SELECT c.Id, c.Name, c.Place, c.RegistrationTill, c.DateStart, c.DateEnd 
	FROM tbl_Roles r 
	INNER JOIN tbl_Conferences c 
	ON r.ConferenceId = c.Id
	WHERE r.UserId = @p_userId
END;
