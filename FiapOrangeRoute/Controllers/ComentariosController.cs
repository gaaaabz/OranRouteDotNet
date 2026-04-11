using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FiapOrangeRoute.Data;
using FiapOrangeRoute.Models;

namespace FiapOrangeRoute.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComentariosController : ControllerBase
{
    private readonly AppDbContext _ctx;
    private readonly ILogger<ComentariosController> _log;

    public ComentariosController(AppDbContext ctx, ILogger<ComentariosController> log)
    {
        _ctx = ctx;
        _log = log;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _log.LogInformation("Listando comentarios");

        var comentarios = await _ctx.Comentarios
            .Include(c => c.Usuario)
            .Include(c => c.TrilhaCarreira)
            .ToListAsync();

        return Ok(comentarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var comentario = await _ctx.Comentarios
            .Include(c => c.Usuario)
            .Include(c => c.TrilhaCarreira)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (comentario == null)
        {
            _log.LogWarning("Comentario {Id} nao encontrado", id);
            return NotFound();
        }

        return Ok(comentario);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Comentario comentario)
    {
        var usuario = await _ctx.Usuarios.FindAsync(comentario.IdUsuario);
        var trilha = await _ctx.TrilhasCarreira.FindAsync(comentario.IdTrilhaCarreira);

        if (usuario == null || trilha == null)
        {
            _log.LogWarning("FK invalida em comentario");
            return BadRequest("Usuario ou Trilha invalido");
        }

        _ctx.Comentarios.Add(comentario);
        await _ctx.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = comentario.Id }, comentario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Comentario comentario)
    {
        if (id != comentario.Id)
            return BadRequest();

        var existing = await _ctx.Comentarios.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.Conteudo = comentario.Conteudo;
        existing.Ativo = comentario.Ativo;
        existing.IdUsuario = comentario.IdUsuario;
        existing.IdTrilhaCarreira = comentario.IdTrilhaCarreira;

        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var comentario = await _ctx.Comentarios.FindAsync(id);
        if (comentario == null)
            return NotFound();

        _ctx.Comentarios.Remove(comentario);
        await _ctx.SaveChangesAsync();

        return NoContent();
    }
}