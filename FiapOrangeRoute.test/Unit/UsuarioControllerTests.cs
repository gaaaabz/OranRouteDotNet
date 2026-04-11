using FiapOrangeRoute.Controllers;
using FiapOrangeRoute.Data;
using FiapOrangeRoute.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapOrangeRoute.Tests.Unit
{
    public class UsuariosControllerTests
    {
        private AppDbContext GetContext()
        {
            var opt = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(opt);
        }

        [Fact]
        public async Task GetAll_SemUsuarios_RetornaListaVazia()
        {
            var ctx = GetContext();
            var logger = new Mock<ILogger<UsuariosController>>();
            var ctrl = new UsuariosController(ctx, logger.Object);

            var result = await ctrl.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsType<List<Usuario>>(ok.Value);
            Assert.Empty(list);
        }

        [Fact]
        public async Task GetById_IdNaoExiste_Retorna404()
        {
            var ctx = GetContext();
            var logger = new Mock<ILogger<UsuariosController>>();
            var ctrl = new UsuariosController(ctx, logger.Object);

            var result = await ctrl.GetById(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_SalvaUsuario()
        {
            var ctx = GetContext();
            ctx.TiposUsuario.Add(new TipoUsuario { Nome = "Admin" });
            await ctx.SaveChangesAsync();

            var logger = new Mock<ILogger<UsuariosController>>();
            var ctrl = new UsuariosController(ctx, logger.Object);

            var usuario = new Usuario
            {
                Nome = "Gabriel",
                Email = "teste@email.com",
                Senha = "123",
                TipoUsuarioId = 1
            };

            await ctrl.Create(usuario);

            Assert.Equal(1, ctx.Usuarios.Count());
        }

        [Fact]
        public async Task Delete_IdExiste_Retorna204()
        {
            var ctx = GetContext();
            ctx.Usuarios.Add(new Usuario { Nome = "Teste", Email = "a@a.com", Senha = "123", TipoUsuarioId = 1 });
            await ctx.SaveChangesAsync();

            var logger = new Mock<ILogger<UsuariosController>>();
            var ctrl = new UsuariosController(ctx, logger.Object);

            var result = await ctrl.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
