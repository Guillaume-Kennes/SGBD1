using Microsoft.Extensions.Logging.Abstractions;
using Tests.Shared;

namespace Tests.RepositoriesTests {
    public class KotRepoTests : IClassFixture<DatabaseFixture> {

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

    }
}
