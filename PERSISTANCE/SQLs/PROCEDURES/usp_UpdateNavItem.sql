USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_UpdateNavItem 
	@p_Id INT,
	@p_title nvarchar(255),
	@p_content nvarchar(max),
	@p_order INT = 0
AS
BEGIN
	UPDATE tbl_NavItems 
	SET Title = @p_title, Content = @p_content, @p_order = @p_order
	WHERE Id = @p_Id;
END;