/* SQLINES DEMO *** ------------------------- Table Creation Started   -----------------------------------

*/


-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE SystemInfo (
	SystemInfoId INT PRIMARY KEY IDENTITY
   ,SystemInfoName VARCHAR(255) UNIQUE
   ,SystemInfoValue VARCHAR(MAX)
);


-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE UserRoleMaster (
	UserRoleId INT PRIMARY KEY IDENTITY
   ,UserRoleName VARCHAR(255)
);

-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE UserInfo (
	UserId UNIQUEIDENTIFIER PRIMARY KEY
   ,EmailId VARCHAR(255) UNIQUE NOT NULL
   ,UserRoleId INT
   ,FirstName NVARCHAR(500)
   ,LastName NVARCHAR(500)
   ,PasswordHash VARCHAR(MAX)
   ,PasswordSalt VARCHAR(MAX)
   ,IsActive BIT
   ,CreatedBy UNIQUEIDENTIFIER
   ,CreatedDate DATETIME2
   ,UpdatedBy UNIQUEIDENTIFIER
   ,UpdatedDate DATETIME2
);


-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE RestaurantCompanyMaster (
	RestaurantCompanyId UNIQUEIDENTIFIER PRIMARY KEY
   ,RestaurantCompanyName NVARCHAR(255)
   ,Description NVARCHAR(255)
   ,Website NVARCHAR(255)
   ,Phone NVARCHAR(255)
   ,IsDeleted BIT
   ,CreatedBy UNIQUEIDENTIFIER
   ,CreatedDate DATETIME2
   ,UpdatedBy UNIQUEIDENTIFIER
   ,UpdatedDate DATETIME2
);

-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE RestaurantStatusMaster (
	RestaurantStatusId INT PRIMARY KEY IDENTITY
   ,RestaurantStatusName VARCHAR(50)
);

-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE RestaurantMaster (
	RestaurantId UNIQUEIDENTIFIER PRIMARY KEY
   ,RestaurantCompanyId UNIQUEIDENTIFIER
   ,RestaurantName NVARCHAR(255)
   ,Description NVARCHAR(255)
   ,Website NVARCHAR(255)
   ,Phone NVARCHAR(255)
   ,StreetAddress NVARCHAR(500)
   ,State NVARCHAR(255)
   ,Country NVARCHAR(255)
   ,OpeningTime TIME
   ,ClosingTime TIME
   ,Latitude DECIMAL(12, 9)
   ,Longitude DECIMAL(12, 9)
   ,CurrentStatus INT
   ,IsDeleted BIT
   ,CreatedBy UNIQUEIDENTIFIER
   ,CreatedDate DATETIME2
   ,UpdatedBy UNIQUEIDENTIFIER
   ,UpdatedDate DATETIME2
);


-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE RestaurantUserMapping (
	RestaurantId UNIQUEIDENTIFIER NOT NULL
   ,UserId UNIQUEIDENTIFIER NOT NULL
   ,AssignedBy UNIQUEIDENTIFIER NOT NULL
);


-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE RestaurantHolidayMaster (
	RestaurantHolidayId INT PRIMARY KEY IDENTITY
   ,RestaurantId UNIQUEIDENTIFIER
   ,HolidayDate DATE
   ,CreatedBy UNIQUEIDENTIFIER
   ,CreatedDate DATETIME2
   ,UpdatedBy UNIQUEIDENTIFIER
   ,UpdatedDate DATETIME2
);

-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE RestaurantTableTypeMaster (
	TableTypeId INT PRIMARY KEY
   ,TableTypeName NVARCHAR(255)
);


-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE RestaurantTableMaster (
	RestaurantTableId UNIQUEIDENTIFIER PRIMARY KEY
   ,RestaurantTableName NVARCHAR(255)
   ,TotalSeats INT
   ,IsActive BIT
   ,TableTypeId INT
   ,RestaurantId UNIQUEIDENTIFIER
   ,CreatedBy UNIQUEIDENTIFIER
   ,CreatedDate DATETIME2
   ,UpdatedBy UNIQUEIDENTIFIER
   ,UpdatedDate DATETIME2
);

-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE RestaurantTableTagMaster (
	RestaurantTableTagId UNIQUEIDENTIFIER PRIMARY KEY
   ,RestaurantTableTagName NVARCHAR(255)
);

-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE RestaurantTableTagMapping (
	RestaurantTableId UNIQUEIDENTIFIER
   ,RestaurantTableTagId UNIQUEIDENTIFIER
);

-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE BookingStatusMaster (
	BookingStatusId INT PRIMARY KEY IDENTITY
   ,BookingStatusName NVARCHAR(255)
);


-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE RestaurantBookingMaster (
	BookingId UNIQUEIDENTIFIER PRIMARY KEY
   ,BookingBy UNIQUEIDENTIFIER
   ,BookingDate DATETIME2
   ,MinHours INT
   ,BookingStatus INT
   ,TableIds INT
   ,ConfirmedBy UNIQUEIDENTIFIER
);


/* SQLINES DEMO *** ------------------------- Table Creation Completed   -----------------------------------

*/

/* SQLINES DEMO *** ------------------------- Adding Foreign Key and Constraints Started  -----------------------------------

*/
ALTER TABLE UserInfo ADD FOREIGN KEY (UserRoleId) REFERENCES UserRoleMaster (UserRoleId);

ALTER TABLE RestaurantCompanyMaster ADD FOREIGN KEY (CreatedBy) REFERENCES UserInfo (UserId);

ALTER TABLE RestaurantCompanyMaster ADD FOREIGN KEY (UpdatedBy) REFERENCES UserInfo (UserId);

ALTER TABLE RestaurantMaster ADD FOREIGN KEY (RestaurantCompanyId) REFERENCES RestaurantCompanyMaster (RestaurantCompanyId);

ALTER TABLE RestaurantMaster ADD FOREIGN KEY (CreatedBy) REFERENCES UserInfo (UserId);

ALTER TABLE RestaurantMaster ADD FOREIGN KEY (UpdatedBy) REFERENCES UserInfo (UserId);

ALTER TABLE RestaurantMaster ADD FOREIGN KEY (CurrentStatus) REFERENCES RestaurantStatusMaster (RestaurantStatusId);

ALTER TABLE RestaurantUserMapping ADD FOREIGN KEY (RestaurantId) REFERENCES RestaurantMaster (RestaurantId);

ALTER TABLE RestaurantUserMapping ADD FOREIGN KEY (UserId) REFERENCES UserInfo (UserId);

ALTER TABLE RestaurantUserMapping ADD FOREIGN KEY (AssignedBy) REFERENCES UserInfo (UserId);

ALTER TABLE RestaurantUserMapping ADD CONSTRAINT PK_UserAndRestaurantMapping PRIMARY KEY (RestaurantId, UserId)

ALTER TABLE RestaurantHolidayMaster ADD FOREIGN KEY (RestaurantId) REFERENCES RestaurantMaster (RestaurantId);

ALTER TABLE RestaurantHolidayMaster ADD FOREIGN KEY (CreatedBy) REFERENCES UserInfo (UserId);

ALTER TABLE RestaurantHolidayMaster ADD FOREIGN KEY (UpdatedBy) REFERENCES UserInfo (UserId);

ALTER TABLE RestaurantTableMaster ADD FOREIGN KEY (TableTypeId) REFERENCES RestaurantTableTypeMaster (TableTypeId);

ALTER TABLE RestaurantTableMaster ADD FOREIGN KEY (RestaurantId) REFERENCES RestaurantMaster (RestaurantId);

ALTER TABLE RestaurantTableMaster ADD FOREIGN KEY (CreatedBy) REFERENCES UserInfo (UserId);

ALTER TABLE RestaurantTableMaster ADD FOREIGN KEY (UpdatedBy) REFERENCES UserInfo (UserId);

ALTER TABLE RestaurantTableTagMapping ADD FOREIGN KEY (RestaurantTableId) REFERENCES RestaurantTableMaster (RestaurantTableId);

ALTER TABLE RestaurantTableTagMapping ADD FOREIGN KEY (RestaurantTableTagId) REFERENCES RestaurantTableTagMaster (RestaurantTableTagId);

ALTER TABLE RestaurantBookingMaster ADD FOREIGN KEY (BookingStatus) REFERENCES BookingStatusMaster (BookingStatusId);

ALTER TABLE RestaurantBookingMaster ADD FOREIGN KEY (BookingBy) REFERENCES UserInfo (UserId);

ALTER TABLE RestaurantBookingMaster ADD FOREIGN KEY (ConfirmedBy) REFERENCES UserInfo (UserId);


/* SQLINES DEMO *** ------------------------ Adding Foreign Key and Constraints Completed  -----------------------------------
*/


/* SQLINES DEMO *** ------------------------ Adding Static Entries  -----------------------------------
*/


SET IDENTITY_INSERT UserRoleMaster ON;

INSERT INTO UserRoleMaster
(
	UserRoleId
   ,UserRoleName
)
VALUES
(
	1
   ,'System'
);
INSERT INTO UserRoleMaster
(
	UserRoleId
   ,UserRoleName
)
VALUES
(
	2
   ,'Super Admin'
);
INSERT INTO UserRoleMaster
(
	UserRoleId
   ,UserRoleName
)
VALUES
(
	3
   ,'Admin'
);
INSERT INTO UserRoleMaster
(
	UserRoleId
   ,UserRoleName
)
VALUES
(
	4
   ,'Restaurant Admin'
);
INSERT INTO UserRoleMaster
(
	UserRoleId
   ,UserRoleName
)
VALUES
(
	5
   ,'Customer'
);


SET IDENTITY_INSERT UserRoleMaster OFF;

SET IDENTITY_INSERT RestaurantStatusMaster ON;

INSERT INTO RestaurantStatusMaster
(
	RestaurantStatusId
   ,RestaurantStatusName
)
VALUES
(
	1
   ,'Opened'
);
INSERT INTO RestaurantStatusMaster
(
	RestaurantStatusId
   ,RestaurantStatusName
)
VALUES
(
	2
   ,'Closed'
);

SET IDENTITY_INSERT RestaurantStatusMaster OFF;

SET IDENTITY_INSERT SystemInfo ON;

INSERT INTO SystemInfo
(
	SystemInfoId
   ,SystemInfoName
   ,SystemInfoValue
)
VALUES
(
	1
   ,'IS_SYSTEM_INITIALIZED'
   ,'false'
);

SET IDENTITY_INSERT SystemInfo OFF;


DECLARE @SystemUUID UNIQUEIDENTIFIER = NEWID();
INSERT INTO UserInfo
(
	UserId
   ,EmailId
   ,UserRoleId
   ,FirstName
   ,LastName
   ,PasswordHash
   ,PasswordSalt
   ,IsActive
   ,CreatedBy
   ,CreatedDate
   ,UpdatedBy
   ,UpdatedDate
)
VALUES
(
	@SystemUUID
   ,'System@SmartTable.com'
   ,1
   ,'System'
   ,''
   ,''
   ,''
   ,1
   ,@SystemUUID
   ,GETDATE()
   ,@SystemUUID
   ,GETDATE()
)






/*
--------------------------------------------------------------------------------------------------------------

                         Function Creation Started

---------------------------------------------------------------------------------------------------------------

*/

GO
/****** Object:  UserDefinedFunction [dbo].[udf_StringToUUID]    Script Date: 07-12-2021 16:12:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER FUNCTION [dbo].[udf_StringToUUID]
(
	@uuid VARCHAR(50)
)
-- WITH ENCRYPTION, SCHEMABINDING, EXECUTE AS CALLER|SELF|OWNER|USER
RETURNS UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @Result UNIQUEIDENTIFIER = NULL;

	SET @uuid = REPLACE(@uuid, '-', '');
	SET @uuid = REPLACE(@uuid, CHAR(39), '');

	SELECT
		@Result =
		CAST(SUBSTRING(@uuid, 1, 8) + '-' + SUBSTRING(@uuid, 9, 4) + '-' + SUBSTRING(@uuid, 13, 4) + '-' +
		SUBSTRING(@uuid, 17, 4) + '-' + SUBSTRING(@uuid, 21, 12)
		AS UNIQUEIDENTIFIER)
	RETURN @Result;
END



/*
--------------------------------------------------------------------------------------------------------------

                         Function Creation Ended

---------------------------------------------------------------------------------------------------------------

*/



/*
--------------------------------------------------------------------------------------------------------------

                         Stored Procedure Creation

---------------------------------------------------------------------------------------------------------------

*/

GO
/****** Object:  StoredProcedure [dbo].[usp_InsertUserInfo]    Script Date: 16-11-2021 07:47:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [dbo].[usp_InsertUserInfo]
(
	@UserId UNIQUEIDENTIFIER = NULL
   ,@EmailId VARCHAR(255)
   ,@UserRoleId INT
   ,@FirstName NVARCHAR(500)
   ,@LastName NVARCHAR(500)
   ,@PasswordHash VARCHAR(MAX)
   ,@PasswordSalt VARCHAR(MAX)
   ,@IsActive BIT
   ,@CreatorOrUpdator UNIQUEIDENTIFIER
   ,@CreatedOrUpdatedDate DATETIME2
)
-- WITH ENCRYPTION, RECOMPILE, EXECUTE AS CALLER|SELF|OWNER| 'user_name'
AS
BEGIN

	IF @UserId IS NULL
		BEGIN
			SET @UserId = NEWID();
			INSERT INTO UserInfo
			(
				UserId
			   ,EmailId
			   ,UserRoleId
			   ,FirstName
			   ,LastName
			   ,PasswordHash
			   ,PasswordSalt
			   ,IsActive
			   ,CreatedBy
			   ,CreatedDate
			   ,UpdatedBy
			   ,UpdatedDate
			)
			VALUES
			(
				@UserId
			   ,@EmailId
			   ,@UserRoleId
			   ,@FirstName
			   ,@LastName
			   ,@PasswordHash
			   ,@PasswordSalt
			   ,@IsActive
			   ,@CreatorOrUpdator
			   ,@CreatedOrUpdatedDate
			   ,@CreatorOrUpdator
			   ,@CreatedOrUpdatedDate
			);
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
END;




GO
/****** Object:  StoredProcedure [dbo].[usp_RestaurantCompany_IU]    Script Date: 01-12-2021 19:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [dbo].[usp_RestaurantCompany_IU]
(
	@RestaurantCompanyId UNIQUEIDENTIFIER = NULL
   ,@RestaurantCompanyName NVARCHAR(500)
   ,@Description NVARCHAR(500)
   ,@Website NVARCHAR(500)
   ,@Phone NVARCHAR(500)
   ,@CreatedOrUpdatedBy UNIQUEIDENTIFIER
   ,@CreatedOrUpdatedDate DATETIME2
)
-- WITH ENCRYPTION, RECOMPILE, EXECUTE AS CALLER|SELF|OWNER| 'user_name'
AS
BEGIN
	SET NOCOUNT ON;

	IF @RestaurantCompanyId IS NULL
		BEGIN
			SET @RestaurantCompanyId = NEWID();
			INSERT INTO RestaurantCompanyMaster
			(
				RestaurantCompanyId
			   ,RestaurantCompanyName
			   ,Description
			   ,Website
			   ,Phone
			   ,IsDeleted
			   ,CreatedBy
			   ,CreatedDate
			   ,UpdatedBy
			   ,UpdatedDate
			)
			VALUES
			(
				@RestaurantCompanyId
			   ,@RestaurantCompanyName
			   ,@Description
			   ,@Website
			   ,@Phone
			   ,0
			   ,@CreatedOrUpdatedBy
			   ,@CreatedOrUpdatedDate
			   ,@CreatedOrUpdatedBy
			   ,@CreatedOrUpdatedDate
			)
		END
	ELSE
		BEGIN
			UPDATE RestaurantCompanyMaster
			SET RestaurantCompanyName = @RestaurantCompanyName
			   ,Description = @Description
			   ,Website = @Website
			   ,Phone = @Phone
			   ,UpdatedBy = @CreatedOrUpdatedBy
			   ,UpdatedDate = @CreatedOrUpdatedDate
			WHERE RestaurantCompanyId = @RestaurantCompanyId
			AND IsDeleted = 0
		END

	SELECT
		rcm.RestaurantCompanyId
	   ,rcm.RestaurantCompanyName
	   ,rcm.Description
	   ,rcm.Website
	   ,rcm.Phone
	   ,rcm.CreatedBy
	   ,rcm.CreatedDate
	   ,rcm.UpdatedBy
	   ,rcm.UpdatedDate
	   ,rcm.IsDeleted
	FROM RestaurantCompanyMaster rcm
	WHERE rcm.RestaurantCompanyId = @RestaurantCompanyId
END;

GO
/****** Object:  StoredProcedure [dbo].[usp_Restaurant_IU]    Script Date: 03-12-2021 20:33:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [dbo].[usp_Restaurant_IU]
(
	@RestaurantId UNIQUEIDENTIFIER = NULL
   ,@RestaurantCompanyId UNIQUEIDENTIFIER
   ,@RestaurantName NVARCHAR(510)
   ,@Description NVARCHAR(510)
   ,@Website NVARCHAR(510)
   ,@Phone NVARCHAR(510)
   ,@StreetAddress NVARCHAR(1000)
   ,@State NVARCHAR(510)
   ,@Country NVARCHAR(510)
   ,@OpeningTime TIME
   ,@ClosingTime TIME
   ,@Latitude DECIMAL(12, 9)
   ,@Longitude DECIMAL(12, 9)
   ,@CreatedOrUpdatedBy UNIQUEIDENTIFIER
   ,@CreatedOrUpdatedDate DATETIME2
)
-- WITH ENCRYPTION, RECOMPILE, EXECUTE AS CALLER|SELF|OWNER| 'user_name'
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT
				1
			FROM dbo.RestaurantCompanyMaster rcm
			WHERE rcm.RestaurantCompanyId = @RestaurantCompanyId
			AND rcm.IsDeleted = 0)
		BEGIN

			RAISERROR ('Invalid Restaurant Company', 16, 1);

		END

	IF @RestaurantId IS NULL
		BEGIN
			SET @RestaurantId = NEWID();

			INSERT INTO RestaurantMaster
			(
				RestaurantId
			   ,RestaurantCompanyId
			   ,RestaurantName
			   ,Description
			   ,Website
			   ,Phone
			   ,StreetAddress
			   ,State
			   ,Country
			   ,OpeningTime
			   ,ClosingTime
			   ,Latitude
			   ,Longitude
			   ,CurrentStatus
			   ,CreatedBy
			   ,CreatedDate
			   ,UpdatedBy
			   ,UpdatedDate
			   ,IsDeleted
			)
			VALUES
			(
				@RestaurantId
			   ,@RestaurantCompanyId
			   ,@RestaurantName
			   ,@Description
			   ,@Website
			   ,@Phone
			   ,@StreetAddress
			   ,@State
			   ,@Country
			   ,@OpeningTime
			   ,@ClosingTime
			   ,@Latitude
			   ,@Longitude
			   ,2
			   ,@CreatedOrUpdatedBy
			   ,@CreatedOrUpdatedDate
			   ,@CreatedOrUpdatedBy
			   ,@CreatedOrUpdatedDate
			   ,0
			)
		END
	ELSE
		BEGIN
			UPDATE RestaurantMaster
			SET RestaurantName = @RestaurantName
			   ,Description = @Description
			   ,Website = @Website
			   ,Phone = @Phone
			   ,StreetAddress = @StreetAddress
			   ,State = @State
			   ,Country = @Country
			   ,Latitude = @Latitude
			   ,Longitude = @Longitude
			   ,OpeningTime = @OpeningTime
			   ,ClosingTime = @ClosingTime
			   ,UpdatedBy = @CreatedOrUpdatedBy
			   ,UpdatedDate = @CreatedOrUpdatedDate
			WHERE RestaurantId = @RestaurantId;
		END


	SELECT
		rm.RestaurantId
	   ,rm.RestaurantCompanyId
	   ,rm.RestaurantName
	   ,rm.Description
	   ,rm.Website
	   ,rm.Phone
	   ,rm.StreetAddress
	   ,rm.State
	   ,rm.Country
	   ,rm.OpeningTime
	   ,rm.ClosingTime
	   ,rm.CurrentStatus
	   ,rm.CreatedBy
	   ,rm.CreatedDate
	   ,rm.UpdatedBy
	   ,rm.UpdatedDate
	   ,rm.IsDeleted
	   ,rm.Latitude
	   ,rm.Longitude
	FROM RestaurantMaster rm
	WHERE rm.RestaurantId = @RestaurantId;
END

GO
/****** Object:  StoredProcedure [dbo].[usp_AssignRestaurantUsers]    Script Date: 07-12-2021 16:13:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [dbo].[usp_AssignRestaurantUsers]
(
	@UserIds NVARCHAR(MAX)
   ,@RestaurantId UNIQUEIDENTIFIER
   ,@AssignedBy UNIQUEIDENTIFIER
)
-- WITH ENCRYPTION, RECOMPILE, EXECUTE AS CALLER|SELF|OWNER| 'user_name'
AS
BEGIN


	--- VALIDATING Users Data

	IF EXISTS (SELECT
				1
			FROM STRING_SPLIT(@UserIds, ',') AS ss
			LEFT JOIN UserInfo ui
				ON ui.UserId = dbo.udf_StringToUUID(ss.value)
			WHERE ui.UserId IS NULL
			OR ui.UserRoleId <> 4)
		BEGIN
			RAISERROR ('INVALID_USERID_IN_STRING', 16, 1);
		END

	IF EXISTS (SELECT
				1
			FROM dbo.RestaurantUserMapping rum
			WHERE rum.UserId IN (@UserIds))
		BEGIN
			RAISERROR ('USER_ALREADY_ASSIGNED', 16, 1);

		END

	---- Completing User Validation


	INSERT INTO RestaurantUserMapping
	(
		RestaurantId
	   ,UserId
	   ,AssignedBy
	)
	SELECT
		@RestaurantId
	   ,dbo.udf_StringToUUID(ss.value) AS UserId
	   ,@AssignedBy
	FROM STRING_SPLIT(@UserIds, ',') AS ss

END