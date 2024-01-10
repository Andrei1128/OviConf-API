USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetConferences	
AS
BEGIN
	SELECT Id, Name, Description, Place, RegistrationTill, DateStart as StartDate, DateEnd as EndDate
	FROM tbl_Conferences 
	WHERE DateEnd > GETDATE()
	AND IsActive = 1
	ORDER BY DateStart
END;