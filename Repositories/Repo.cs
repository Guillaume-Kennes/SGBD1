using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SGBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBD.Interfaces;

namespace SGBD.Repositories {
    public class Repo : IRepo {


        private readonly string connectionString = @"Server=MSI\MSSQL; DataBase=SGBD; User ID=sa; Password=GuillaumeK15_; TrustServerCertificate=True;";
        private readonly ILogger<Repo> _logger;

        public Repo(ILogger<Repo> logger) {
            _logger = logger;
        }

        public List<Student> GetAll() {

            List<Student> list = new List<Student>();

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();

            }

            return list;
        }


        public void Add(Student student) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                string querry = "insert into Etudiant(ETU_NOM, ETU_PRENOM, ETU_MATRICULE, ETU_EMAIL) values (@Nom, @Prenom, @Matricule, @Email)";

                using (SqlCommand command = new SqlCommand(querry, connection)) {
                    command.Parameters.AddWithValue("@Nom", student.LastName);
                    command.Parameters.AddWithValue("@Prenom", student.FirstName);
                    command.Parameters.AddWithValue("@Matricule", student.Matricule);
                    command.Parameters.AddWithValue("@Email", student.Email);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0) {
                        _logger.LogInformation("Student added successfully.");
                    } else {
                        _logger.LogWarning("No rows were inserted.");
                    }
                }


            }
        }

        public void Delete(int id) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                string querry = "delete from Etudiant where ETU_ID = @Id";
                using (SqlCommand command = new SqlCommand(querry, connection)) {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0) {
                        _logger.LogInformation("Student deleted successfully.");
                    } else {
                        _logger.LogWarning("No rows were deleted.");
                    }
                }

            }

        }
    }
}
