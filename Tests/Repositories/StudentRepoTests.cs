using Microsoft.Extensions.Logging.Abstractions;
using Tests.Shared;

namespace Tests.RepositoriesTests {
    public class StudentRepoTests : IClassFixture<DatabaseFixture> {

        private readonly DatabaseFixture _fixture;
        private string _connectionString;
        DBSetup dBSetup => _fixture.DbSetup;

        public StudentRepoTests(DatabaseFixture databaseFixture) {
            _fixture = databaseFixture;
            _connectionString = _fixture.ConnectionString;
        }

        [Fact]
        public async Task GetAllTest() {
            await dBSetup.InitStudentsDataAsync();

            var logger = NullLogger<RepoDLL.Repositories.StudentRepo>.Instance;
            var repo = new RepoDLL.Repositories.StudentRepo(logger, _connectionString);

            var students = repo.GetAll();

            Assert.NotNull(students);
            Assert.NotEmpty(students);
            Assert.Equal(3, students.Count);

        }

        [Theory]
        [InlineData("%d%", 2)]
        [InlineData("z%", 0)]
        public async Task FindStudentByLastnameTest(string lastName, int result) {

            await dBSetup.InitStudentsDataAsync();

            var logger = NullLogger<RepoDLL.Repositories.StudentRepo>.Instance;
            var repo = new RepoDLL.Repositories.StudentRepo(logger, _connectionString);

            var students = repo.FindStudentByLastname(lastName);

            Assert.Equal(result, students.Count);

        }


    }
}
