using FiapOrangeRoute.DTOs.Favorito;
using FiapOrangeRoute.Repositories.Interfaces;
using FiapOrangeRoute.Services.Interfaces;

namespace FiapOrangeRoute.Services.Implementations;

public class FavoritoService : IFavoritoService
{
    private readonly IFavoritoRepository _repository;

    public FavoritoService(IFavoritoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<FavoritoResponseDTO>> GetAllAsync()
    {
        var favoritos = await _repository.GetAllAsync();

        return favoritos.Select(f => new FavoritoResponseDTO
        {
            Id = f.Id,
            IdUsuario = f.IdUsuario,
            UsuarioNome = f.Usuario?.Nome,
            IdTrilhaCarreira = f.IdTrilhaCarreira,
            TrilhaTitulo = f.TrilhaCarreira?.Titulo
        });
    }

    public async Task<FavoritoResponseDTO?> GetByIdAsync(int id)
    {
        var favorito = await _repository.GetByIdAsync(id);

        if (favorito == null)
            return null;

        return new FavoritoResponseDTO
        {
            Id = favorito.Id,
            IdUsuario = favorito.IdUsuario,
            UsuarioNome = favorito.Usuario?.Nome,
            IdTrilhaCarreira = favorito.IdTrilhaCarreira,
            TrilhaTitulo = favorito.TrilhaCarreira?.Titulo
        };
    }

    public async Task<FavoritoResponseDTO> CreateAsync(FavoritoCreateDTO dto)
    {
        var favorito = new Favorito
        {
            IdUsuario = dto.IdUsuario,
            IdTrilhaCarreira = dto.IdTrilhaCarreira
        };

        var created = await _repository.CreateAsync(favorito);

        return new FavoritoResponseDTO
        {
            Id = created.Id,
            IdUsuario = created.IdUsuario,
            IdTrilhaCarreira = created.IdTrilhaCarreira
        };
    }

    public async Task<bool> UpdateAsync(int id, FavoritoUpdateDTO dto)
    {
        var favorito = await _repository.GetByIdAsync(id);

        if (favorito == null)
            return false;

        favorito.IdUsuario = dto.IdUsuario;
        favorito.IdTrilhaCarreira = dto.IdTrilhaCarreira;

        await _repository.UpdateAsync(favorito);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _repository.ExistsAsync(id);

        if (!exists)
            return false;

        await _repository.DeleteAsync(id);

        return true;
    }
}