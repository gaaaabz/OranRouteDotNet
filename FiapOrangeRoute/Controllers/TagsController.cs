using FiapOrangeRoute.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly AppDbContext _ctx;
    private readonly ILogger<TagsController> _log;

    public TagsController(AppDbContext ctx, ILogger<TagsController> log)
    {
        _ctx = ctx;
        _log = log;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _ctx.Tags.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tag = await _ctx.Tags.FindAsync(id);
        if (tag == null) return NotFound();

        return Ok(tag);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Tag tag)
    {
        _ctx.Tags.Add(tag);
        await _ctx.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = tag.Id }, tag);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Tag tag)
    {
        if (id != tag.Id) return BadRequest();

        var existing = await _ctx.Tags.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Nome = tag.Nome;

        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var tag = await _ctx.Tags.FindAsync(id);
        if (tag == null) return NotFound();

        _ctx.Tags.Remove(tag);
        await _ctx.SaveChangesAsync();

        return NoContent();
    }
}