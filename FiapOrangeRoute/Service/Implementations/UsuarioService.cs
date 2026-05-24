// Services/Implementations/UsuarioService.cs

using FiapOrangeRoute.DTOs.Usuario;
using FiapOrangeRoute.Helpers;
using FiapOrangeRoute.Models;
using FiapOrangeRoute.Repositories.Interfaces;
using FiapOrangeRoute.Services.Interfaces;

namespace FiapOrangeRoute.Services.Implementations;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;
    private readonly ILogger<UsuarioService> _logger;

    public UsuarioService(
        IUsuarioRepository repository,
        ILogger<UsuarioService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PagedResult<UsuarioResponseDTO>> GetPagedAsync(
        PaginationParams paginationParams)
    {
        var pagedUsuarios = await _repository
            .GetPagedAsync(paginationParams);

        return new PagedResult<UsuarioResponseDTO>
        {
            Items = pagedUsuarios.Items.Select(u =>
                new UsuarioResponseDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Ativo = u.Ativo,
                    TipoUsuarioId = u.TipoUsuarioId,
                    TipoUsuarioNome = u.TipoUsuario?.Nome
                }),

            TotalItems = pagedUsuarios.TotalItems,
            PageNumber = pagedUsuarios.PageNumber,
            PageSize = pagedUsuarios.PageSize
        };
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

    public async Task<UsuarioResponseDTO> CreateAsync(
        UsuarioCreateDTO dto)
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

        _logger.LogInformation(
            "Usuário criado: {Email}",
            created.Email);

        return new UsuarioResponseDTO
        {
            Id = created.Id,
            Nome = created.Nome,
            Email = created.Email,
            Ativo = created.Ativo,
            TipoUsuarioId = created.TipoUsuarioId
        };
    }

    public async Task<bool> UpdateAsync(
        int id,
        UsuarioUpdateDTO dto)
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

        _logger.LogInformation(
            "Usuário atualizado: {Id}",
            usuario.Id);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _repository.ExistsAsync(id);

        if (!exists)
            return false;

        await _repository.DeleteAsync(id);

        _logger.LogInformation(
            "Usuário removido: {Id}",
            id);

        return true;
    }
}