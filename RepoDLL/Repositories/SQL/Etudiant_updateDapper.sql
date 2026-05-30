update Etudiant set ETU_NOM = @LastName, ETU_PRENOM = @FirstName, ETU_MATRICULE = @Matricule, ETU_EMAIL = @Email
where ETU_ID = @Id;