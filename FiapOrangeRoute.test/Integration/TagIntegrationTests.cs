using FiapOrangeRoute.DTOs.Tag;
using System.Net;
using System.Net.Http.Json;

namespace FiapOrangeRoute.Tests.Integration;

public class TagsIntegrationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TagsIntegrationTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetTags_Retorna200()
    {
        var response =
            await _client.GetAsync("/api/tags");

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);
    }

    [Fact]
    public async Task PostTag_Retorna201()
    {
        var tag = new TagCreateDTO
        {
            Nome = "CSharp"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/tags",
                tag);

        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode);
    }
}