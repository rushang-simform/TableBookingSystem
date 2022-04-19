/*

----------------------------------------- Table Creation Started   -----------------------------------

*/

CREATE TABLE IF NOT EXISTS UserRoleMaster (
  UserRoleId int PRIMARY KEY AUTO_INCREMENT,
  UserRoleName varchar(255)
);

CREATE TABLE IF NOT EXISTS UserMaster (
  UserId char(36) PRIMARY KEY,
  EmailId varchar(255) UNIQUE NOT NULL,
  UserRoleId int,
  FirstName varchar(255),
  LastName varchar(255),
  Password varchar(255),
  PasswordSalt varchar(255),
  IsActive bit,
  CreatedBy int,
  CreateDate datetime,
  UpdatedBy int,
  UpdatedDate datetime
);


CREATE TABLE IF NOT EXISTS RestaurantCompanyMaster (
  RestaurantCompanyId char(36) PRIMARY KEY,
  RestaurantCompanyName nvarchar(255),
  Description nvarchar(255),
  Website nvarchar(255),
  Phone nvarchar(255),
  CreatedBy char(36),
  CreateDate datetime,
  UpdatedBy char(36),
  UpdatedDate datetime
);

CREATE TABLE IF NOT EXISTS RestaurantMaster (
  RestaurantId char(36) PRIMARY KEY,
  RestaurantCompanyId char(36),
  RestaurantName nvarchar(255),
  Description nvarchar(255),
  Website nvarchar(255),
  Phone nvarchar(255),
  StreetAddress nvarchar(255),
  State nvarchar(255),
  Country nvarchar(255),
  OpeningTime datetime,
  ClosingTime datetime,
  Langitude datetime,
  Longitude datetime,
  CurrentStatus bit,
  CreatedBy char(36),
  CreateDate datetime,
  UpdatedBy char(36),
  UpdatedDate datetime
);


CREATE TABLE IF NOT EXISTS RestaurantUserMapping (
  RestaurantId char(36),
  UserId char(36)
);


CREATE TABLE IF NOT EXISTS RestaurantHolidayMapping (
  RestaurantHolidyId int PRIMARY KEY,
  RestaurantId char(36),
  HolidayDate date,
  CreatedBy char(36),
  CreateDate datetime,
  UpdatedBy char(36),
  UpdatedDate datetime
);

CREATE TABLE IF NOT EXISTS RestaurantTableType (
  TableTypeId int PRIMARY KEY,
  TableTypeName nvarchar(255)
);

CREATE TABLE IF NOT EXISTS RestaurantTableMaster (
  TableId char(36) PRIMARY KEY,
  TableName nvarchar(255),
  TotalSeats int,
  CurrentStatus bit,
  TableTypeId int,
  RestaurantId char(36),
  CreatedBy char(36),
  CreateDate datetime,
  UpdatedBy char(36),
  UpdatedDate datetime
);

CREATE TABLE IF NOT EXISTS TableTagMaster (
  TableTagId char(36) PRIMARY KEY,
  TableTagName nvarchar(255)
);

CREATE TABLE IF NOT EXISTS TableTagMapping (
  TableId char(36),
  TableTagId char(36)
);

CREATE TABLE IF NOT EXISTS BookingStatusMaster (
  BookingStatusId char(36) PRIMARY KEY,
  BookingStatusName nvarchar(255)
);


CREATE TABLE IF NOT EXISTS RestaurantBookingMaster (
  BookingId char(36) PRIMARY KEY,
  BookingBy char(36),
  BookingDate datetime,
  MinHours int,
  BookingStatus char(36),
  TableIds int,
  ConfirmedBy char(36)
);


/*

----------------------------------------- Table Creation Completed   -----------------------------------

*/

/*

----------------------------------------- Adding Foreign Key and Constraints Started  -----------------------------------

*/
ALTER TABLE UserMaster ADD FOREIGN KEY (UserRoleId) REFERENCES UserRoleMaster (UserRoleId);

ALTER TABLE RestaurantCompanyMaster ADD FOREIGN KEY (CreatedBy) REFERENCES UserMaster (UserId);

ALTER TABLE RestaurantCompanyMaster ADD FOREIGN KEY (UpdatedBy) REFERENCES UserMaster (UserId);

ALTER TABLE RestaurantMaster ADD FOREIGN KEY (RestaurantCompanyId) REFERENCES RestaurantCompanyMaster (RestaurantCompanyId);

ALTER TABLE RestaurantMaster ADD FOREIGN KEY (CreatedBy) REFERENCES UserMaster (UserId);

ALTER TABLE RestaurantMaster ADD FOREIGN KEY (UpdatedBy) REFERENCES UserMaster (UserId);

ALTER TABLE RestaurantUserMapping ADD FOREIGN KEY(RestaurantId) REFERENCES RestaurantMaster(RestaurantId);

ALTER TABLE RestaurantUserMapping ADD FOREIGN KEY(UserId) REFERENCES UserMaster(UserId);

ALTER TABLE RestaurantHolidayMapping ADD FOREIGN KEY (RestaurantId) REFERENCES RestaurantMaster (RestaurantId);

ALTER TABLE RestaurantHolidayMapping ADD FOREIGN KEY (CreatedBy) REFERENCES UserMaster (UserId);

ALTER TABLE RestaurantHolidayMapping ADD FOREIGN KEY (UpdatedBy) REFERENCES UserMaster (UserId);

ALTER TABLE RestaurantTableMaster ADD FOREIGN KEY (TableTypeId) REFERENCES RestaurantTableType (TableTypeId);

ALTER TABLE RestaurantTableMaster ADD FOREIGN KEY (RestaurantId) REFERENCES RestaurantMaster (RestaurantId);

ALTER TABLE RestaurantTableMaster ADD FOREIGN KEY (CreatedBy) REFERENCES UserMaster (UserId);

ALTER TABLE RestaurantTableMaster ADD FOREIGN KEY (UpdatedBy) REFERENCES UserMaster (UserId);

ALTER TABLE TableTagMapping ADD FOREIGN KEY (TableId) REFERENCES RestaurantTableMaster (TableId);

ALTER TABLE TableTagMapping ADD FOREIGN KEY (TableTagId) REFERENCES TableTagMaster (TableTagId);

ALTER TABLE RestaurantBookingMaster ADD FOREIGN KEY (BookingStatus) REFERENCES BookingStatusMaster (BookingStatusId);

ALTER TABLE RestaurantBookingMaster ADD FOREIGN KEY (BookingBy) REFERENCES UserMaster (UserId);

ALTER TABLE RestaurantBookingMaster ADD FOREIGN KEY (ConfirmedBy) REFERENCES UserMaster (UserId);


/*
----------------------------------------- Adding Foreign Key and Constraints Completed  -----------------------------------
*/


/*
----------------------------------------- Adding Static Entries  -----------------------------------
*/


INSERT INTO UserRoleMaster (UserRoleId, UserRoleName)
  VALUES (1, 'Super Admin');
INSERT INTO UserRoleMaster (UserRoleId, UserRoleName)
  VALUES (2, 'Admin');
INSERT INTO UserRoleMaster (UserRoleId, UserRoleName)
  VALUES (3, 'Restaurant Admin');
INSERT INTO UserRoleMaster (UserRoleId, UserRoleName)
  VALUES (4, 'Customer');