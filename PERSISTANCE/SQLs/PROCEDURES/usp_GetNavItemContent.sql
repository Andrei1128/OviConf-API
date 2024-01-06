USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_GetNavItemContent
	@p_Id Int
AS
BEGIN
	SELECT Content
	FROM tbl_NavItems 
	WHERE ID = @p_Id
	AND IsActive = 1
END;
