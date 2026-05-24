// Services/Implementations/LinkService.cs

using FiapOrangeRoute.DTOs.Link;
using FiapOrangeRoute.Helpers;
using FiapOrangeRoute.Models;
using FiapOrangeRoute.Repositories.Interfaces;
using FiapOrangeRoute.Services.Interfaces;

namespace FiapOrangeRoute.Services.Implementations;

public class LinkService : ILinkService
{
    private readonly ILinkRepository _repository;
    private readonly ILogger<LinkService> _logger;

    public LinkService(
        ILinkRepository repository,
        ILogger<LinkService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PagedResult<LinkResponseDTO>>
        GetPagedAsync(PaginationParams paginationParams)
    {
        var pagedResult = await _repository
            .GetPagedAsync(paginationParams);

        return new PagedResult<LinkResponseDTO>
        {
            Items = pagedResult.Items.Select(l =>
                new LinkResponseDTO
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Conteudo = l.Conteudo,
                    IdTrilhaCarreira = l.IdTrilhaCarreira,
                    TrilhaTitulo = l.TrilhaCarreira?.Titulo
                }),

            TotalItems = pagedResult.TotalItems,
            PageNumber = pagedResult.PageNumber,
            PageSize = pagedResult.PageSize
        };
    }

    public async Task<IEnumerable<LinkResponseDTO>>
        GetAllAsync()
    {
        var links = await _repository.GetAllAsync();

        return links.Select(l =>
            new LinkResponseDTO
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Conteudo = l.Conteudo,
                IdTrilhaCarreira = l.IdTrilhaCarreira,
                TrilhaTitulo = l.TrilhaCarreira?.Titulo
            });
    }

    public async Task<LinkResponseDTO?>
        GetByIdAsync(int id)
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

    public async Task<LinkResponseDTO>
        CreateAsync(LinkCreateDTO dto)
    {
        var link = new Link
        {
            Titulo = dto.Titulo,
            Conteudo = dto.Conteudo,
            IdTrilhaCarreira = dto.IdTrilhaCarreira
        };

        var created = await _repository.CreateAsync(link);

        _logger.LogInformation(
            "Link criado: {Titulo}",
            created.Titulo);

        return new LinkResponseDTO
        {
            Id = created.Id,
            Titulo = created.Titulo,
            Conteudo = created.Conteudo,
            IdTrilhaCarreira = created.IdTrilhaCarreira
        };
    }

    public async Task<bool> UpdateAsync(
        int id,
        LinkUpdateDTO dto)
    {
        var link = await _repository.GetByIdAsync(id);

        if (link == null)
            return false;

        link.Titulo = dto.Titulo;
        link.Conteudo = dto.Conteudo;
        link.IdTrilhaCarreira = dto.IdTrilhaCarreira;

        await _repository.UpdateAsync(link);

        _logger.LogInformation(
            "Link atualizado: {Id}",
            link.Id);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _repository.ExistsAsync(id);

        if (!exists)
            return false;

        await _repository.DeleteAsync(id);

        _logger.LogInformation(
            "Link removido: {Id}",
            id);

        return true;
    }
}