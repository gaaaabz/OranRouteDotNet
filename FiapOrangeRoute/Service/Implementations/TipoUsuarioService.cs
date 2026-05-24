// Services/Implementations/TipoUsuarioService.cs

using FiapOrangeRoute.DTOs.TipoUsuario;
using FiapOrangeRoute.Helpers;
using FiapOrangeRoute.Models;
using FiapOrangeRoute.Repositories.Interfaces;
using FiapOrangeRoute.Services.Interfaces;

namespace FiapOrangeRoute.Services.Implementations;

public class TipoUsuarioService : ITipoUsuarioService
{
    private readonly ITipoUsuarioRepository _repository;
    private readonly ILogger<TipoUsuarioService> _logger;

    public TipoUsuarioService(
        ITipoUsuarioRepository repository,
        ILogger<TipoUsuarioService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PagedResult<TipoUsuarioResponseDTO>>
        GetPagedAsync(PaginationParams paginationParams)
    {
        var pagedResult = await _repository
            .GetPagedAsync(paginationParams);

        return new PagedResult<TipoUsuarioResponseDTO>
        {
            Items = pagedResult.Items.Select(t =>
                new TipoUsuarioResponseDTO
                {
                    Id = t.Id,
                    Nome = t.Nome
                }),

            TotalItems = pagedResult.TotalItems,
            PageNumber = pagedResult.PageNumber,
            PageSize = pagedResult.PageSize
        };
    }

    public async Task<IEnumerable<TipoUsuarioResponseDTO>>
        GetAllAsync()
    {
        var tipos = await _repository.GetAllAsync();

        return tipos.Select(t =>
            new TipoUsuarioResponseDTO
            {
                Id = t.Id,
                Nome = t.Nome
            });
    }

    public async Task<TipoUsuarioResponseDTO?>
        GetByIdAsync(int id)
    {
        var tipo = await _repository.GetByIdAsync(id);

        if (tipo == null)
            return null;

        return new TipoUsuarioResponseDTO
        {
            Id = tipo.Id,
            Nome = tipo.Nome
        };
    }

    public async Task<TipoUsuarioResponseDTO>
        CreateAsync(TipoUsuarioCreateDTO dto)
    {
        var tipo = new TipoUsuario
        {
            Nome = dto.Nome
        };

        var created = await _repository
            .CreateAsync(tipo);

        _logger.LogInformation(
            "TipoUsuario criado: {Nome}",
            created.Nome);

        return new TipoUsuarioResponseDTO
        {
            Id = created.Id,
            Nome = created.Nome
        };
    }

    public async Task<bool> UpdateAsync(
        int id,
        TipoUsuarioUpdateDTO dto)
    {
        var tipo = await _repository.GetByIdAsync(id);

        if (tipo == null)
            return false;

        tipo.Nome = dto.Nome;

        await _repository.UpdateAsync(tipo);

        _logger.LogInformation(
            "TipoUsuario atualizado: {Id}",
            tipo.Id);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _repository.ExistsAsync(id);

        if (!exists)
            return false;

        await _repository.DeleteAsync(id);

        _logger.LogInformation(
            "TipoUsuario removido: {Id}",
            id);

        return true;
    }
}