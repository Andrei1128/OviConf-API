USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_CreateConference 
	@p_name nvarchar(255),
	@p_dateStart Datetime,
	@p_dateEnd DateTime
AS
BEGIN
	INSERT INTO tbl_Conferences (Name,DateStart,DateEnd)
		VALUES (@p_name,@p_dateStart,@p_dateEnd)
END;
