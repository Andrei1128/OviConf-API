USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetConferences	
	@p_id INT
AS
BEGIN
	SELECT Id, Name
	FROM tbl_Conferences
	WHERE Id = @p_id
END;