using FiapOrangeRoute.DTOs.TipoUsuario;
using FiapOrangeRoute.Models;
using FiapOrangeRoute.Repositories.Interfaces;
using FiapOrangeRoute.Services.Interfaces;

namespace FiapOrangeRoute.Services.Implementations;

public class TipoUsuarioService : ITipoUsuarioService
{
    private readonly ITipoUsuarioRepository _repository;

    public TipoUsuarioService(ITipoUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TipoUsuarioResponseDTO>> GetAllAsync()
    {
        var tipos = await _repository.GetAllAsync();

        return tipos.Select(t => new TipoUsuarioResponseDTO
        {
            Id = t.Id,
            Nome = t.Nome
        });
    }

    public async Task<TipoUsuarioResponseDTO?> GetByIdAsync(int id)
    {
        var tipo = await _repository.GetByIdAsync(id);

        if (tipo == null)
            return null;

        return new TipoUsuarioResponseDTO
        {
            Id = tipo.Id,
            Nome = tipo.Nome
        };
    }

    public async Task<TipoUsuarioResponseDTO> CreateAsync(TipoUsuarioCreateDTO dto)
    {
        var tipo = new TipoUsuario
        {
            Nome = dto.Nome
        };

        var created = await _repository.CreateAsync(tipo);

        return new TipoUsuarioResponseDTO
        {
            Id = created.Id,
            Nome = created.Nome
        };
    }

    public async Task<bool> UpdateAsync(int id, TipoUsuarioUpdateDTO dto)
    {
        var tipo = await _repository.GetByIdAsync(id);

        if (tipo == null)
            return false;

        tipo.Nome = dto.Nome;

        await _repository.UpdateAsync(tipo);

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