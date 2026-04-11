using FiapOrangeRoute.Controllers;
using FiapOrangeRoute.Data;
using FiapOrangeRoute.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

public class TiposUsuarioControllerTests
{
    private AppDbContext GetContext()
    {
        var opt = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(opt);
    }

    [Fact]
    public async Task GetAll_RetornaVazio()
    {
        var ctx = GetContext();
        var logger = new Mock<ILogger<TiposUsuarioController>>();
        var ctrl = new TiposUsuarioController(ctx, logger.Object);

        var result = await ctrl.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result);
        var list = Assert.IsType<List<TipoUsuario>>(ok.Value);

        Assert.Empty(list);
    }
}