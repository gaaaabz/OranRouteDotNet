// Services/Implementations/TagService.cs

using FiapOrangeRoute.DTOs.Tag;
using FiapOrangeRoute.Helpers;
using FiapOrangeRoute.Models;
using FiapOrangeRoute.Repositories.Interfaces;
using FiapOrangeRoute.Services.Interfaces;

namespace FiapOrangeRoute.Services.Implementations;

public class TagService : ITagService
{
    private readonly ITagRepository _repository;
    private readonly ILogger<TagService> _logger;

    public TagService(
        ITagRepository repository,
        ILogger<TagService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PagedResult<TagResponseDTO>>
        GetPagedAsync(PaginationParams paginationParams)
    {
        var pagedResult = await _repository
            .GetPagedAsync(paginationParams);

        return new PagedResult<TagResponseDTO>
        {
            Items = pagedResult.Items.Select(t =>
                new TagResponseDTO
                {
                    Id = t.Id,
                    Nome = t.Nome
                }),

            TotalItems = pagedResult.TotalItems,
            PageNumber = pagedResult.PageNumber,
            PageSize = pagedResult.PageSize
        };
    }

    public async Task<IEnumerable<TagResponseDTO>>
        GetAllAsync()
    {
        var tags = await _repository.GetAllAsync();

        return tags.Select(t =>
            new TagResponseDTO
            {
                Id = t.Id,
                Nome = t.Nome
            });
    }

    public async Task<TagResponseDTO?>
        GetByIdAsync(int id)
    {
        var tag = await _repository.GetByIdAsync(id);

        if (tag == null)
            return null;

        return new TagResponseDTO
        {
            Id = tag.Id,
            Nome = tag.Nome
        };
    }

    public async Task<TagResponseDTO>
        CreateAsync(TagCreateDTO dto)
    {
        var tag = new Tag
        {
            Nome = dto.Nome
        };

        var created = await _repository.CreateAsync(tag);

        _logger.LogInformation(
            "Tag criada: {Nome}",
            created.Nome);

        return new TagResponseDTO
        {
            Id = created.Id,
            Nome = created.Nome
        };
    }

    public async Task<bool> UpdateAsync(
        int id,
        TagUpdateDTO dto)
    {
        var tag = await _repository.GetByIdAsync(id);

        if (tag == null)
            return false;

        tag.Nome = dto.Nome;

        await _repository.UpdateAsync(tag);

        _logger.LogInformation(
            "Tag atualizada: {Id}",
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
            "Tag removida: {Id}",
            id);

        return true;
    }
}