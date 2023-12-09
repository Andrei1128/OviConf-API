USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetUserData 
	@p_email nvarchar(255)	
AS
BEGIN
	SELECT Id, Name, Email, Password 
	FROM tbl_Users 
	WHERE Email = @p_email
END;