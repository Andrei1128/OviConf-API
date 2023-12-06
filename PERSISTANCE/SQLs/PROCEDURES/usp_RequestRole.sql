USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_RequestRole 
	@p_userId INT,
	@p_requestedRole nvarchar(50),
	@p_conferenceId INT = null
AS
BEGIN
	DECLARE @v_userRoleRank INT;
	SELECT @v_userRoleRank = r.Rank FROM tbl_Users u INNER JOIN tbl_RoleRanks r ON u.Role = r.Name WHERE u.Id = @p_userId;

	DECLARE @v_requestedRoleRank INT;
	SELECT @v_requestedRoleRank = Rank FROM tbl_RoleRanks WHERE Name = @p_requestedRole;

	IF @v_userRoleRank >= @v_requestedRoleRank
		BEGIN
			SELECT 0;
		END
	ELSE
		BEGIN
			INSERT INTO tbl_RoleRequests (UserId, RequestedRole, RequestedAt)
			VALUES (@p_userId, @p_requestedRole, GETDATE())
			SELECT 1;
		END
END;