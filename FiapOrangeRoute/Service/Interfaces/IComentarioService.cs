using FiapOrangeRoute.DTOs.Comentario;

namespace FiapOrangeRoute.Services.Interfaces;

public interface IComentarioService
{
    Task<IEnumerable<ComentarioResponseDTO>> GetAllAsync();

    Task<ComentarioResponseDTO?> GetByIdAsync(int id);

    Task<ComentarioResponseDTO> CreateAsync(ComentarioCreateDTO dto);

    Task<bool> UpdateAsync(int id, ComentarioUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}