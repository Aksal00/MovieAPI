-- Check if database exists, if not create it
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MoviesDb')
BEGIN
    CREATE DATABASE MoviesDb;
END
GO

-- Switch to MovieDb
USE MoviesDb;
GO

-- Check if table exists, if not create it
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Movies')
BEGIN
    CREATE TABLE Movies (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ImdbId NVARCHAR(20) NOT NULL UNIQUE,
        Title NVARCHAR(255) NOT NULL,
        Year NVARCHAR(10),
        Type NVARCHAR(50),
        Poster NVARCHAR(500),
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
END
GO