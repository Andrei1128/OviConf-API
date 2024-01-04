USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_AddNavItem 
	@p_conferenceId INT,
	@p_title nvarchar(255),
	@p_content nvarchar(max),
	@p_order INT = 0
AS
BEGIN
	INSERT INTO tbl_NavItems (ConferenceId, Title, Content, [Order])
	VALUES (@p_conferenceId, @p_title, @p_content, @p_order)
END;