
IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '0002')
INSERT INTO Employees (FullName, EmployeeNumber, EmailAddress, CellphoneNumber) VALUES
('Milton Coleman', '0002', 'miltoncoleman@amce.com', '+27 55 937 274');

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '0003')
INSERT INTO Employees (FullName, EmployeeNumber, EmailAddress, CellphoneNumber) VALUES
('Colin Horton', '0003', 'colinhorton@amce.com', '+27 20 915 7545');

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1005')
INSERT INTO Employees (FullName, EmployeeNumber, EmailAddress, CellphoneNumber) VALUES
('Charlotte Osborne', '1005', 'charlotteosborne@acme.com', '+27 55 760 177');

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1006')
INSERT INTO Employees (FullName, EmployeeNumber, EmailAddress, CellphoneNumber) VALUES
('Marie Walters', '1006', 'mariewalters@acme.com', '+27 20 918 6908');

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1008')
INSERT INTO Employees (FullName, EmployeeNumber, EmailAddress, CellphoneNumber) VALUES
('Leonard Gill', '1008', 'leonardgill@acme.com', '+27 55 525 585');

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1009')
INSERT INTO Employees (FullName, EmployeeNumber, EmailAddress, CellphoneNumber) VALUES
('Enrique Thomas', '1009', 'enriquethomas@acme.com', '+27 20 916 1335');

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1010')
INSERT INTO Employees (FullName, EmployeeNumber, EmailAddress, CellphoneNumber) VALUES
('Omar Dunn', '1010', 'omardunn@acme.com', NULL);

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1012')
INSERT INTO Employees (FullName, EmployeeNumber, EmailAddress, CellphoneNumber) VALUES
('Dewey George', '1012', 'deweygeorge@acme.com', '+27 55 260 127');

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1013')
INSERT INTO Employees (FullName, EmployeeNumber, EmailAddress, CellphoneNumber) VALUES
('Rudy Lewis', '1013', 'rudylewis@acme.com', NULL);

IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = '1015')
INSERT INTO Employees (FullName, EmployeeNumber, EmailAddress, CellphoneNumber) VALUES
('Neal French', '1015', 'nealfrench@acme.com', '+27 20 919 4882');
