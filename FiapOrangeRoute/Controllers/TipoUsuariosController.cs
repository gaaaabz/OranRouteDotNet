using FiapOrangeRoute.DTOs.TipoUsuario;
using FiapOrangeRoute.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiapOrangeRoute.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TiposUsuarioController : ControllerBase
{
    private readonly ITipoUsuarioService _service;

    public TiposUsuarioController(ITipoUsuarioService service)
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
        var tipo = await _service.GetByIdAsync(id);

        if (tipo == null)
            return NotFound();

        return Ok(tipo);
    }

    [HttpPost]
    public async Task<IActionResult> Post(TipoUsuarioCreateDTO dto)
    {
        var created = await _service.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById),
            new { id = created.Id },
            created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, TipoUsuarioUpdateDTO dto)
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