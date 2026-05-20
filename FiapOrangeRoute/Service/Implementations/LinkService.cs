using FiapOrangeRoute.DTOs.Link;
using FiapOrangeRoute.Models;
using FiapOrangeRoute.Repositories.Interfaces;
using FiapOrangeRoute.Services.Interfaces;

namespace FiapOrangeRoute.Services.Implementations;

public class LinkService : ILinkService
{
    private readonly ILinkRepository _repository;

    public LinkService(ILinkRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LinkResponseDTO>> GetAllAsync()
    {
        var links = await _repository.GetAllAsync();

        return links.Select(l => new LinkResponseDTO
        {
            Id = l.Id,
            Titulo = l.Titulo,
            Conteudo = l.Conteudo,
            IdTrilhaCarreira = l.IdTrilhaCarreira,
            TrilhaTitulo = l.TrilhaCarreira?.Titulo
        });
    }

    public async Task<LinkResponseDTO?> GetByIdAsync(int id)
    {
        var link = await _repository.GetByIdAsync(id);

        if (link == null)
            return null;

        return new LinkResponseDTO
        {
            Id = link.Id,
            Titulo = link.Titulo,
            Conteudo = link.Conteudo,
            IdTrilhaCarreira = link.IdTrilhaCarreira,
            TrilhaTitulo = link.TrilhaCarreira?.Titulo
        };
    }

    public async Task<LinkResponseDTO> CreateAsync(LinkCreateDTO dto)
    {
        var link = new Link
        {
            Titulo = dto.Titulo,
            Conteudo = dto.Conteudo,
            IdTrilhaCarreira = dto.IdTrilhaCarreira
        };

        var created = await _repository.CreateAsync(link);

        return new LinkResponseDTO
        {
            Id = created.Id,
            Titulo = created.Titulo,
            Conteudo = created.Conteudo,
            IdTrilhaCarreira = created.IdTrilhaCarreira
        };
    }

    public async Task<bool> UpdateAsync(int id, LinkUpdateDTO dto)
    {
        var link = await _repository.GetByIdAsync(id);

        if (link == null)
            return false;

        link.Titulo = dto.Titulo;
        link.Conteudo = dto.Conteudo;
        link.IdTrilhaCarreira = dto.IdTrilhaCarreira;

        await _repository.UpdateAsync(link);

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