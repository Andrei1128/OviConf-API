USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_UpdateConference 
	@p_id Int,
	@p_name nvarchar(255),
	@p_dateStart Datetime,
	@p_dateEnd DateTime
AS
BEGIN
	UPDATE tbl_Conferences
	SET Name = @p_name, DateStart = @p_dateStart, DateEnd = @p_dateEnd
	WHERE Id = @p_id
END;
