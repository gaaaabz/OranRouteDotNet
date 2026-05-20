using FiapOrangeRoute.DTOs.Tag;

namespace FiapOrangeRoute.Services.Interfaces;

public interface ITagService
{
    Task<IEnumerable<TagResponseDTO>> GetAllAsync();

    Task<TagResponseDTO?> GetByIdAsync(int id);

    Task<TagResponseDTO> CreateAsync(TagCreateDTO dto);

    Task<bool> UpdateAsync(int id, TagUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}