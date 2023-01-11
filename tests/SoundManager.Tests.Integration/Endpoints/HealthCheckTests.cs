
namespace SoundManager.Tests.Integration.Endpoints;

public class HealthCheckTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public HealthCheckTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task WhenHealthCheckIsCalled_ItShouldReturn200()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("health-check");

        response.EnsureSuccessStatusCode();
    }
}