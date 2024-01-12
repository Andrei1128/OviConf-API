USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_RegisterUser 
	@p_email nvarchar(255),
	@p_name nvarchar(255),
	@p_password nvarchar(255)
AS
BEGIN
	declare @tbl_Id table ([Value] int)

	INSERT INTO tbl_Users (Email,Name,Password)
	OUTPUT Inserted.ID into @tbl_Id
	VALUES (@p_email,@p_name,@p_password)

	INSERT INTO tbl_Roles (UserId,Role,RequestedAt,OperatedAt,OperatedBy,IsAccepted)
	VALUES((SELECT TOP 1 [Value] FROM @tbl_Id),'User',GETDATE(),GETDATE(),1,1);
END;