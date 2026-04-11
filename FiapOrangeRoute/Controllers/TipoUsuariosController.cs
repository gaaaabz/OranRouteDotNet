using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FiapOrangeRoute.Data;
using FiapOrangeRoute.Models;

namespace FiapOrangeRoute.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TiposUsuarioController : ControllerBase
{
    private readonly AppDbContext _ctx;
    private readonly ILogger<TiposUsuarioController> _log;

    public TiposUsuarioController(AppDbContext ctx, ILogger<TiposUsuarioController> log)
    {
        _ctx = ctx;
        _log = log;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _log.LogInformation("Listando tipos de usuario");
        return Ok(await _ctx.TiposUsuario.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tipo = await _ctx.TiposUsuario.FindAsync(id);

        if (tipo == null)
        {
            _log.LogWarning("TipoUsuario {Id} nao encontrado", id);
            return NotFound();
        }

        return Ok(tipo);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TipoUsuario tipo)
    {
        _ctx.TiposUsuario.Add(tipo);
        await _ctx.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = tipo.Id }, tipo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TipoUsuario tipo)
    {
        if (id != tipo.Id) return BadRequest();

        var existing = await _ctx.TiposUsuario.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Nome = tipo.Nome;

        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var tipo = await _ctx.TiposUsuario.FindAsync(id);
        if (tipo == null) return NotFound();

        _ctx.TiposUsuario.Remove(tipo);
        await _ctx.SaveChangesAsync();

        return NoContent();
    }
}