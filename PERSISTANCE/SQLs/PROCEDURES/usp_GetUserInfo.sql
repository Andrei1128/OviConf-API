USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetUserInfo 
	@p_userId int
AS
BEGIN
	SELECT Id, Name, Email 
	FROM tbl_Users 
	WHERE Id = @p_userId
END;