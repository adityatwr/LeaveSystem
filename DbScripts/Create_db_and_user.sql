-- 1. Check if database exists, create if not
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

-- 2. Check if login exists, create if not
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

-- 3. Check if user exists in the database, create if not
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

-- 4. Grant privileges if not already granted
-- (This doesn't double-check role membership, but will just execute)
ALTER ROLE db_owner ADD MEMBER leavesystemlogin;
PRINT 'Added leavesystemlogin to db_owner role.';
GO
