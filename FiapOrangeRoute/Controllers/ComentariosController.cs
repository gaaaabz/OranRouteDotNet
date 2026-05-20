using FiapOrangeRoute.DTOs.Comentario;
using FiapOrangeRoute.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiapOrangeRoute.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComentariosController : ControllerBase
{
    private readonly IComentarioService _service;

    public ComentariosController(IComentarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var comentario = await _service.GetByIdAsync(id);

        if (comentario == null)
            return NotFound();

        return Ok(comentario);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ComentarioCreateDTO dto)
    {
        var created = await _service.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById),
            new { id = created.Id },
            created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ComentarioUpdateDTO dto)
    {
        var updated = await _service.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}