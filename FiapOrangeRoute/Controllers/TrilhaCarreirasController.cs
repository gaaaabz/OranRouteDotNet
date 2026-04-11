using FiapOrangeRoute.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TrilhasCarreiraController : ControllerBase
{
    private readonly AppDbContext _ctx;
    private readonly ILogger<TrilhasCarreiraController> _log;

    public TrilhasCarreiraController(AppDbContext ctx, ILogger<TrilhasCarreiraController> log)
    {
        _ctx = ctx;
        _log = log;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trilhas = await _ctx.TrilhasCarreira
            .Include(t => t.Links)
            .ToListAsync();

        return Ok(trilhas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var trilha = await _ctx.TrilhasCarreira.FindAsync(id);
        if (trilha == null) return NotFound();

        return Ok(trilha);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TrilhaCarreira trilha)
    {
        _ctx.TrilhasCarreira.Add(trilha);
        await _ctx.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = trilha.Id }, trilha);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TrilhaCarreira trilha)
    {
        if (id != trilha.Id) return BadRequest();

        var existing = await _ctx.TrilhasCarreira.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Titulo = trilha.Titulo;
        existing.Conteudo = trilha.Conteudo;

        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var trilha = await _ctx.TrilhasCarreira.FindAsync(id);
        if (trilha == null) return NotFound();

        _ctx.TrilhasCarreira.Remove(trilha);
        await _ctx.SaveChangesAsync();

        return NoContent();
    }
}