
--- Init DB 

/*******************************************************************************
   Drop database if it exists
********************************************************************************/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'PhotoStudio')
BEGIN
	ALTER DATABASE [PhotoStudio] SET OFFLINE WITH ROLLBACK IMMEDIATE;
	ALTER DATABASE [PhotoStudio] SET ONLINE;
	DROP DATABASE [PhotoStudio];
END

GO

/*******************************************************************************
   Create database
********************************************************************************/
CREATE DATABASE [PhotoStudio];
GO

USE [PhotoStudio];
GO

/*******************************************************************************
   Create tables
********************************************************************************/


CREATE TABLE dbo.Orders 
( 
    Id INTEGER PRIMARY KEY IDENTITY(1,1), 
    CustomerName VARCHAR(50)
); 

CREATE TABLE dbo.Photos
( 
    Id INTEGER PRIMARY KEY IDENTITY(1,1), 
    Name VARCHAR(50)
); 

CREATE TABLE dbo.OrderLines
(
    -- prevent from adding non existing orders / photos 
    OrderId INTEGER REFERENCES Orders(Id) ON DELETE CASCADE, 
    PhotoId INTEGER REFERENCES Photos(Id) ON DELETE CASCADE, 
    PRIMARY KEY(OrderId, PhotoId)
);

GO

/*******************************************************************************
   Insert some data 
********************************************************************************/

USE PhotoStudio; 

INSERT INTO Photos(Name)
VALUES('Photo1.jpg');


INSERT INTO Photos(Name)
VALUES('Photo2.jpg');


INSERT INTO Photos(Name)
VALUES('Photo3.jpg');


INSERT INTO Photos(Name)
VALUES('Photo4.jpg');

INSERT INTO Orders(CustomerName)
VALUES('Adir');

INSERT INTO Orders(CustomerName)
VALUES('Muji');

INSERT INTO Orders(CustomerName)
VALUES('Liam');

INSERT INTO Orders(CustomerName)
VALUES('Chapi');

INSERT INTO OrderLines(OrderId, PhotoId)
VALUES(1,1);

INSERT INTO OrderLines(OrderId, PhotoId)
VALUES(1,3);

INSERT INTO OrderLines(OrderId, PhotoId)
VALUES(2,2);

INSERT INTO OrderLines(OrderId, PhotoId)
VALUES(3,2);

INSERT INTO OrderLines(OrderId, PhotoId)
VALUES(3,4);

INSERT INTO OrderLines(OrderId, PhotoId)
VALUES(4,1);


INSERT INTO OrderLines(OrderId, PhotoId)
VALUES(4,3);

GO