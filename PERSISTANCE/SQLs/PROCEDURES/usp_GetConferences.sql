USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetConferences	
AS
BEGIN
	SELECT Id, Name
	FROM tbl_Conferences 
END;