USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetConference
	@p_id INT
AS
BEGIN
	SELECT Id, Name, Place, Description, RegistrationTill, DateStart, DateEnd 
	FROM tbl_Conferences
	WHERE Id = @p_id
END;
