using FiapOrangeRoute.DTOs.TagCarreira;
using FiapOrangeRoute.Models;
using FiapOrangeRoute.Repositories.Interfaces;
using FiapOrangeRoute.Services.Interfaces;

namespace FiapOrangeRoute.Services.Implementations;

public class TagCarreiraService : ITagCarreiraService
{
    private readonly ITagCarreiraRepository _repository;

    public TagCarreiraService(ITagCarreiraRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TagCarreiraResponseDTO>> GetAllAsync()
    {
        var tags = await _repository.GetAllAsync();

        return tags.Select(t => new TagCarreiraResponseDTO
        {
            Id = t.Id,
            IdTag = t.IdTag,
            TagNome = t.Tag?.Nome,
            IdTrilhaCarreira = t.IdTrilhaCarreira,
            TrilhaTitulo = t.TrilhaCarreira?.Titulo
        });
    }

    public async Task<TagCarreiraResponseDTO?> GetByIdAsync(int id)
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

    public async Task<TagCarreiraResponseDTO> CreateAsync(TagCarreiraCreateDTO dto)
    {
        var tag = new TagCarreira
        {
            IdTag = dto.IdTag,
            IdTrilhaCarreira = dto.IdTrilhaCarreira
        };

        var created = await _repository.CreateAsync(tag);

        return new TagCarreiraResponseDTO
        {
            Id = created.Id,
            IdTag = created.IdTag,
            IdTrilhaCarreira = created.IdTrilhaCarreira
        };
    }

    public async Task<bool> UpdateAsync(int id, TagCarreiraUpdateDTO dto)
    {
        var tag = await _repository.GetByIdAsync(id);

        if (tag == null)
            return false;

        tag.IdTag = dto.IdTag;
        tag.IdTrilhaCarreira = dto.IdTrilhaCarreira;

        await _repository.UpdateAsync(tag);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _repository.ExistsAsync(id);

        if (!exists)
            return false;

        await _repository.DeleteAsync(id);

        return true;
    }
}