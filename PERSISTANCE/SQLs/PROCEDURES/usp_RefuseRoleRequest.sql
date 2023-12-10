USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_RefuseRoleRequest 
	@p_operatedBy nvarchar(255),
	@p_requestId INT
AS
BEGIN
	UPDATE tbl_Roles SET IsAccepted = 0, OperatedAt = GETDATE(), OperatedBy = @p_operatedBy
	WHERE Id = @p_requestId;
END;