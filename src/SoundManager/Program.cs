using SoundManager.Core;
using SoundManager.Infrastructure;
using SoundManager.Infrastructure.Database;
using SoundManager.UseCases;

namespace SoundManager;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("Sqlite");
        if (connectionString == null) throw new Exception("Connection string not found");

        builder.Services.AddControllers();
        builder.Services.AddInfrastructure(connectionString);
        builder.Services.AddUseCases(builder.Configuration);
        builder.Services.AddCoreDependencies();


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
        }

        app.UseHttpsRedirection();
        app.MapGet("/health-check", () => Results.Ok());

        app.MapControllers();

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();

        app.Run();
    }
}