USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetConferences	
AS
BEGIN
	SELECT Id, Name, DateStart, DateEnd
	FROM tbl_Conferences 
	WHERE DateEnd > GETDATE()
END;