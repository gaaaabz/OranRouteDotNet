using FiapOrangeRoute.DTOs.Tag;
using FiapOrangeRoute.Models;
using FiapOrangeRoute.Repositories.Interfaces;
using FiapOrangeRoute.Services.Interfaces;

namespace FiapOrangeRoute.Services.Implementations;

public class TagService : ITagService
{
    private readonly ITagRepository _repository;

    public TagService(ITagRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TagResponseDTO>> GetAllAsync()
    {
        var tags = await _repository.GetAllAsync();

        return tags.Select(t => new TagResponseDTO
        {
            Id = t.Id,
            Nome = t.Nome
        });
    }

    public async Task<TagResponseDTO?> GetByIdAsync(int id)
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

    public async Task<TagResponseDTO> CreateAsync(TagCreateDTO dto)
    {
        var tag = new Tag
        {
            Nome = dto.Nome
        };

        var created = await _repository.CreateAsync(tag);

        return new TagResponseDTO
        {
            Id = created.Id,
            Nome = created.Nome
        };
    }

    public async Task<bool> UpdateAsync(int id, TagUpdateDTO dto)
    {
        var tag = await _repository.GetByIdAsync(id);

        if (tag == null)
            return false;

        tag.Nome = dto.Nome;

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