using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using SoundManager.Core.Models;
using SoundManager.Tests.Integration.Utils;

namespace SoundManager.Tests.Integration.Endpoints;

public class GroupEndpointsTests : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly DatabaseTestHelpers _database;

    public GroupEndpointsTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _database = new DatabaseTestHelpers(_factory.Services);
    }
    
    [Fact]
    public async Task GetGroups_ReturnsEmptyList_WhenNoGroupsExist()
    {
        var client = _factory.CreateClient();
        var groups = await client.GetFromJsonAsync<List<Group>>("/api/v1/groups");
        groups.Should().BeEmpty();
    }
    
    [Fact]
    public async Task GetGroups_ReturnsListOfGroups_WhenGroupsExist()
    {
        var client = _factory.CreateClient();
        var insertedGroups = _database.CreateGroups(10);
        var groups = await client.GetFromJsonAsync<List<Group>>("/api/v1/groups");
        groups.Should().HaveCount(10);
        groups.Should().BeEquivalentTo(insertedGroups);
    }
    
    [Fact]
    public async Task GetGroup_ReturnsGroup_WhenGroupExists()
    {
        var client = _factory.CreateClient();
        var insertedGroup = _database.CreateGroups(1).First();
        var group = await client.GetFromJsonAsync<Group>($"/api/v1/groups/{insertedGroup.Id}");
        group.Should().BeEquivalentTo(insertedGroup);
    }
    
    [Fact]
    public async Task GetGroup_ReturnsNotFound_WhenGroupDoesNotExist()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync($"/api/v1/groups/{Guid.NewGuid()}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeleteGroup_ReturnsNoContent_WhenGroupExists()
    {
        var client = _factory.CreateClient();
        var insertedGroup = _database.CreateGroups(1).First();
        var response = await client.DeleteAsync($"/api/v1/groups/{insertedGroup.Id}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    public void Dispose()
    {
        _database.Dispose();
    }
}