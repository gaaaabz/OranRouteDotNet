using FiapOrangeRoute.Models;
using System.Net;
using System.Net.Http.Json;

namespace FiapOrangeRoute.Tests.Integration;

public class TrilhasCarreiraIntegrationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TrilhasCarreiraIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetTrilhas_Retorna200()
    {
        var response = await _client.GetAsync("/api/trilhascarreira");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostTrilha_Retorna201()
    {
        var trilha = new TrilhaCarreira
        {
            Titulo = "Backend",
            Conteudo = "Aprender APIs"
        };

        var response = await _client.PostAsJsonAsync("/api/trilhascarreira", trilha);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}