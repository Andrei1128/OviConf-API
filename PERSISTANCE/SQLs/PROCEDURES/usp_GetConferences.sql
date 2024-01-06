USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetConferences	
AS
BEGIN
	SELECT Id, Name, Place, RegistrationTill, DateStart, DateEnd
	FROM tbl_Conferences 
	WHERE DateEnd > GETDATE()
	AND IsActive = 1
	ORDER BY DateStart
END;