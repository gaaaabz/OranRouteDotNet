// Services/Interfaces/IComentarioService.cs

using FiapOrangeRoute.DTOs.Comentario;
using FiapOrangeRoute.Helpers;

namespace FiapOrangeRoute.Services.Interfaces;

public interface IComentarioService
{
    Task<PagedResult<ComentarioResponseDTO>> GetPagedAsync(
        PaginationParams paginationParams);

    Task<IEnumerable<ComentarioResponseDTO>> GetAllAsync();

    Task<ComentarioResponseDTO?> GetByIdAsync(int id);

    Task<ComentarioResponseDTO> CreateAsync(
        ComentarioCreateDTO dto);

    Task<bool> UpdateAsync(
        int id,
        ComentarioUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}