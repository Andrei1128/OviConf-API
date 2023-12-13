USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetMyConferences
	@p_userId INT
AS
BEGIN
	SELECT c.Id,c.Name,c.DateStart,c.DateEnd 
	FROM tbl_UserConferences uc 
	INNER JOIN tbl_Conferences c 
	ON uc.ConferenceId = c.Id
	WHERE UserId = @p_userId
END;
