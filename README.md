# EntityFrameworkCore
EntityFramework Core Hands On

References:

https://dotnettutorials.net/lesson/entity-framework-core/

https://dotnettutorials.net/lesson/install-entity-framework-core/

https://dotnettutorials.net/lesson/dbcontext-entity-framework-core/

https://dotnettutorials.net/lesson/database-connection-string-in-entity-framework-core/

https://dotnettutorials.net/lesson/crud-operations-in-entity-framework-core/



USE EFCoreDB1
GO
TRUNCATE TABLE Students;
DELETE FROM Branches;

-- Truncate the Foreign Key Table
TRUNCATE TABLE Students;
GO

-- Delete All Records from the Primary Key Table
DELETE FROM Branches;
GO

-- RESEED The Identity
DBCC CHECKIDENT ('EFCoreDB1.dbo.Branches', RESEED, 0);

