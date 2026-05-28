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
using System.Reflection;

namespace SGBD.Repositories {
    public class StudentRepo : BaseRepo, IStudentRepo {


        private readonly string connectionString = @"Server=MSI\MSSQL; DataBase=SGBD; User ID=sa; Password=GuillaumeK15_; TrustServerCertificate=True;";
        private readonly ILogger<StudentRepo> _logger;

        public StudentRepo(ILogger<StudentRepo> logger) {
            _logger = logger;
        }


        public List<Student> FindStudentByLastname(string lastName) {
            List<Student> list = new List<Student>();
            string querry = GetFileFromAssembly("Etudiant_findByLastname.sql");
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand(querry, connection)) {
                    command.Parameters.AddWithValue("@lastName", lastName);
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            try {
                                var student = new Student {
                                    Id = reader.GetInt32(reader.GetOrdinal("Etu_Id")),
                                    LastName = reader.GetString(reader.GetOrdinal("Etu_Nom")),
                                    FirstName = reader.IsDBNull(reader.GetOrdinal("Etu_Prenom")) ? null : reader.GetString(reader.GetOrdinal("Etu_Prenom")),
                                    Matricule = reader.GetString(reader.GetOrdinal("Etu_Matricule")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Etu_Email")) ? null : reader.GetString(reader.GetOrdinal("Etu_Email"))
                                };
                                list.Add(student);
                            } catch (Exception ex) {
                                _logger.LogError(ex, "Error reading student record from data reader.");
                            }
                        }
                    }
                }
            }
            return list;
        }

        public List<Student> GetAll() {

            List<Student> list = new List<Student>();

            string querry = GetFileFromAssembly("Etudiant_selectAll.sql");

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand(querry, connection)) {
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            try {
                                var student = new Student {
                                    Id = reader.GetInt32(reader.GetOrdinal("Etu_Id")),
                                    LastName = reader.GetString(reader.GetOrdinal("Etu_Nom")),
                                    FirstName = reader.IsDBNull(reader.GetOrdinal("Etu_Prenom")) ? null : reader.GetString(reader.GetOrdinal("Etu_Prenom")),
                                    Matricule = reader.GetString(reader.GetOrdinal("Etu_Matricule")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Etu_Email")) ? null : reader.GetString(reader.GetOrdinal("Etu_Email"))
                                };

                                list.Add(student);
                            } catch (Exception ex) {
                                _logger.LogError(ex, "Error reading student record from data reader.");
                            }
                        }
                    }
                }
            }

            _logger.LogInformation("Fetched {Count} students from database.", list.Count);
            return list;
        }


        public void Add(Student student) {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                string querry = GetFileFromAssembly("Etudiant_insert.sql");

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
            string querry = GetFileFromAssembly("Etudiant_delete.sql");
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
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


        public void Update(Student student) {
            string querry = GetFileFromAssembly("Etudiant_update.sql");
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand(querry, connection)) {
                    command.Parameters.AddWithValue("@Id", student.Id);
                    command.Parameters.AddWithValue("@Nom", student.LastName);
                    command.Parameters.AddWithValue("@Prenom", student.FirstName);
                    command.Parameters.AddWithValue("@Matricule", student.Matricule);
                    command.Parameters.AddWithValue("@Email", student.Email);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0) {
                        _logger.LogInformation("Student updated successfully.");
                    } else {
                        _logger.LogWarning("No rows were updated.");
                    }
                }

            }
        }

    }
}
