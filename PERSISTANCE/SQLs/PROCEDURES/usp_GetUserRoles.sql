USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetUserRoles 
	@p_userId INT
AS
BEGIN
	SELECT Role as RoleName, ConferenceId FROM tbl_Roles WHERE UserId = @p_userId AND IsAccepted = 1;
END; 