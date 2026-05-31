using Microsoft.Extensions.Logging.Abstractions;
using Shared;
using Testcontainers.MsSql;

namespace Tests.RepositoriesTests {
    public class StudentDapperRepoTests {

        [Fact]
        public async Task GetAllTest() {

            var container = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword("yourStrong(!)Password")
                .Build();

            await container.StartAsync();

            DBSetup dBSetup = new DBSetup(container.GetConnectionString());

            await dBSetup.CreateDBAsync();
            await dBSetup.CreateTablesAsync();
            await dBSetup.InitStudentsDataAsync();

            try {
                string connectionString = container.GetConnectionString();
                connectionString = connectionString.Replace("master", "SGBD");

                var logger = NullLogger<Repositories.StudentDapperRepo>.Instance;
                var repo = new Repositories.StudentDapperRepo(logger, connectionString);

                var students = repo.GetAll();

                Assert.NotNull(students);
                Assert.NotEmpty(students);
                Assert.Equal(3, students.Count);

            } finally {
                await container.DisposeAsync();
            }

        }
        //1:26

        [Theory]
        [InlineData("%d%", 2)]
        [InlineData("z%", 0)]
        public async Task FindStudentByLastnameTest(string lastName, int result) {

            var container = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword("yourStrong(!)Password")
                .Build();

            await container.StartAsync();

            DBSetup dBSetup = new DBSetup(container.GetConnectionString());

            await dBSetup.CreateDBAsync();
            await dBSetup.CreateTablesAsync();
            await dBSetup.InitStudentsDataAsync();

            try {
                string connectionString = container.GetConnectionString();
                connectionString = connectionString.Replace("master", "SGBD");

                var logger = NullLogger<Repositories.StudentDapperRepo>.Instance;
                var repo = new Repositories.StudentDapperRepo(logger, connectionString);

                var students = repo.FindStudentByLastname(lastName);

                Assert.Equal(result, students.Count);

            } finally {
                await container.DisposeAsync();
            }

        }

    }
}
