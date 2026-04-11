using FiapOrangeRoute.Models;
using System.Net;
using System.Net.Http.Json;

namespace FiapOrangeRoute.Tests.Integration;

public class UsuariosIntegrationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public UsuariosIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetUsuarios_Retorna200()
    {
        var response = await _client.GetAsync("/api/usuarios");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostUsuario_Retorna201()
    {
        var usuario = new Usuario
        {
            Nome = "Teste",
            Email = "teste@email.com",
            Senha = "123",
            TipoUsuarioId = 1
        };

        var response = await _client.PostAsJsonAsync("/api/usuarios", usuario);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task GetUsuarioInexistente_Retorna404()
    {
        var response = await _client.GetAsync("/api/usuarios/999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteUsuarioInexistente_Retorna404()
    {
        var response = await _client.DeleteAsync("/api/usuarios/999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}