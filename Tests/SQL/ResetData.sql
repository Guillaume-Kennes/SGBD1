USE [SGBD];
SET NOCOUNT ON; /*remove the rows affected count message*/

-- remove child rows first (FK)
DELETE FROM dbo.[Kot];
DBCC CHECKIDENT ('dbo.[Kot]', RESEED, 0);

DELETE FROM dbo.[Etudiant];
DBCC CHECKIDENT ('dbo.[Etudiant]', RESEED, 0);
