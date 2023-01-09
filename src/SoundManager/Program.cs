using FastEndpoints;
using SoundManager.Core;
using SoundManager.Infrastructure;
using SoundManager.Infrastructure.Database;
using SoundManager.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructure();
builder.Services.AddUseCases(builder.Configuration);
builder.Services.AddCoreDependencies();
builder.Services.AddFastEndpoints();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseFastEndpoints(options =>
{
    options.Endpoints.RoutePrefix = "api/v1";
});

using var scope = app.Services.CreateScope();
using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
context.Database.EnsureCreated();

app.Run();