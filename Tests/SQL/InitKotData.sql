USE [SGBD]

delete from [dbo].[Kot];
delete from [dbo].[Etudiant];

SET IDENTITY_INSERT [dbo].[Etudiant] ON;

INSERT INTO [dbo].[Etudiant] ([ETU_ID], [ETU_NOM], [ETU_PRENOM], [ETU_MATRICULE], [ETU_EMAIL])
VALUES
  (6,  'Doe',       'John', '123456', 'john.doe@gmail.com'),
  (12, 'Dafiduck',  'John', 'HE01',   'johndoe@gmail.com'),
  (14, 'test null', NULL,   'HE02',   NULL);

SET IDENTITY_INSERT [dbo].[Etudiant] OFF;

SET IDENTITY_INSERT [dbo].[Kot] ON;

INSERT INTO [dbo].[Kot] ([KOT_ID], [KOT_NAME], [KOT_ETUDIANT_ID])
VALUES
  (1, 'Kot1', 6),
  (2, 'Kot2', 12);

SET IDENTITY_INSERT [dbo].[Kot] OFF;
