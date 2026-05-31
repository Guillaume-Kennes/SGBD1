USE [SGBD]

DELETE FROM [dbo].[Etudiant];

INSERT INTO [dbo].[Etudiant]
           ([ETU_NOM]
           ,[ETU_PRENOM]
           ,[ETU_MATRICULE]
           ,[ETU_EMAIL])
     VALUES
           ('Doe',	'John',	'HE03',	'john.doe@gmail.com'),
           ('Dafiduck', 'John', 'HE01', 'johndoe@gmail.com'),
           ('test', NULL, 'HE02', NULL)
