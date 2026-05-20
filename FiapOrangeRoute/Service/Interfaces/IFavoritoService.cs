using FiapOrangeRoute.DTOs.Favorito;

namespace FiapOrangeRoute.Services.Interfaces;

public interface IFavoritoService
{
    Task<IEnumerable<FavoritoResponseDTO>> GetAllAsync();

    Task<FavoritoResponseDTO?> GetByIdAsync(int id);

    Task<FavoritoResponseDTO> CreateAsync(FavoritoCreateDTO dto);

    Task<bool> UpdateAsync(int id, FavoritoUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}