DROP DATABASE Merge_database;

CREATE DATABASE Merge_database;

USE Merge_database;

CREATE TABLE TableOne
(
TableOneID      int             AUTO_INCREMENT          PRIMARY KEY         NOT NULL,
FName           varchar(30)     NOT NULL,
LName           varchar(30)     NOT NULL
);

CREATE TABLE TableTwo
(
TableTwoID      int             AUTO_INCREMENT          PRIMARY KEY         NOT NULL,
FakeRowOne      varchar(30)     NOT NULL,
FakeRowTwo      varchar(30)     NOT NULL,
FakeRowThree    varchar(30)     NULL,
TableOneID      int             NOT NULL
);

CREATE TABLE Temp
(
TempID          int             AUTO_INCREMENT          PRIMARY KEY         NOT NULL,
FName           varchar(30)     NULL,
LName           varchar(30)     NULL,
FakeRowOne      varchar(30)     NULL,
FakeRowTwo      varchar(30)     NULL,
FakeRowThree    varchar(30)     NULL
);

ALTER TABLE TableTwo
ADD CONSTRAINT FK_TableTwoMustHaveTableOneID
FOREIGN KEY (TableOneID) REFERENCES TableOne(TableOneID);

/**********************************************
Test Data
**********************************************/

INSERT INTO TableOne
(
FName,
LName
)
VALUES
(
'Cameron',
'Harp'
);

INSERT INTO TableOne
(
FName,
LName
)
VALUES
(
'JD',
'Russel'
);

INSERT INTO TableOne
(
FName,
LName
)
VALUES
(
'Matt',
'Kimber'
);

INSERT INTO TableOne
(
FName,
LName
)
VALUES
(
'Jennifer',
'Harp'
);

INSERT INTO TableOne
(
FName,
LName
)
VALUES
(
'Rich',
'Fry'
);

INSERT INTO TableTwo
(
FakeRowOne,
FakeRowTwo,
FakeRowThree,
TableOneID
)
VALUES
(
'Something',
'Something Else',
'Something More',
1
);

INSERT INTO TableTwo
(
FakeRowOne,
FakeRowTwo,
FakeRowThree,
TableOneID
)
VALUES
(
'Blah',
'Blah Else',
'Blah More',
2
);

INSERT INTO TableTwo
(
FakeRowOne,
FakeRowTwo,
FakeRowThree,
TableOneID
)
VALUES
(
'Test',
'Test Else',
'Test More',
3
);

INSERT INTO TableTwo
(
FakeRowOne,
FakeRowTwo,
FakeRowThree,
TableOneID
)
VALUES
(
'Dont know what to put here',
'More random stuff',
null,
4
);

INSERT INTO TableTwo
(
FakeRowOne,
FakeRowTwo,
FakeRowThree,
TableOneID
)
VALUES
(
'Please work for me',
'This sucks',
'Holy cow',
5
);

DELIMITER |
CREATE PROCEDURE sp_combo
(
IN i_ID1     int,
IN i_ID2     int
)

BEGIN

DECLARE _FName           varchar(30);
DECLARE _LName           varchar(30);
DECLARE _FakeRowOne      varchar(30);
DECLARE _FakeRowTwo      varchar(30);
DECLARE _FakeRowThree    varchar(30);

SELECT FName FROM TableOne WHERE TableOneID = i_ID1 INTO _FName;
SELECT LName FROM TableOne WHERE TableOneID = i_ID1 INTO _LName;
SELECT FakeRowOne FROM TableTwo WHERE TableTwoID = i_ID2 INTO _FakeRowOne;
SELECT FakeRowTwo FROM TableTwo WHERE TableTwoID = i_ID2 INTO _FakeRowTwo;
SELECT FakeRowThree FROM TableTwo WHERE TableTwoID = i_ID2 INTO _FakeRowThree;

INSERT INTO Temp
(
FName,
LName,
FakeRowOne,
FakeRowTwo,
FakeRowThree
)
VALUES
(
_FName,
_LName,
_FakeRowOne,
_FakeRowTwo,
_FakeRowThree
);

END ||
DELIMITER ;

CALL sp_combo
(
1,
1
);

CALL sp_combo
(
2,
2
);

CALL sp_combo
(
3,
3
);

CALL sp_combo
(
4,
4
);

CALL sp_combo
(
5,
5
);

/*********************************
Alter the temp table
*********************************/

RENAME TABLE Temp TO Whatever;
ALTER TABLE Whatever CHANGE TempID WhateverID   int;
ALTER TABLE Whatever MODIFY FName varchar(30) NOT NULL;
ALTER TABLE Whatever MODIFY LName varchar(30) NOT NULL;
ALTER TABLE Whatever MODIFY FakeRowOne varchar(30) NOT NULL;
ALTER TABLE Whatever MODIFY FakeRowTwo varchar(30) NOT NULL;