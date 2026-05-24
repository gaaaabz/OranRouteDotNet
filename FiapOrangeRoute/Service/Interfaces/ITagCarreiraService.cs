// Services/Interfaces/ITagCarreiraService.cs

using FiapOrangeRoute.DTOs.TagCarreira;
using FiapOrangeRoute.Helpers;

namespace FiapOrangeRoute.Services.Interfaces;

public interface ITagCarreiraService
{
    Task<PagedResult<TagCarreiraResponseDTO>> GetPagedAsync(
        PaginationParams paginationParams);

    Task<IEnumerable<TagCarreiraResponseDTO>> GetAllAsync();

    Task<TagCarreiraResponseDTO?> GetByIdAsync(int id);

    Task<TagCarreiraResponseDTO> CreateAsync(
        TagCarreiraCreateDTO dto);

    Task<bool> UpdateAsync(
        int id,
        TagCarreiraUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}