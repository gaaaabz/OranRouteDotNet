using FiapOrangeRoute.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

public class TrilhasCarreiraControllerTests
{
    private AppDbContext GetContext()
    {
        var opt = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(opt);
    }

    [Fact]
    public async Task Create_SalvaTrilha()
    {
        var ctx = GetContext();
        var logger = new Mock<ILogger<TrilhasCarreiraController>>();
        var ctrl = new TrilhasCarreiraController(ctx, logger.Object);

        var trilha = new TrilhaCarreira { Titulo = "Backend" };

        await ctrl.Create(trilha);

        Assert.Equal(1, ctx.TrilhasCarreira.Count());
    }
}