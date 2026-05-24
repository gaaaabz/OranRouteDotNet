// Services/Interfaces/ITagService.cs

using FiapOrangeRoute.DTOs.Tag;
using FiapOrangeRoute.Helpers;

namespace FiapOrangeRoute.Services.Interfaces;

public interface ITagService
{
    Task<PagedResult<TagResponseDTO>> GetPagedAsync(
        PaginationParams paginationParams);

    Task<IEnumerable<TagResponseDTO>> GetAllAsync();

    Task<TagResponseDTO?> GetByIdAsync(int id);

    Task<TagResponseDTO> CreateAsync(TagCreateDTO dto);

    Task<bool> UpdateAsync(int id, TagUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}