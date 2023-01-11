using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using SoundManager.Core.Models;
using SoundManager.Dtos;
using SoundManager.Tests.Integration.Utils;

namespace SoundManager.Tests.Integration.Endpoints;

public class SoundEndpointsTests : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly DatabaseTestHelpers _database;

    public SoundEndpointsTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _database = new DatabaseTestHelpers(_factory.Services);
    }
    
    [Fact]
    public async Task GetSounds_ShouldReturnEmptyList_WhenNoSoundsExist()
    {
        var client = _factory.CreateClient();
        var sounds = await client.GetFromJsonAsync<List<SoundEffect>>("/api/v1/sounds");

        sounds.Should().BeEmpty();
    }

    [Fact]
    public async Task CreateSound_ShouldCreateFileAndDbEntry_AndReturnsSoundEffectInfo()
    {
        var client = _factory.CreateClient();
        using var content = new MultipartFormDataContent();
        var data = FileTestHelpers.ReadTestFile("sound.mp3");
        using var file = new ByteArrayContent(data);
        content.Add(file, "File", "test.mp3");
        content.Add(new StringContent("Test"), "Name");

        var response = await client.PostAsync("/api/v1/sounds", content);
        var body = await response.Content.ReadAsStringAsync();

        var soundEffect = JsonSerializer.Deserialize<SoundEffectDto>(body, _options);
        response.EnsureSuccessStatusCode();
        soundEffect!.Name.Should().Be("Test");
        soundEffect.TotalMilliseconds.Should().Be(12042);
        
        FileTestHelpers.SoundFileExists($"{soundEffect.Id}.mp3").Should().BeTrue();
        _database.SoundEffectExists(soundEffect);
    }
    
    public void Dispose()
    {
        _database.Dispose();
    }
}