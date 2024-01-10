USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetAllRoleRequests
AS
BEGIN
<<<<<<< HEAD
	SELECT r.Id ,u.Name as UserName, r.RequestedAt, r.Role as RequestedRole, r.ConferenceId, c.Name as ConferenceName
	FROM tbl_Roles as r 
	INNER JOIN tbl_Users as u
	ON r.UserId = u.Id
	INNER JOIN tbl_Conferences as c
	ON c.Id = r.ConferenceId
	AND r.IsAccepted IS NULL OR r.IsAccepted = 0
	ORDER BY r.RequestedAt;
=======
    SELECT r.Id ,u.Name as UserName, r.RequestedAt, r.Role as RequestedRole, r.ConferenceId 
    FROM tbl_Roles as r INNER JOIN tbl_Users as u
    ON r.UserId = u.Id
    AND r.IsAccepted IS NULL OR r.IsAccepted = 0
    ORDER BY r.RequestedAt;
>>>>>>> 6b1b888949511b086623ab0958f166e5b181cd0d
END;