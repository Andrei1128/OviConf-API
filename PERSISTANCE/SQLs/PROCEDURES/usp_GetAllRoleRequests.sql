USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetAllRoleRequests
AS
BEGIN
    SELECT r.Id ,u.Name as UserName, r.RequestedAt, r.Role as RequestedRole, r.ConferenceId 
    FROM tbl_Roles as r INNER JOIN tbl_Users as u
    ON r.UserId = u.Id
    AND r.IsAccepted IS NULL OR r.IsAccepted = 0
    ORDER BY r.RequestedAt;
END;