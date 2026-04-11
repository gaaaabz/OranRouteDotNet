using FiapOrangeRoute.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class FavoritosController : ControllerBase
{
    private readonly AppDbContext _ctx;
    private readonly ILogger<FavoritosController> _log;

    public FavoritosController(AppDbContext ctx, ILogger<FavoritosController> log)
    {
        _ctx = ctx;
        _log = log;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var favoritos = await _ctx.Favoritos
            .Include(f => f.Usuario)
            .Include(f => f.TrilhaCarreira)
            .ToListAsync();

        return Ok(favoritos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var favorito = await _ctx.Favoritos.FindAsync(id);

        if (favorito == null)
            return NotFound();

        return Ok(favorito);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Favorito favorito)
    {
        var usuario = await _ctx.Usuarios.FindAsync(favorito.IdUsuario);
        var trilha = await _ctx.TrilhasCarreira.FindAsync(favorito.IdTrilhaCarreira);

        if (usuario == null || trilha == null)
            return BadRequest("Usuario ou Trilha invalido");

        _ctx.Favoritos.Add(favorito);
        await _ctx.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = favorito.Id }, favorito);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Favorito favorito)
    {
        if (id != favorito.Id)
            return BadRequest();

        var existing = await _ctx.Favoritos.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.IdUsuario = favorito.IdUsuario;
        existing.IdTrilhaCarreira = favorito.IdTrilhaCarreira;

        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var favorito = await _ctx.Favoritos.FindAsync(id);
        if (favorito == null)
            return NotFound();

        _ctx.Favoritos.Remove(favorito);
        await _ctx.SaveChangesAsync();

        return NoContent();
    }
}