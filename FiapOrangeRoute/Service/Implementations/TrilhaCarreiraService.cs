// Services/Implementations/TrilhaCarreiraService.cs

using FiapOrangeRoute.DTOs.TrilhaCarreira;
using FiapOrangeRoute.Helpers;
using FiapOrangeRoute.Repositories.Interfaces;
using FiapOrangeRoute.Services.Interfaces;

namespace FiapOrangeRoute.Services.Implementations;

public class TrilhaCarreiraService
    : ITrilhaCarreiraService
{
    private readonly ITrilhaCarreiraRepository _repository;
    private readonly ILogger<TrilhaCarreiraService> _logger;

    public TrilhaCarreiraService(
        ITrilhaCarreiraRepository repository,
        ILogger<TrilhaCarreiraService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PagedResult<TrilhaCarreiraResponseDTO>>
        GetPagedAsync(PaginationParams paginationParams)
    {
        var pagedResult = await _repository
            .GetPagedAsync(paginationParams);

        return new PagedResult<TrilhaCarreiraResponseDTO>
        {
            Items = pagedResult.Items.Select(t =>
                new TrilhaCarreiraResponseDTO
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Conteudo = t.Conteudo
                }),

            TotalItems = pagedResult.TotalItems,
            PageNumber = pagedResult.PageNumber,
            PageSize = pagedResult.PageSize
        };
    }

    public async Task<IEnumerable<TrilhaCarreiraResponseDTO>>
        GetAllAsync()
    {
        var trilhas = await _repository.GetAllAsync();

        return trilhas.Select(t =>
            new TrilhaCarreiraResponseDTO
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Conteudo = t.Conteudo
            });
    }

    public async Task<TrilhaCarreiraResponseDTO?>
        GetByIdAsync(int id)
    {
        var trilha = await _repository.GetByIdAsync(id);

        if (trilha == null)
            return null;

        return new TrilhaCarreiraResponseDTO
        {
            Id = trilha.Id,
            Titulo = trilha.Titulo,
            Conteudo = trilha.Conteudo
        };
    }

    public async Task<TrilhaCarreiraResponseDTO>
        CreateAsync(TrilhaCarreiraCreateDTO dto)
    {
        var trilha = new TrilhaCarreira
        {
            Titulo = dto.Titulo,
            Conteudo = dto.Conteudo
        };

        var created = await _repository.CreateAsync(trilha);

        _logger.LogInformation(
            "Trilha criada: {Titulo}",
            created.Titulo);

        return new TrilhaCarreiraResponseDTO
        {
            Id = created.Id,
            Titulo = created.Titulo,
            Conteudo = created.Conteudo
        };
    }

    public async Task<bool> UpdateAsync(
        int id,
        TrilhaCarreiraUpdateDTO dto)
    {
        var trilha = await _repository.GetByIdAsync(id);

        if (trilha == null)
            return false;

        trilha.Titulo = dto.Titulo;
        trilha.Conteudo = dto.Conteudo;

        await _repository.UpdateAsync(trilha);

        _logger.LogInformation(
            "Trilha atualizada: {Id}",
            trilha.Id);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _repository.ExistsAsync(id);

        if (!exists)
            return false;

        await _repository.DeleteAsync(id);

        _logger.LogInformation(
            "Trilha removida: {Id}",
            id);

        return true;
    }
}