// Services/Implementations/TagCarreiraService.cs

using FiapOrangeRoute.DTOs.TagCarreira;
using FiapOrangeRoute.Helpers;
using FiapOrangeRoute.Models;
using FiapOrangeRoute.Repositories.Interfaces;
using FiapOrangeRoute.Services.Interfaces;

namespace FiapOrangeRoute.Services.Implementations;

public class TagCarreiraService : ITagCarreiraService
{
    private readonly ITagCarreiraRepository _repository;
    private readonly ILogger<TagCarreiraService> _logger;

    public TagCarreiraService(
        ITagCarreiraRepository repository,
        ILogger<TagCarreiraService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PagedResult<TagCarreiraResponseDTO>>
        GetPagedAsync(PaginationParams paginationParams)
    {
        var pagedResult = await _repository
            .GetPagedAsync(paginationParams);

        return new PagedResult<TagCarreiraResponseDTO>
        {
            Items = pagedResult.Items.Select(t =>
                new TagCarreiraResponseDTO
                {
                    Id = t.Id,
                    IdTag = t.IdTag,
                    TagNome = t.Tag?.Nome,
                    IdTrilhaCarreira = t.IdTrilhaCarreira,
                    TrilhaTitulo = t.TrilhaCarreira?.Titulo
                }),

            TotalItems = pagedResult.TotalItems,
            PageNumber = pagedResult.PageNumber,
            PageSize = pagedResult.PageSize
        };
    }

    public async Task<IEnumerable<TagCarreiraResponseDTO>>
        GetAllAsync()
    {
        var tags = await _repository.GetAllAsync();

        return tags.Select(t =>
            new TagCarreiraResponseDTO
            {
                Id = t.Id,
                IdTag = t.IdTag,
                TagNome = t.Tag?.Nome,
                IdTrilhaCarreira = t.IdTrilhaCarreira,
                TrilhaTitulo = t.TrilhaCarreira?.Titulo
            });
    }

    public async Task<TagCarreiraResponseDTO?>
        GetByIdAsync(int id)
    {
        var tag = await _repository.GetByIdAsync(id);

        if (tag == null)
            return null;

        return new TagCarreiraResponseDTO
        {
            Id = tag.Id,
            IdTag = tag.IdTag,
            TagNome = tag.Tag?.Nome,
            IdTrilhaCarreira = tag.IdTrilhaCarreira,
            TrilhaTitulo = tag.TrilhaCarreira?.Titulo
        };
    }

    public async Task<TagCarreiraResponseDTO>
        CreateAsync(TagCarreiraCreateDTO dto)
    {
        var tag = new TagCarreira
        {
            IdTag = dto.IdTag,
            IdTrilhaCarreira = dto.IdTrilhaCarreira
        };

        var created = await _repository
            .CreateAsync(tag);

        _logger.LogInformation(
            "TagCarreira criada: {Id}",
            created.Id);

        return new TagCarreiraResponseDTO
        {
            Id = created.Id,
            IdTag = created.IdTag,
            IdTrilhaCarreira = created.IdTrilhaCarreira
        };
    }

    public async Task<bool> UpdateAsync(
        int id,
        TagCarreiraUpdateDTO dto)
    {
        var tag = await _repository.GetByIdAsync(id);

        if (tag == null)
            return false;

        tag.IdTag = dto.IdTag;
        tag.IdTrilhaCarreira = dto.IdTrilhaCarreira;

        await _repository.UpdateAsync(tag);

        _logger.LogInformation(
            "TagCarreira atualizada: {Id}",
            tag.Id);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _repository.ExistsAsync(id);

        if (!exists)
            return false;

        await _repository.DeleteAsync(id);

        _logger.LogInformation(
            "TagCarreira removida: {Id}",
            id);

        return true;
    }
}