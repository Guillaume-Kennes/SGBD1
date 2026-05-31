using Dapper;
using Microsoft.Data.SqlClient;
using RepoDLL.Repositories;
using System.Data;

namespace Tests.Shared {
    public class DBSetup : BaseRepo {

        private readonly string _connectionString;
        public DBSetup(string connectionString) : base() {
            _connectionString = connectionString;
        }

        public async Task CreateDBAsync() {
            await RunScript("CreateDB.sql");
        }

        public async Task CreateTablesAsync() {
            await RunScript("CreateTables.sql");
        }

        public async Task InitStudentsDataAsync() {
            await RunScript("InitStudentsData.sql");
        }

        private async Task RunScript(string filename) {
            string sql = await GetFileFromAssemblyAsync(filename);
            using (IDbConnection connection = new SqlConnection(_connectionString)) {
                await connection.ExecuteAsync(sql);
            }
        }

    }
}
