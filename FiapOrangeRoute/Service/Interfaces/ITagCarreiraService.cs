using FiapOrangeRoute.DTOs.TagCarreira;

namespace FiapOrangeRoute.Services.Interfaces;

public interface ITagCarreiraService
{
    Task<IEnumerable<TagCarreiraResponseDTO>> GetAllAsync();

    Task<TagCarreiraResponseDTO?> GetByIdAsync(int id);

    Task<TagCarreiraResponseDTO> CreateAsync(TagCarreiraCreateDTO dto);

    Task<bool> UpdateAsync(int id, TagCarreiraUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}