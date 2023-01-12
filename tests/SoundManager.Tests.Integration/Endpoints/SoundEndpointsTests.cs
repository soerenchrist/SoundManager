using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Bogus;
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
        var sounds = await client.GetFromJsonAsync<List<SoundEffect>>("/api/v1/sounds", _options);

        sounds.Should().BeEmpty();
    }

    [Fact]
    public async Task GetSounds_ShouldReturnSoundEffects_WhenSomeExist()
    {
        var client = _factory.CreateClient();
        _database.CreateSoundEffects(10);

        var sounds = await client.GetFromJsonAsync<List<SoundEffectDto>>("/api/v1/sounds", _options);

        sounds.Should().HaveCount(10);
    }

    [Fact]
    public async Task GetSoundById_ShouldReturn404_IfSoundDoesNotExist()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/api/v1/sounds/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetSoundById_ShouldReturnSound_IfSoundExists()
    {
        var client = _factory.CreateClient();
        var sound = _database.CreateSoundEffects(1).First();

        var response = await client.GetAsync($"/api/v1/sounds/{sound.Id}");
        var soundDto = await response.Content.ReadFromJsonAsync<SoundEffectDto>(_options);

        soundDto.Should().NotBeNull();
        soundDto!.Id.Should().Be(sound.Id);
    }

    [Fact]
    public async Task DeleteSound_ShouldReturn404_IfSoundDoesNotExist()
    {
        var client = _factory.CreateClient();
        var response = await client.DeleteAsync($"/api/v1/sounds/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteSound_ShouldReturn204_IfSoundExists()
    {
        var client = _factory.CreateClient();
        var sound = _database.CreateSoundEffects(1).First();

        var response = await client.DeleteAsync($"/api/v1/sounds/{sound.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteSound_ShouldDeleteFileFromSystem_AndRemoveSoundFromDatabase()
    {
        var client = _factory.CreateClient();

        var createResp = await client.PostAsync("/api/v1/sounds", CreateFileContent());
        var createdBody = await createResp.Content.ReadAsStringAsync();
        var createdData = JsonSerializer.Deserialize<SoundEffectDto>(createdBody, _options);

        FileTestHelpers.SoundFileExists($"{createdData!.Id}.mp3").Should().BeTrue();
        await client.DeleteAsync($"/api/v1/sounds/{createdData.Id}");

        FileTestHelpers.SoundFileExists($"{createdData.Id}.mp3").Should().BeFalse();
        _database.SoundEffectExists(createdData).Should().BeFalse();
    }

    [Fact]
    public async Task CreateSound_ShouldCreateFileAndDbEntry_AndReturnsSoundEffectInfo()
    {
        var client = _factory.CreateClient();

        var response = await client.PostAsync("/api/v1/sounds", CreateFileContent());
        var body = await response.Content.ReadAsStringAsync();

        var soundEffect = JsonSerializer.Deserialize<SoundEffectDto>(body, _options);
        response.EnsureSuccessStatusCode();

        soundEffect!.Name.Should().Be("Test");
        soundEffect.TotalMilliseconds.Should().Be(12042);

        FileTestHelpers.SoundFileExists($"{soundEffect.Id}.mp3").Should().BeTrue();
        _database.SoundEffectExists(soundEffect);
    }

    private static HttpContent CreateFileContent()
    {
        var content = new MultipartFormDataContent();
        var data = FileTestHelpers.ReadTestFile("sound.mp3");
        var file = new ByteArrayContent(data);
        content.Add(file, "File", "test.mp3");
        content.Add(new StringContent("Test"), "Name");
        return content;
    }

    public void Dispose()
    {
        _database.Dispose();
    }
}