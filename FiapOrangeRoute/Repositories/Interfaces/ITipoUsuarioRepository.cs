using FiapOrangeRoute.Helpers;
using FiapOrangeRoute.Models;

namespace FiapOrangeRoute.Repositories.Interfaces;

public interface ITipoUsuarioRepository
{
    Task<PagedResult<TipoUsuario>> GetPagedAsync(
        PaginationParams paginationParams);

    Task<IEnumerable<TipoUsuario>> GetAllAsync();

    Task<TipoUsuario?> GetByIdAsync(int id);

    Task<TipoUsuario> CreateAsync(TipoUsuario tipoUsuario);

    Task UpdateAsync(TipoUsuario tipoUsuario);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}