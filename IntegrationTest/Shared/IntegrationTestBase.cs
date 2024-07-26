using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using IntegrationTests.Shared.Mocks;

namespace IntegrationTests.Shared;

[Collection("IntegrationTests")]
public abstract class IntegrationTestBase : VerifyBase, IAsyncLifetime
{
    private readonly IntegrationTestFactory _applicationFactory;
    private readonly Func<Task> _resetDatabase;

    public HttpClient RequisitionAdminClient { get; private set; } = default!;
    public HttpClient IpAdminClient { get; private set; } = default!;
    public HttpClient RequisitionProgramAdminClient { get; private set; } = default!;
    public HttpClient AnonymousClient { get; private set; } = default!;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public IntegrationTestBase(IntegrationTestFactory applicationFactory) : base()
    {
        _applicationFactory = applicationFactory;
        InitiateClients();
        AddAuthenticationHeaderToClients();
        _resetDatabase = applicationFactory.ResetDatabase;
        _jsonSerializerOptions = new()
        {
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    private void InitiateClients()
    {
        RequisitionAdminClient = _applicationFactory.RequisitionAdminClient;
        RequisitionProgramAdminClient = _applicationFactory.RequisitionProgramAdminClient;
        AnonymousClient = _applicationFactory.AnonymousClient;
    }
    private void AddAuthenticationHeaderToClients()
    {
        var adminToken = new MockJwtToken().AddClaims(Guid.NewGuid(), "requisition-admin").Generate();
        RequisitionAdminClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
        var programAdminToken = new MockJwtToken().AddClaims(Guid.NewGuid(), "requisition-program-admin").Generate();
        RequisitionProgramAdminClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", programAdminToken);


    }
    public Task InitializeAsync()
    {
        return _resetDatabase();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    protected VerifySettings GetVerifySettings()
    {
        var classname = GetType().Name;
        var settings = new VerifySettings();
        settings.UseDirectory($"Results/{classname}");
        return settings;
    }

}