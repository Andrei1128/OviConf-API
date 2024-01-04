USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetNavItems 
	@p_conferenceId Int
AS
BEGIN
	SELECT Id, Title 
	FROM tbl_NavItems 
	WHERE ConferenceId = @p_conferenceId
END;
