// Controllers/TrilhasCarreiraController.cs

using FiapOrangeRoute.DTOs.TrilhaCarreira;
using FiapOrangeRoute.Helpers;
using FiapOrangeRoute.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiapOrangeRoute.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrilhasCarreiraController : ControllerBase
{
    private readonly ITrilhaCarreiraService _service;

    public TrilhasCarreiraController(
        ITrilhaCarreiraService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] PaginationParams paginationParams)
    {
        var trilhas = await _service
            .GetPagedAsync(paginationParams);

        return Ok(trilhas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var trilha = await _service.GetByIdAsync(id);

        if (trilha == null)
            return NotFound();

        return Ok(trilha);
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        TrilhaCarreiraCreateDTO dto)
    {
        var created = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(
        int id,
        TrilhaCarreiraUpdateDTO dto)
    {
        var updated = await _service
            .UpdateAsync(id, dto);

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