using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace SoundManager.Tests.Integration;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.UseConfiguration(BuildTestConfiguration());
    }

    private static IConfiguration BuildTestConfiguration()
    {
        var config = new Dictionary<string, string?>
        {
            { "ConnectionStrings:Sqlite", "Data Source=Test.db" },
            { "Sounds:Directory", "TestSounds" }
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(config)
            .Build();
        return configuration;
    }
}