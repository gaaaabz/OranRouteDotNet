using FiapOrangeRoute.DTOs.TrilhaCarreira;
using FiapOrangeRoute.Repositories.Interfaces;
using FiapOrangeRoute.Services.Interfaces;

namespace FiapOrangeRoute.Services.Implementations;

public class TrilhaCarreiraService : ITrilhaCarreiraService
{
    private readonly ITrilhaCarreiraRepository _repository;

    public TrilhaCarreiraService(ITrilhaCarreiraRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TrilhaCarreiraResponseDTO>> GetAllAsync()
    {
        var trilhas = await _repository.GetAllAsync();

        return trilhas.Select(t => new TrilhaCarreiraResponseDTO
        {
            Id = t.Id,
            Titulo = t.Titulo,
            Conteudo = t.Conteudo
        });
    }

    public async Task<TrilhaCarreiraResponseDTO?> GetByIdAsync(int id)
    {
        var trilha = await _repository.GetByIdAsync(id);

        if (trilha == null)
            return null;

        return new TrilhaCarreiraResponseDTO
        {
            Id = trilha.Id,
            Titulo = trilha.Titulo,
            Conteudo = trilha.Conteudo
        };
    }

    public async Task<TrilhaCarreiraResponseDTO> CreateAsync(TrilhaCarreiraCreateDTO dto)
    {
        var trilha = new TrilhaCarreira
        {
            Titulo = dto.Titulo,
            Conteudo = dto.Conteudo
        };

        var created = await _repository.CreateAsync(trilha);

        return new TrilhaCarreiraResponseDTO
        {
            Id = created.Id,
            Titulo = created.Titulo,
            Conteudo = created.Conteudo
        };
    }

    public async Task<bool> UpdateAsync(int id, TrilhaCarreiraUpdateDTO dto)
    {
        var trilha = await _repository.GetByIdAsync(id);

        if (trilha == null)
            return false;

        trilha.Titulo = dto.Titulo;
        trilha.Conteudo = dto.Conteudo;

        await _repository.UpdateAsync(trilha);

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