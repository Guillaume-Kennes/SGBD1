select
  Etu_Id   as Id,
  Etu_Matricule as Matricule,
  Etu_Prenom as FirstName,
  Etu_Nom  as LastName,
  Etu_Email as Email
from Etudiant
where Etu_Nom like '%' + @lastName + '%';
