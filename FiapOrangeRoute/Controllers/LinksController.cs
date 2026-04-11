using FiapOrangeRoute.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class LinksController : ControllerBase
{
    private readonly AppDbContext _ctx;
    private readonly ILogger<LinksController> _log;

    public LinksController(AppDbContext ctx, ILogger<LinksController> log)
    {
        _ctx = ctx;
        _log = log;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _ctx.Links.Include(l => l.TrilhaCarreira).ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var link = await _ctx.Links.FindAsync(id);
        if (link == null) return NotFound();

        return Ok(link);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Link link)
    {
        var trilha = await _ctx.TrilhasCarreira.FindAsync(link.IdTrilhaCarreira);

        if (trilha == null)
            return BadRequest("Trilha invalida");

        _ctx.Links.Add(link);
        await _ctx.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = link.Id }, link);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Link link)
    {
        if (id != link.Id) return BadRequest();

        var existing = await _ctx.Links.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Titulo = link.Titulo;
        existing.Conteudo = link.Conteudo;
        existing.IdTrilhaCarreira = link.IdTrilhaCarreira;

        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var link = await _ctx.Links.FindAsync(id);
        if (link == null) return NotFound();

        _ctx.Links.Remove(link);
        await _ctx.SaveChangesAsync();

        return NoContent();
    }
}