using FiapOrangeRoute.DTOs.Usuario;
using FiapOrangeRoute.Models;
using FiapOrangeRoute.Repositories.Interfaces;
using FiapOrangeRoute.Services.Interfaces;

namespace FiapOrangeRoute.Services.Implementations;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UsuarioResponseDTO>> GetAllAsync()
    {
        var usuarios = await _repository.GetAllAsync();

        return usuarios.Select(u => new UsuarioResponseDTO
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email,
            Ativo = u.Ativo,
            TipoUsuarioId = u.TipoUsuarioId,
            TipoUsuarioNome = u.TipoUsuario?.Nome
        });
    }

    public async Task<UsuarioResponseDTO?> GetByIdAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);

        if (usuario == null)
            return null;

        return new UsuarioResponseDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Ativo = usuario.Ativo,
            TipoUsuarioId = usuario.TipoUsuarioId,
            TipoUsuarioNome = usuario.TipoUsuario?.Nome
        };
    }

    public async Task<UsuarioResponseDTO> CreateAsync(UsuarioCreateDTO dto)
    {
        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = dto.Senha,
            Foto = dto.Foto,
            TipoUsuarioId = dto.TipoUsuarioId,
            Ativo = "A"
        };

        var created = await _repository.CreateAsync(usuario);

        return new UsuarioResponseDTO
        {
            Id = created.Id,
            Nome = created.Nome,
            Email = created.Email,
            Ativo = created.Ativo,
            TipoUsuarioId = created.TipoUsuarioId
        };
    }

    public async Task<bool> UpdateAsync(int id, UsuarioUpdateDTO dto)
    {
        var usuario = await _repository.GetByIdAsync(id);

        if (usuario == null)
            return false;

        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email;
        usuario.Senha = dto.Senha;
        usuario.Foto = dto.Foto;
        usuario.Ativo = dto.Ativo;
        usuario.TipoUsuarioId = dto.TipoUsuarioId;

        await _repository.UpdateAsync(usuario);

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