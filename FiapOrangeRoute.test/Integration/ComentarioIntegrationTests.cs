using FiapOrangeRoute.DTOs.Comentario;
using System.Net;
using System.Net.Http.Json;

namespace FiapOrangeRoute.Tests.Integration;

public class ComentariosIntegrationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ComentariosIntegrationTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetComentarios_Retorna200()
    {
        var response =
            await _client.GetAsync("/api/comentarios");

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);
    }
}