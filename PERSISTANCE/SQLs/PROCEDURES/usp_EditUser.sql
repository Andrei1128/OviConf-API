USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_EditUser
	@p_userId int,
	@p_name nvarchar(255),
	@p_password nvarchar(255)
AS
BEGIN
	UPDATE tbl_Users
	SET Name = @p_name, Password = @p_password
	WHERE Id = @p_userId

	SELECT 1 FROM tbl_Users WHERE Id = @p_userId
END;