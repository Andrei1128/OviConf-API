USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetNavTitles 
	@p_conferenceId Int
AS
BEGIN
	SELECT Id, Title 
	FROM tbl_NavItems 
	WHERE ConferenceId = @p_conferenceId
	AND IsActive = 1
	ORDER BY [Order]
END;
