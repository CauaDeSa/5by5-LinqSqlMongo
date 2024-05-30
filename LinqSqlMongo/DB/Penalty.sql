CREATE TABLE Penalty 
(
    PenaltyID int IDENTITY(1,1),
    CompanyName varchar(100) not null,
    CNPJ varchar(50) not null,
    DriverName varchar(50) not null,
    CPF varchar(20) not null,
    VigencyDate date not null,
);

GO

CREATE PROCEDURE spInitialize
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Penalty' AND xtype='U')
    BEGIN
        CREATE TABLE Penalty 
        (
            PenaltyID int IDENTITY(1,1),
            CompanyName varchar(100) not null,
            CNPJ varchar(50) not null,
            DriverName varchar(50) not null,
            CPF varchar(20) not null,
            VigencyDate date not null,
        );
    END

    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ProcessControl' AND xtype='U')
    BEGIN
        CREATE TABLE ProcessControl 
        (
            ID int IDENTITY(1,1),
            Description varchar(100) not null,
            Date date not null,
            NumberOfRecords int not null,
        );
    END
END

GO

CREATE PROCEDURE spInsertPenalty
    @CompanyName varchar(100),
    @CNPJ varchar(50),
    @DriverName varchar(50),
    @CPF varchar(20),
    @VigencyDate date
AS
BEGIN
    INSERT INTO Penalty (CompanyName, CNPJ, DriverName, CPF, VigencyDate)
    VALUES (@CompanyName, @CNPJ, @DriverName, @CPF, @VigencyDate);
END;

GO

CREATE PROCEDURE spGetPenalty
    @PenaltyID int
AS
BEGIN
    SELECT * FROM Penalty WHERE PenaltyID = @PenaltyID;
END;

GO

CREATE PROCEDURE spGetAllPenalties
AS
BEGIN
    SELECT * FROM Penalty;
END;