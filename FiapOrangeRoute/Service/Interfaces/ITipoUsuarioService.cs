using FiapOrangeRoute.DTOs.TipoUsuario;

namespace FiapOrangeRoute.Services.Interfaces;

public interface ITipoUsuarioService
{
    Task<IEnumerable<TipoUsuarioResponseDTO>> GetAllAsync();

    Task<TipoUsuarioResponseDTO?> GetByIdAsync(int id);

    Task<TipoUsuarioResponseDTO> CreateAsync(TipoUsuarioCreateDTO dto);

    Task<bool> UpdateAsync(int id, TipoUsuarioUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}