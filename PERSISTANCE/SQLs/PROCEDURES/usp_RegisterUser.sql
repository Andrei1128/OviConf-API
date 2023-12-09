USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_RegisterUser 
	@p_email nvarchar(255),
	@p_name nvarchar(255),
	@p_password nvarchar(50)
AS
BEGIN
	DECLARE @v_userId TABLE (ID INT PRIMARY KEY)

	INSERT INTO tbl_Users (Email,Name,Password)
	OUTPUT Inserted.ID INTO @v_userId
	VALUES (@p_email,@p_name,@p_password)

	INSERT INTO tbl_Roles (UserId,RoleName)
	VALUES((SELECT TOP 1 ID FROM @v_userId),'User');
END;