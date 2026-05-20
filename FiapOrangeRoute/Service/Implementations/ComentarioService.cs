using FiapOrangeRoute.DTOs.Comentario;
using FiapOrangeRoute.Repositories.Interfaces;
using FiapOrangeRoute.Services.Interfaces;

namespace FiapOrangeRoute.Services.Implementations;

public class ComentarioService : IComentarioService
{
    private readonly IComentarioRepository _repository;

    public ComentarioService(IComentarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ComentarioResponseDTO>> GetAllAsync()
    {
        var comentarios = await _repository.GetAllAsync();

        return comentarios.Select(c => new ComentarioResponseDTO
        {
            Id = c.Id,
            Conteudo = c.Conteudo,
            Ativo = c.Ativo,
            IdUsuario = c.IdUsuario,
            UsuarioNome = c.Usuario?.Nome,
            IdTrilhaCarreira = c.IdTrilhaCarreira,
            TrilhaTitulo = c.TrilhaCarreira?.Titulo
        });
    }

    public async Task<ComentarioResponseDTO?> GetByIdAsync(int id)
    {
        var comentario = await _repository.GetByIdAsync(id);

        if (comentario == null)
            return null;

        return new ComentarioResponseDTO
        {
            Id = comentario.Id,
            Conteudo = comentario.Conteudo,
            Ativo = comentario.Ativo,
            IdUsuario = comentario.IdUsuario,
            UsuarioNome = comentario.Usuario?.Nome,
            IdTrilhaCarreira = comentario.IdTrilhaCarreira,
            TrilhaTitulo = comentario.TrilhaCarreira?.Titulo
        };
    }

    public async Task<ComentarioResponseDTO> CreateAsync(ComentarioCreateDTO dto)
    {
        var comentario = new Comentario
        {
            Conteudo = dto.Conteudo,
            IdUsuario = dto.IdUsuario,
            IdTrilhaCarreira = dto.IdTrilhaCarreira,
            Ativo = "A"
        };

        var created = await _repository.CreateAsync(comentario);

        return new ComentarioResponseDTO
        {
            Id = created.Id,
            Conteudo = created.Conteudo
        };
    }

    public async Task<bool> UpdateAsync(int id, ComentarioUpdateDTO dto)
    {
        var comentario = await _repository.GetByIdAsync(id);

        if (comentario == null)
            return false;

        comentario.Conteudo = dto.Conteudo;
        comentario.Ativo = dto.Ativo;

        await _repository.UpdateAsync(comentario);

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