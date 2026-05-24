using FiapOrangeRoute.DTOs.TipoUsuario;
using System.Net;
using System.Net.Http.Json;

namespace FiapOrangeRoute.Tests.Integration;

public class TiposUsuarioIntegrationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TiposUsuarioIntegrationTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetTiposUsuario_Retorna200()
    {
        var response =
            await _client.GetAsync("/api/tiposusuario");

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);
    }

    [Fact]
    public async Task PostTipoUsuario_Retorna201()
    {
        var tipo = new TipoUsuarioCreateDTO
        {
            Nome = "Admin"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/tiposusuario",
                tipo);

        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode);
    }
}