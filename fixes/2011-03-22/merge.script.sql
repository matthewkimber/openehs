USE OpenEHS_database;

CREATE TABLE Temp
(
    TempID                      int                 AUTO_INCREMENT  PRIMARY KEY         NOT NULL,
    FirstName                   varchar(50)         NULL,
    MiddleName                  varchar(50)         NULL,
    LastName                    varchar(50)         NULL,
    PhoneNumber                 varchar(20)         NULL,
    StaffType                   tinyint(1)          NULL,
    LicenseNumber               varchar(20)         NULL            Default NULL,
    AddressID                   int                 NULL,
    Username                    varchar(50)         NULL,
    EmailAddress                varchar(50)         NULL,
    ApplicationName             varchar(50)         NULL,
    `Password`                  varchar(30)         NULL,
    PasswordQuestion            varchar(50)         NULL,
    PasswordAnswer              varchar(50)         NULL,
    DateCreated                 timestamp           NULL            DEFAULT NOW(),
    LastLogin                   datetime            NULL,
    LastActivity                datetime            NULL,
    LastPasswordChange          datetime            NULL,
    IsOnline                    bit(1)              NULL            DEFAULT 0,
    IpAddress                   varchar(20)         NULL,
    IsLockedOut                 bit(1)              NULL            DEFAULT 0,
    FailedPasswordAttemptCount  int                 NULL            DEFAULT 0,
    IsApproved                  bit(1)              NULL            DEFAULT 0,
    IsActive                    bit(1)              NULL            DEFAULT 1
);

DELIMITER |
CREATE PROCEDURE sp_mergeStaffUser
(
    IN i_ID1    int
)

BEGIN

DECLARE _FirstName                        varchar(50);
DECLARE _MiddleName                       varchar(50);
DECLARE _LastName                         varchar(50);
DECLARE _PhoneNumber                      varchar(20);
DECLARE _StaffType                        tinyint;
DECLARE _LicenseNumber                    varchar(20);
DECLARE _AddressID                        int;
DECLARE _UserID                           int;
DECLARE _UserName                         varchar(50);
DECLARE _EmailAddress                     varchar(50);
DECLARE _ApplicationName                  varchar(50);
DECLARE _Password                         varchar(30);
DECLARE _PasswordQuestion                 varchar(50);
DECLARE _PasswordAnswer                   varchar(50);
DECLARE _DateCreated                      timestamp;
DECLARE _LastLogin                        datetime;
DECLARE _LastActivity                     datetime;
DECLARE _LastPasswordChange               datetime;
DECLARE _IsOnline                         bit(1);
DECLARE _IpAddress                        varchar(20);
DECLARE _IsLockedOut                      bit(1);
DECLARE _FailedPasswordAttemptCount       int;
DECLARE _IsApproved                       bit(1);
DECLARE _IsActive                         bit(1);

SELECT FirstName FROM Staff WHERE StaffID = i_ID1 INTO _FirstName;
SELECT MiddleName FROM Staff WHERE StaffID = i_ID1 INTO _MiddleName;
SELECT LastName FROM Staff WHERE StaffID = i_ID1 INTO _LastName;
SELECT PhoneNumber FROM Staff WHERE StaffID = i_ID1 INTO _PhoneNumber;
SELECT StaffType FROM Staff WHERE StaffID = i_ID1 INTO _StaffType;
SELECT LicenseNumber FROM Staff WHERE StaffID = i_ID1 INTO _LicenseNumber;
SELECT AddressID FROM Staff WHERE StaffID = i_ID1 INTO _AddressID;

SELECT UserID FROM Staff WHERE StaffID = i_ID1 INTO _UserID;

SELECT UserName FROM User WHERE UserID = _UserID INTO _UserName;
SELECT EmailAddress FROM User WHERE UserID = _UserID INTO _EmailAddress;
SELECT ApplicationName FROM User WHERE UserID = _UserID INTO _ApplicationName;
SELECT Password FROM User WHERE UserID = _UserID INTO _Password;
SELECT PasswordQuestion FROM User WHERE UserID = _UserID INTO _PasswordQuestion;
SELECT PasswordAnswer FROM User WHERE UserID = _UserID INTO _PasswordAnswer;
SELECT DateCreated FROM User WHERE UserID = _UserID INTO _DateCreated;
SELECT LastLogin FROM User WHERE UserID = _UserID INTO _LastLogin;
SELECT LastActivity FROM User WHERE UserID = _UserID INTO _LastActivity;
SELECT LastPasswordChange FROM User WHERE UserID = _UserID INTO _LastPasswordChange;
SELECT IsOnline FROM User WHERE UserID = _UserID INTO _IsOnline;
SELECT IpAddress FROM User WHERE UserID = _UserID INTO _IpAddress;
SELECT IsLockedOut FROM User WHERE UserID = _UserID INTO _IsLockedOut;
SELECT FailedPasswordAttemptCount FROM User WHERE UserID = _UserID INTO _FailedPasswordAttemptCount;
SELECT IsApproved FROM User WHERE UserID = _UserID INTO _IsApproved;

SET _IsActive = 1;


INSERT INTO Temp
(
    FirstName,
    MiddleName,
    LastName,
    PhoneNumber,
    StaffType,
    LicenseNumber,
    AddressID,
    Username,
    EmailAddress,
    ApplicationName,
    `Password`,
    PasswordQuestion,
    PasswordAnswer,
    DateCreated,
    LastLogin,
    LastActivity,
    LastPasswordChange,
    IsOnline,
    IpAddress,
    IsLockedOut,
    FailedPasswordAttemptCount,
    IsApproved,
    IsActive
)
VALUES
(
    _FirstName,
    _MiddleName,
    _LastName,
    _PhoneNumber,
    _StaffType,
    _LicenseNumber,
    _AddressID,
    _UserName,
    _EmailAddress,
    _ApplicationName,
    _Password,
    _PasswordQuestion,
    _PasswordAnswer,
    _DateCreated,
    _LastLogin,
    _LastActivity,
    _LastPasswordChange,
    _IsOnline,
    _IpAddress,
    _IsLockedOut,
    _FailedPasswordAttemptCount,
    _IsApproved,
    _IsActive
);

END ||

DELIMITER ;

CALL sp_mergeStaffUser
(
    1
);

CALL sp_mergeStaffUser
(
    3
);

CALL sp_mergeStaffUser
(
    4
);

CALL sp_mergeStaffUser
(
    5
);

CALL sp_mergeStaffUser
(
    6
);

CALL sp_mergeStaffUser
(
    7
);

CALL sp_mergeStaffUser
(
    8
);

CALL sp_mergeStaffUser
(
    9
);

CALL sp_mergeStaffUser
(
    10
);

CALL sp_mergeStaffUser
(
    12
);

ALTER TABLE Staff
DROP FOREIGN KEY FK_StaffMustHaveAddressID;

ALTER TABLE Staff
DROP FOREIGN KEY FK_StaffMustHaveUserID;

ALTER TABLE PatientCheckIn
DROP FOREIGN KEY FK_PatientCheckInMustHaveStaffID;

ALTER TABLE Note
DROP FOREIGN KEY FK_NoteMustHaveStaffID;

ALTER TABLE SurgeryStaff
DROP FOREIGN KEY FK_SurgeryStaffMustHaveStaffID;

ALTER TABLE Template
DROP FOREIGN KEY TemplateMustHaveStaffID;

ALTER TABLE PatientCheckIn CHANGE StaffID UserID   int;

ALTER TABLE SurgeryStaff CHANGE StaffID UserID   int;

ALTER TABLE Note CHANGE StaffID UserID   int;

ALTER TABLE Template CHANGE StaffID UserID   int;

DROP TABLE `User`;

DROP TABLE Staff;

RENAME TABLE Temp TO `User`;

ALTER TABLE `User` CHANGE TempID UserID                 int         AUTO_INCREMENT;
ALTER TABLE `User` MODIFY FirstName                     varchar(50) NOT NULL;
ALTER TABLE `User` MODIFY LastName                      varchar(50) NOT NULL;
ALTER TABLE `User` MODIFY StaffType                     tinyint     NOT NULL;
ALTER TABLE `User` MODIFY AddressID                     int         NOT NULL;
ALTER TABLE `User` MODIFY UserName                      varchar(50) NOT NULL;
ALTER TABLE `User` MODIFY `Password`                    varchar(30) NOT NULL;
ALTER TABLE `User` MODIFY DateCreated                   timestamp   NOT NULL;
ALTER TABLE `User` MODIFY IsOnline                      bit         NOT NULL;
ALTER TABLE `User` MODIFY IsLockedOut                   bit         NOT NULL;
ALTER TABLE `User` MODIFY FailedPasswordAttemptCount    int         NOT NULL;
ALTER TABLE `User` MODIFY IsApproved                    bit         NOT NULL;
ALTER TABLE `User` MODIFY IsActive                      bit         NOT NULL;

ALTER TABLE `User`
ADD CONSTRAINT FK_UserMustHaveAddressID
FOREIGN KEY (AddressID) REFERENCES Address(AddressID);

ALTER TABLE PatientCheckIn
ADD CONSTRAINT FK_PatientCheckInMustHaveUserID
FOREIGN KEY (UserID) REFERENCES User(UserID);

ALTER TABLE Note
ADD CONSTRAINT FK_NotenMustHaveUserID
FOREIGN KEY (UserID) REFERENCES User(UserID);

ALTER TABLE Template
ADD CONSTRAINT FK_TemplateMustHaveUserID
FOREIGN KEY (UserID) REFERENCES User(UserID);

ALTER TABLE SurgeryStaff
ADD CONSTRAINT FK_SurgeryStaffMustHaveUserID
FOREIGN KEY (UserID) REFERENCES User(UserID);