USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_RegisterUser 
	@p_email nvarchar(255),
	@p_name nvarchar(255),
	@p_password nvarchar(255)
AS
BEGIN
	INSERT INTO tbl_Users (Email,Name,Password)
	OUTPUT Inserted.ID as ID INTO #TEMP
	VALUES (@p_email,@p_name,@p_password)

	INSERT INTO tbl_Roles (UserId,Role,RequestedAt,OperatedAt,OperatedBy,IsAccepted)
	VALUES((SELECT TOP 1 ID FROM #TEMP),'User',GETDATE(),GETDATE(),1,1);
END;