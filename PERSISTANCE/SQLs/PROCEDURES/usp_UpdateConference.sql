USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_UpdateConference 
	@p_id Int,
	@p_name nvarchar(255),
	@p_place nvarchar(255),
	@p_description nvarchar(max),
	@p_registrationTill Datetime,
	@p_startDate Datetime,
	@p_endDate DateTime,
	@p_isActive Bit
AS
BEGIN
	UPDATE tbl_Conferences
	SET 
		Name = @p_name, 
		Place = @p_place, 
		Description = @p_description, 
		RegistrationTill = @p_registrationTill, 
		DateStart = @p_startDate, 
		DateEnd = @p_endDate,
		IsActive = @p_isActive,
		ModifiedAt = GETDATE()
	WHERE Id = @p_id
END;
