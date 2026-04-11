using FiapOrangeRoute.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _ctx;
    private readonly ILogger<UsuariosController> _log;

    public UsuariosController(AppDbContext ctx, ILogger<UsuariosController> log)
    {
        _ctx = ctx;
        _log = log;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _log.LogInformation("Listando usuarios");

        var usuarios = await _ctx.Usuarios
            .Include(u => u.TipoUsuario)
            .ToListAsync();

        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var usuario = await _ctx.Usuarios
            .Include(u => u.TipoUsuario)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null)
        {
            _log.LogWarning("Usuario {Id} nao encontrado", id);
            return NotFound();
        }

        return Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Usuario usuario)
    {
        var tipo = await _ctx.TiposUsuario.FindAsync(usuario.TipoUsuarioId);

        if (tipo == null)
        {
            _log.LogWarning("TipoUsuarioId inexistente: {Id}", usuario.TipoUsuarioId);
            return BadRequest($"TipoUsuarioId {usuario.TipoUsuarioId} nao existe.");
        }

        _ctx.Usuarios.Add(usuario);
        await _ctx.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Usuario usuario)
    {
        if (id != usuario.Id) return BadRequest();

        var existing = await _ctx.Usuarios.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Nome = usuario.Nome;
        existing.Email = usuario.Email;
        existing.Senha = usuario.Senha;
        existing.TipoUsuarioId = usuario.TipoUsuarioId;
        existing.Ativo = usuario.Ativo;

        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var usuario = await _ctx.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();

        _ctx.Usuarios.Remove(usuario);
        await _ctx.SaveChangesAsync();

        return NoContent();
    }
}