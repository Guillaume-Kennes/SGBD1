using Dapper;
using InterfacesDLL.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ModelsDLL.DTO;
using System.Data;

namespace RepoDLL.Repositories {
    public class KotRepo : BaseRepo, IKotRepo {


        private readonly string _connectionString = @"Server=MSI\MSSQL; DataBase=SGBD; User ID=sa; Password=GuillaumeK15_; TrustServerCertificate=True;";
        private readonly ILogger<KotRepo> _logger;

        public KotRepo(ILogger<KotRepo> logger) {
            _logger = logger;
        }

        public KotRepo(ILogger<KotRepo> logger, string connectionString) {
            _logger = logger;
            _connectionString = connectionString;
        }

        public List<KotStudentDTO> GetAll() {
            List<KotStudentDTO> kots = new List<KotStudentDTO>();
            string querry = GetFileFromAssembly("Kot_selectAll.sql");

            using (IDbConnection connection = new SqlConnection(_connectionString)) {
                kots = connection.Query<KotStudentDTO>(querry).ToList();
                return kots;
            }
        }

        public void Delete(int id) {
            string querry = GetFileFromAssembly("Kot_delete.sql");

            using (IDbConnection connection = new SqlConnection(_connectionString)) {
                connection.Execute(querry, new { Id = id });
            }
        }

    }
}
