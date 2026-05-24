// Services/Interfaces/IFavoritoService.cs

using FiapOrangeRoute.DTOs.Favorito;
using FiapOrangeRoute.Helpers;

namespace FiapOrangeRoute.Services.Interfaces;

public interface IFavoritoService
{
    Task<PagedResult<FavoritoResponseDTO>> GetPagedAsync(
        PaginationParams paginationParams);

    Task<IEnumerable<FavoritoResponseDTO>> GetAllAsync();

    Task<FavoritoResponseDTO?> GetByIdAsync(int id);

    Task<FavoritoResponseDTO> CreateAsync(
        FavoritoCreateDTO dto);

    Task<bool> UpdateAsync(
        int id,
        FavoritoUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}