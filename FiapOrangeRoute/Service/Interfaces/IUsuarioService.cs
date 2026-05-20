using FiapOrangeRoute.DTOs.Usuario;

namespace FiapOrangeRoute.Services.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioResponseDTO>> GetAllAsync();

    Task<UsuarioResponseDTO?> GetByIdAsync(int id);

    Task<UsuarioResponseDTO> CreateAsync(UsuarioCreateDTO dto);

    Task<bool> UpdateAsync(int id, UsuarioUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}