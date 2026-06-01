using Microsoft.Extensions.Logging.Abstractions;
using Tests.Shared;

namespace Tests.RepositoriesTests {

    [Collection("IntegrationDB")]
    public class StudentRepoTests {

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

        [Theory]
        [InlineData(6)]
        [InlineData(12)]
        public async Task DeleteTest(int id) {
            await dBSetup.InitStudentsDataAsync();
            var logger = NullLogger<RepoDLL.Repositories.StudentRepo>.Instance;
            var repo = new RepoDLL.Repositories.StudentRepo(logger, _connectionString);

            repo.Delete(id);

            var students = repo.GetAll();
            var student = students.FirstOrDefault(s => s.Id == id);

            Assert.Null(student);
        }

        [Fact]
        public async Task DeleteThrowsExceptionTest() {
            await dBSetup.InitKotDataAsync();
            var logger = NullLogger<RepoDLL.Repositories.StudentRepo>.Instance;
            var repo = new RepoDLL.Repositories.StudentRepo(logger, _connectionString);

            Assert.ThrowsAny<Exception>(() => repo.Delete(6)); //link to a kot so can t be deleted, should throw exception
        }

        [Theory]
        [InlineData("HE20", "Jr", "vini", "Email@gmail.com")]
        public async Task AddTests(string matricule, string firstName, string lastName, string email) {
            await dBSetup.InitStudentsDataAsync();
            var logger = NullLogger<RepoDLL.Repositories.StudentRepo>.Instance;
            var repo = new RepoDLL.Repositories.StudentRepo(logger, _connectionString);

            var student = new ModelsDLL.Models.Student {
                Matricule = matricule,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
            repo.Add(student);

            //verify that the student was added
            var students = repo.GetAll();
            Assert.Contains(students, s => s.Matricule == matricule && s.FirstName == firstName && s.LastName == lastName);
        }
    }
}
