// Services/Interfaces/ITipoUsuarioService.cs

using FiapOrangeRoute.DTOs.TipoUsuario;
using FiapOrangeRoute.Helpers;

namespace FiapOrangeRoute.Services.Interfaces;

public interface ITipoUsuarioService
{
    Task<PagedResult<TipoUsuarioResponseDTO>> GetPagedAsync(
        PaginationParams paginationParams);

    Task<IEnumerable<TipoUsuarioResponseDTO>> GetAllAsync();

    Task<TipoUsuarioResponseDTO?> GetByIdAsync(int id);

    Task<TipoUsuarioResponseDTO> CreateAsync(
        TipoUsuarioCreateDTO dto);

    Task<bool> UpdateAsync(
        int id,
        TipoUsuarioUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}