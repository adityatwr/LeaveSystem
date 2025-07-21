IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Employees')
    PRINT 'Table Employees exists.';
ELSE
BEGIN
    CREATE TABLE Employees (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        FullName NVARCHAR(100) NOT NULL,
        EmployeeNumber NVARCHAR(10) NOT NULL UNIQUE,
        EmailAddress NVARCHAR(100) NOT NULL,
        CellphoneNumber NVARCHAR(20) NULL,
        ManagerId INT NULL,  -- Self-referencing FK
        IsDeleted BIT NOT NULL DEFAULT(0),
        DeletedAt DATETIME NULL,
        FOREIGN KEY (ManagerId) REFERENCES Employees(Id)
    );
END
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'LeaveRequests')
    PRINT 'Table LeaveRequests exists.';
ELSE
BEGIN
    CREATE TABLE LeaveRequests (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        EmployeeId INT NOT NULL,
        LeaveType NVARCHAR(50) NOT NULL,
        StartDate DATE NOT NULL,
        EndDate DATE NOT NULL,
        Status NVARCHAR(50) NOT NULL,
        ManagerRemarks NVARCHAR(255) NULL,
        RequestedOn DATETIME NOT NULL DEFAULT(GETDATE()),
        ApproverId INT NULL,
        IsDeleted BIT NOT NULL DEFAULT(0),
        DeletedAt DATETIME NULL,
        FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
        FOREIGN KEY (ApproverId) REFERENCES Employees(Id)
    );
END
GO
