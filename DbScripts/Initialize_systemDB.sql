-- =============================================
-- 1. Database and User Creation
-- =============================================
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'LeaveSystemDB')
BEGIN
    CREATE DATABASE LeaveSystemDB;
    PRINT 'Database LeaveSystemDB created.';
END
ELSE
BEGIN
    PRINT 'Database LeaveSystemDB already exists.';
END
GO

IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = N'leavesystemlogin')
BEGIN
    CREATE LOGIN leavesystemlogin WITH PASSWORD = 'leavesystemlogin@#$234';
    PRINT 'Login leavesystemlogin created.';
END
ELSE
BEGIN
    PRINT 'Login leavesystemlogin already exists.';
END
GO

USE LeaveSystemDB;
GO

IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = N'leavesystemlogin')
BEGIN
    CREATE USER leavesystemlogin FOR LOGIN leavesystemlogin;
    PRINT 'User leavesystemlogin created in LeaveSystemDB.';
END
ELSE
BEGIN
    PRINT 'User leavesystemlogin already exists in LeaveSystemDB.';
END
GO

ALTER ROLE db_owner ADD MEMBER leavesystemlogin;
PRINT 'Added leavesystemlogin to db_owner role.';
GO


-- =============================================
-- 2. Table Creation
-- =============================================
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
		Role INT NOT NULL               -- 0=employee,1=approver,2=CEO
        CONSTRAINT DF_Employees_Role DEFAULT(0),
        FOREIGN KEY (ManagerId) REFERENCES Employees(Id)
    );
    PRINT 'Table Employees created.';
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
    PRINT 'Table LeaveRequests created.';
END
GO


-- =============================================
-- 3. Data Insertion
-- =============================================
IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '0001')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    VALUES 
        ('Linda Jenkins','0001','lindaj@amce.com',NULL,NULL,2);
END

-- Approvers (0002, 0003)
IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '0002')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Milton Coleman','0002','miltoncoleman@amce.com','+2755937274', m.Id, 1
    FROM Employees m WHERE m.EmployeeNumber = '0001';
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '0003')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Colin Horton','0003','colinhorton@acme.com','+27209157545', m.Id, 1
    FROM Employees m WHERE m.EmployeeNumber = '0001';
END

-- Other employees (Role = 0)
IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '2005')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Ella Jefferson','2005','ellajefferson@acme.com','+2755979367', m.Id, 0
    FROM Employees m WHERE m.EmployeeNumber = '0003';
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '2006')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Earl Craig','2006','earlcraig@acme.com','+27209165608', m.Id, 0
    FROM Employees m WHERE m.EmployeeNumber = '0003';
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '2008')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Marsha Murphy','2008','marshamurphy@acme.com','+3655949891', m.Id, 0
    FROM Employees m WHERE m.EmployeeNumber = '0003';
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '2009')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Luis Ortega','2009','luisortega@acme.com','+27209171339', m.Id, 0
    FROM Employees m WHERE m.EmployeeNumber = '0003';
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '2010')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Faye Dennis','2010','fayedennis@acme.com',NULL, m.Id, 0
    FROM Employees m WHERE m.EmployeeNumber = '0003';
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '2012')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Amy Burns','2012','amyburns@acme.com','+27209141775', m.Id, 0
    FROM Employees m WHERE m.EmployeeNumber = '0003';
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '2013')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Darrel Weber','2013','darrelweber@acme.com','+2755615463', m.Id, 0
    FROM Employees m WHERE m.EmployeeNumber = '0003';
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1005')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Charlotte Osborne','1005','charlotteosborne@acme.com','+2755760177', m.Id, 0
    FROM Employees m WHERE m.EmployeeNumber = '0002';
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1006')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Marie Walters','1006','mariewalters@acme.com','+27209186908', m.Id, 0
    FROM Employees m WHERE m.EmployeeNumber = '0002';
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1008')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Leonard Gill','1008','leonardgill@acme.com','+2755525585', m.Id, 0
    FROM Employees m WHERE m.EmployeeNumber = '0002';
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1009')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Enrique Thomas','1009','enriquethomas@acme.com','+27209161335', m.Id, 0
    FROM Employees m WHERE m.EmployeeNumber = '0002';
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1010')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Omar Dunn','1010','omardunn@acme.com',NULL, m.Id, 0
    FROM Employees m WHERE m.EmployeeNumber = '0002';
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1012')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Dewey George','1012','deweygeorge@acme.com','+2755260127', m.Id, 0
    FROM Employees m WHERE m.EmployeeNumber = '0002';
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1013')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Rudy Lewis','1013','rudylewis@acme.com',NULL, m.Id, 0
    FROM Employees m WHERE m.EmployeeNumber = '0002';
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1015')
BEGIN
    INSERT INTO Employees 
        (FullName, EmployeeNumber, EmailAddress, CellphoneNumber, ManagerId, Role)
    SELECT 
        'Neal French','1015','nealfrench@acme.com','+27209194882', m.Id, 0
    FROM Employees m WHERE m.EmployeeNumber = '0002';
END
GO



-- =============================================
-- 4. Set Up Manager Relationships
-- =============================================
-- These employees report to 0001
UPDATE e
SET e.ManagerId = m.Id
FROM Employees e
JOIN Employees m ON m.EmployeeNumber = '0001'
WHERE e.EmployeeNumber IN ('0002', '0003');

-- These employees report to 0003
UPDATE e
SET e.ManagerId = m.Id
FROM Employees e
JOIN Employees m ON m.EmployeeNumber = '0003'
WHERE e.EmployeeNumber IN ('2005', '2006', '2008', '2009', '2010', '2012', '2013');

-- These employees report to 0002
UPDATE e
SET e.ManagerId = m.Id
FROM Employees e
JOIN Employees m ON m.EmployeeNumber = '0002'
WHERE e.EmployeeNumber IN ('1005', '1006', '1008', '1009', '1010', '1012', '1013', '1015');
GO
