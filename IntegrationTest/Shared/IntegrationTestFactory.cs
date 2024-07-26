using System.Data.Common;
using Application.Repositories;
using Infrastructure.Persistence.Repositories;
using IntegrationTests.Shared.Mocks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;

namespace IntegrationTests.Shared;

public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer;
    private DbConnection _dbConnection = default!;
    private Respawner _respawner = default!;
    public HttpClient RequisitionAdminClient { get; private set; } = default!;
    public HttpClient RequisitionProgramAdminClient { get; private set; } = default!;
    public HttpClient AnonymousClient { get; private set; } = default!;

    public IntegrationTestFactory()
    {
        _postgreSqlContainer = new PostgreSqlBuilder()
            .WithImage("postgres:16.0")
            .WithDatabase("requisition-web")
            .WithCleanUp(true)
            .WithAutoRemove(true)
            .WithName("requisition-web-integration-tests")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services => services
                .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme));
        builder.ConfigureTestServices(services => services.Configure<JwtBearerOptions>(
                    JwtBearerDefaults.AuthenticationScheme,
                        options =>
                        {
                            options.Configuration = new OpenIdConnectConfiguration
                            {
                                Issuer = MockJwtTokenProvider.Issuer,
                            };
                            options.TokenValidationParameters.ValidIssuer = MockJwtTokenProvider.Issuer;
                            options.TokenValidationParameters.ValidAudience = MockJwtTokenProvider.Issuer;
                            options.Configuration.SigningKeys.Add(MockJwtTokenProvider.SecurityKey);
                        }
                ));
        builder.UseEnvironment("Test");
        Configuration = GetTestConfig();
        builder.UseConfiguration(Configuration);
    }

    private IConfiguration GetTestConfig()
    {
        var dbConfig = new Dictionary<string, string?>
        {
            {"DB_HOST", _postgreSqlContainer.Hostname},
            {"DB_NAME", "requisition-web"},
            {"DB_USERNAME", PostgreSqlBuilder.DefaultUsername},
            {"DB_PASSWORD", PostgreSqlBuilder.DefaultPassword},
            {"DB_PORT", _postgreSqlContainer.GetMappedPublicPort(PostgreSqlBuilder.PostgreSqlPort).ToString()}
        };
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.integration.test.json", true, true)
            .AddInMemoryCollection(dbConfig)
            .AddEnvironmentVariables()
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
        InitiateClients();
        await InitializeRespawner();
        await ResetDatabase();
    }


    private async Task InitializeRespawner()
    {
        _dbConnection = new NpgsqlConnection(_postgreSqlContainer.GetConnectionString());
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_dbConnection,
            new() { DbAdapter = DbAdapter.Postgres, SchemasToInclude = new[] { "public" } });
    }

    public async Task ResetDatabase()
    {
        await _respawner.ResetAsync(_dbConnection);
        await LoadSeedData();
    }

    public new async Task DisposeAsync()
    {
        await _postgreSqlContainer.DisposeAsync();
    }

    private void InitiateClients()
    {
        RequisitionAdminClient = CreateClient();
        RequisitionProgramAdminClient = CreateClient();
        AnonymousClient = CreateClient();
    }

    private async Task LoadSeedData()
    {
        var serviceProvider = Server.Services.CreateScope().ServiceProvider;
        _ = serviceProvider.GetRequiredService<IUnitOfWork>();
        var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        await applicationDbContext.SaveChangesAsync();
    }

    public IConfiguration Configuration { get; private set; } = default!;
}