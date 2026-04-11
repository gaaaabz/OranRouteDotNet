using System.Net;

namespace FiapOrangeRoute.Tests.Integration;

public class FavoritosIntegrationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public FavoritosIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetFavoritos_Retorna200()
    {
        var response = await _client.GetAsync("/api/favoritos");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}