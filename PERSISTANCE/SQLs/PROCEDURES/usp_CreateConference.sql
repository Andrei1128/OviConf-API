USE OviConf;
GO
CREATE OR ALTER PROCEDURE usp_CreateConference 
	@p_name nvarchar(255),
	@p_place nvarchar(255),
	@p_description nvarchar(max),
	@p_registrationTill Datetime,
	@p_startDate Datetime,
	@p_endDate DateTime
AS
BEGIN
	INSERT INTO tbl_Conferences (Name,Place,Description,RegistrationTill,DateStart,DateEnd,IsActive,CreatedAt)
		VALUES (@p_name,@p_place,@p_description,@p_registrationTill,@p_startDate,@p_endDate,1,GETDATE())
END;
