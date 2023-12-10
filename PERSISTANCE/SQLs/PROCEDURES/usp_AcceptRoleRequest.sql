USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_AcceptRoleRequest 
	@p_operatedBy nvarchar(255),
	@p_requestId INT
AS
BEGIN
	UPDATE tbl_Roles SET IsAccepted = 1, OperatedAt = GETDATE(), OperatedBy = @p_operatedBy
	WHERE Id = @p_requestId;
END;