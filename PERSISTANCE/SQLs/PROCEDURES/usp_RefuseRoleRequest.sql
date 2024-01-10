USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_RefuseRoleRequest 
	@p_requestId INT
AS
BEGIN
	DELETE FROM tbl_Roles
	WHERE Id = @p_requestId;
END;