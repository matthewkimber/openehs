-- ---------------------------------------------------------------------
-- Fixes a crashing error in the db when a user has no associated staff.
-- Simply deletes the erroneous user.
-- ---------------------------------------------------------------------

USE openehs_database;

DELETE FROM User WHERE UserID = 9;
DELETE FROM User WHERE UserID = 10;
DELETE FROM User WHERE UserID = 13;