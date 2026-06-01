using Microsoft.Extensions.Logging.Abstractions;
using Tests.Shared;

namespace Tests.RepositoriesTests {

    [Collection("IntegrationDB")]
    public class KotRepoTests {

        private readonly DatabaseFixture _fixture;
        private string _connectionString;
        DBSetup dBSetup => _fixture.DbSetup;

        public KotRepoTests(DatabaseFixture databaseFixture) {
            _fixture = databaseFixture;
            _connectionString = _fixture.ConnectionString;
        }

        [Fact]
        public async Task GetAllTest() {
            await dBSetup.InitKotDataAsync();

            var logger = NullLogger<RepoDLL.Repositories.KotRepo>.Instance;
            var repo = new RepoDLL.Repositories.KotRepo(logger, _connectionString);

            var kots = repo.GetAll();

            Assert.NotNull(kots);
            Assert.NotEmpty(kots);
            Assert.Equal(2, kots.Count);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)] //this one doesn t exist, should be null
        public async Task DeleteTest(int id) {
            await dBSetup.InitKotDataAsync();
            var logger = NullLogger<RepoDLL.Repositories.KotRepo>.Instance;
            var repo = new RepoDLL.Repositories.KotRepo(logger, _connectionString);

            repo.Delete(id);

            var kots = repo.GetAll();
            var kot = kots.FirstOrDefault(k => k.KOT_ID == id);

            Assert.Null(kot);
        }

    }
}
