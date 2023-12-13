USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_RefuseRoleRequest 
	@p_operatedBy INT,
	@p_requestId INT
AS
BEGIN
	UPDATE tbl_Roles SET IsAccepted = 0, OperatedAt = GETDATE(), OperatedBy = @p_operatedBy
	WHERE Id = @p_requestId;
END;