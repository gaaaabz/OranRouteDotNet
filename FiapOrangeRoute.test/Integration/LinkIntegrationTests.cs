using FiapOrangeRoute.DTOs.Link;
using System.Net;
using System.Net.Http.Json;

namespace FiapOrangeRoute.Tests.Integration;

public class LinksIntegrationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public LinksIntegrationTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetLinks_Retorna200()
    {
        var response =
            await _client.GetAsync("/api/links");

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);
    }

    [Fact]
    public async Task PostLink_Retorna201()
    {
        var trilha = new
        {
            titulo = "Backend",
            conteudo = "API REST"
        };

        await _client.PostAsJsonAsync(
            "/api/trilhascarreira",
            trilha);

        var link = new LinkCreateDTO
        {
            Titulo = "Google",
            Conteudo = "https://google.com",
            IdTrilhaCarreira = 1
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/links",
                link);

        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode);
    }
}