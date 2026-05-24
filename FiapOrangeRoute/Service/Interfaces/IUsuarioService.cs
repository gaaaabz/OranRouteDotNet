// Services/Interfaces/IUsuarioService.cs

using FiapOrangeRoute.DTOs.Usuario;
using FiapOrangeRoute.Helpers;

namespace FiapOrangeRoute.Services.Interfaces;

public interface IUsuarioService
{
    Task<PagedResult<UsuarioResponseDTO>> GetPagedAsync(
        PaginationParams paginationParams);

    Task<IEnumerable<UsuarioResponseDTO>> GetAllAsync();

    Task<UsuarioResponseDTO?> GetByIdAsync(int id);

    Task<UsuarioResponseDTO> CreateAsync(UsuarioCreateDTO dto);

    Task<bool> UpdateAsync(int id, UsuarioUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}