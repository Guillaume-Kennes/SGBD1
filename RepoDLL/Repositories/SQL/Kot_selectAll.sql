SELECT KOT_ID, KOT_NAME, ETU_MATRICULE, ETU_NOM, ETU_PRENOM
FROM dbo.Kot
left join dbo.Etudiant on Etudiant.Etu_Id = Kot.KOT_ETUDIANT_ID
