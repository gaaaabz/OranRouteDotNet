using FiapOrangeRoute.Models;

namespace FiapOrangeRoute.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAllAsync();

    Task<Usuario?> GetByIdAsync(int id);

    Task<Usuario?> GetByEmailAsync(string email);

    Task<Usuario> CreateAsync(Usuario usuario);

    Task UpdateAsync(Usuario usuario);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}