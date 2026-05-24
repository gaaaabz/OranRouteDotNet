using FiapOrangeRoute.DTOs.Favorito;
using FiapOrangeRoute.Helpers;
using FiapOrangeRoute.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiapOrangeRoute.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritosController : ControllerBase
{
    private readonly IFavoritoService _favoritoService;

    public FavoritosController(IFavoritoService favoritoService)
    {
        _favoritoService = favoritoService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] PaginationParams paginationParams)
    {
        var favoritos = await _favoritoService
            .GetPagedAsync(paginationParams);

        return Ok(favoritos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var favorito = await _favoritoService
            .GetByIdAsync(id);

        if (favorito == null)
            return NotFound();

        return Ok(favorito);
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] FavoritoCreateDTO dto)
    {
        var created = await _favoritoService
            .CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(
        int id,
        [FromBody] FavoritoUpdateDTO dto)
    {
        var updated = await _favoritoService
            .UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _favoritoService
            .DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}