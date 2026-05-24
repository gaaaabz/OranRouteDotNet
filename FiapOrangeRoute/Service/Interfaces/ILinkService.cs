// Services/Interfaces/ILinkService.cs

using FiapOrangeRoute.DTOs.Link;
using FiapOrangeRoute.Helpers;

namespace FiapOrangeRoute.Services.Interfaces;

public interface ILinkService
{
    Task<PagedResult<LinkResponseDTO>> GetPagedAsync(
        PaginationParams paginationParams);

    Task<IEnumerable<LinkResponseDTO>> GetAllAsync();

    Task<LinkResponseDTO?> GetByIdAsync(int id);

    Task<LinkResponseDTO> CreateAsync(LinkCreateDTO dto);

    Task<bool> UpdateAsync(int id, LinkUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}