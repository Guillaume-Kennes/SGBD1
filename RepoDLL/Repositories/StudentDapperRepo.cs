using Dapper;
using InterfacesDLL.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ModelsDLL.Models;
using System.Data;

namespace RepoDLL.Repositories {
    public class StudentDapperRepo : BaseRepo, IStudentRepo {


        private readonly string _connectionString = @"Server=MSI\MSSQL; DataBase=SGBD; User ID=sa; Password=GuillaumeK15_; TrustServerCertificate=True;";
        private readonly ILogger<StudentDapperRepo> _logger;

        public StudentDapperRepo(ILogger<StudentDapperRepo> logger) {
            _logger = logger;
        }

        public StudentDapperRepo(ILogger<StudentDapperRepo> logger, string connectionString) {
            _logger = logger;
            _connectionString = connectionString;
        }


        public List<Student> FindStudentByLastname(string lastName) {
            string querry = GetFileFromAssembly("Etudiant_findByLastnameDapper.sql"); // ensure this SQL aliases columns to Student properties (Id, LastName, FirstName, Matricule, Email)

            try {
                using (IDbConnection connection = new SqlConnection(_connectionString)) {
                    List<Student> students = connection.Query<Student>(querry, new { lastName }).ToList();
                    _logger.LogInformation("Fetched {Count} students by last name from database.", students.Count);
                    return students;
                }
            } catch (Exception ex) {
                _logger.LogError(ex, "Error executing FindStudentByLastname with Dapper.");
                throw;
            }
        }

        public List<Student> GetAll() {
            List<Student> students = new List<Student>();
            string querry = GetFileFromAssembly("Etudiant_selectAllDapper.sql");

            try {
                using (IDbConnection connection = new SqlConnection(_connectionString)) {
                    students = connection.Query<Student>(querry).ToList();
                    return students;
                }
            } catch (Exception ex) {
                _logger.LogError(ex, "Error executing GetAll with Dapper.");
                throw;
            }
        }


        public void Add(Student student) {
            string querry = GetFileFromAssembly("Etudiant_insertDapper.sql");
            try {
                using (IDbConnection connection = new SqlConnection(_connectionString)) {
                    int rowsAffected = connection.Execute(querry, student);
                    if (rowsAffected > 0) {
                        _logger.LogInformation("Student added successfully.");
                    } else {
                        _logger.LogWarning("No rows were inserted.");
                    }
                }
            } catch (Exception ex) {
                _logger.LogError(ex, "Error executing Add with Dapper.");
                throw;
            }
        }

        public void Delete(int id) {
            string querry = GetFileFromAssembly("Etudiant_deleteDapper.sql");
            try {
                using (IDbConnection connection = new SqlConnection(_connectionString)) {
                    connection.Execute(querry, new { Id = id });
                }
            } catch (Exception ex) {
                _logger.LogError(ex, "Error executing Delete with Dapper.");
                throw;
            }
        }


        public void Update(Student student) {
            string querry = GetFileFromAssembly("Etudiant_updateDapper.sql");
            try {
                using (IDbConnection connection = new SqlConnection(_connectionString)) {
                    connection.Execute(querry, student);
                }
            } catch (Exception ex) {
                _logger.LogError(ex, "Error executing Update with Dapper.");
                throw;
            }
        }

    }
}
