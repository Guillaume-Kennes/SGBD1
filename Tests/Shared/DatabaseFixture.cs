using Tests.Shared;
using Testcontainers.MsSql;

namespace Tests.Shared {

    //[Collection("IntegrationDB")]
    public class DatabaseFixture : IAsyncLifetime {

        private MsSqlContainer _container;
        public DBSetup DbSetup { get; private set; } = null!;
        public string ConnectionString { get; private set; } = null!;

        public async Task InitializeAsync() {
            _container = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword("yourStrong(!)Password")
                .Build();

            await _container.StartAsync();

            var masterCS = _container.GetConnectionString();
            DbSetup = new DBSetup(masterCS);

            await DbSetup.CreateDBAsync();
            await DbSetup.CreateTablesAsync();
            await DbSetup.InitStudentsDataAsync();

            ConnectionString = masterCS.Replace("master", "SGBD");
        }

        public async Task DisposeAsync() {
            if (_container is not null) {
                await _container.StopAsync();
                await _container.DisposeAsync();
                _container = null!;
            }
        }

    }

    [CollectionDefinition("IntegrationDB", DisableParallelization = true)]
    public class IntegrationDBCollection : ICollectionFixture<DatabaseFixture> {
        //No code needed here, this class is just to define the collection and its fixture
        // links "IntegrationDB" -> DatabaseFixture (no code)
    }
}
