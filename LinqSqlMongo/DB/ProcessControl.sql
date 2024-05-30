CREATE TABLE ProcessControl 
(
    ID int IDENTITY(1,1),
    Description varchar(100) not null,
    Date date not null,
    NumberOfRecords int not null,
);

GO 

CREATE PROCEDURE spInsertProcessControl
    @Description varchar(100),
    @Date date,
    @NumberOfRecords int
AS
BEGIN
    INSERT INTO ProcessControl (Description, Date, NumberOfRecords)
    VALUES (@Description, @Date, @NumberOfRecords)
END