USE [Development]
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertUserInfo]    Script Date: 16-11-2021 07:47:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[usp_InsertUserInfo] (@UserId UNIQUEIDENTIFIER = NULL,
@EmailId VARCHAR(255),
@UserRoleId INT,
@FirstName NVARCHAR(500),
@LastName NVARCHAR(500),
@PasswordHash VARCHAR(MAX),
@PasswordSalt VARCHAR(MAX),
@IsActive BIT,
@CreatorOrUpdator UNIQUEIDENTIFIER,
@CreatedOrUpdatedDate DATETIME2)
-- WITH ENCRYPTION, RECOMPILE, EXECUTE AS CALLER|SELF|OWNER| 'user_name'
AS
BEGIN
	IF @UserId IS NULL
	BEGIN
		SET @UserId = NEWID();
		INSERT INTO UserInfo (UserId, EmailId, UserRoleId, FirstName, LastName, PasswordHash, PasswordSalt, IsActive, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)
			VALUES (@UserId, @EmailId, @UserRoleId, @FirstName, @LastName, @PasswordHash, @PasswordSalt, @IsActive, @CreatorOrUpdator, @CreatedOrUpdatedDate, @CreatorOrUpdator, @CreatedOrUpdatedDate);
	END
	ELSE
	BEGIN
		UPDATE UserInfo
		SET EmailId = @EmailId
		   ,FirstName = @FirstName
		   ,LastName = @LastName
		   ,IsActive = @IsActive
		   ,PasswordHash = @PasswordHash
		   ,PasswordSalt = @PasswordSalt
		   ,UpdatedBy = @CreatorOrUpdator
		   ,UpdatedDate = @CreatedOrUpdatedDate
		WHERE UserId = @UserId
	END

	SELECT
		ui.UserId
	   ,ui.EmailId
	   ,ui.UserRoleId
	   ,ui.FirstName
	   ,ui.LastName
	   ,ui.PasswordHash
	   ,ui.PasswordSalt
	   ,ui.IsActive
	   ,ui.CreatedBy
	   ,ui.CreatedDate
	   ,ui.UpdatedBy
	   ,ui.UpdatedDate
	FROM UserInfo AS ui
	WHERE ui.UserId = @UserId;
END