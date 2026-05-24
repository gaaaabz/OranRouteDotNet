// Services/Interfaces/ITrilhaCarreiraService.cs

using FiapOrangeRoute.DTOs.TrilhaCarreira;
using FiapOrangeRoute.Helpers;

namespace FiapOrangeRoute.Services.Interfaces;

public interface ITrilhaCarreiraService
{
    Task<PagedResult<TrilhaCarreiraResponseDTO>> GetPagedAsync(
        PaginationParams paginationParams);

    Task<IEnumerable<TrilhaCarreiraResponseDTO>> GetAllAsync();

    Task<TrilhaCarreiraResponseDTO?> GetByIdAsync(int id);

    Task<TrilhaCarreiraResponseDTO> CreateAsync(
        TrilhaCarreiraCreateDTO dto);

    Task<bool> UpdateAsync(
        int id,
        TrilhaCarreiraUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}