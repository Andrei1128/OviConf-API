USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_CreateConference 
	@p_name nvarchar(255)
AS
BEGIN
	INSERT INTO tbl_Conferences (Name)
		VALUES (@p_name)
END;