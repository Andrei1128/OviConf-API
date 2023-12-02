USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_RegisterUser 
	@p_email nvarchar(255),
	@p_password nvarchar(50)
AS
BEGIN
	IF (SELECT 1 FROM tbl_Users WHERE Email = @p_email) = 1
		BEGIN
			SELECT 0;
		END
	ELSE
		BEGIN
			INSERT INTO tbl_Users (Email,Role,Password)
			VALUES (@p_email,'User',@p_password)
			SELECT 1;
		END
END;