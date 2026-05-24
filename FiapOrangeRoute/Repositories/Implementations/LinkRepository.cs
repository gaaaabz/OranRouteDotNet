using FiapOrangeRoute.Data;
using FiapOrangeRoute.Helpers;
using FiapOrangeRoute.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiapOrangeRoute.Repositories.Implementations;

public class LinkRepository : ILinkRepository
{
    private readonly AppDbContext _context;

    public LinkRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Link>> GetPagedAsync(
        PaginationParams paginationParams)
    {
        var query = _context.Links
            .Include(l => l.TrilhaCarreira)
            .AsQueryable();

        if (!string.IsNullOrEmpty(paginationParams.Search))
        {
            query = query.Where(l =>
                l.Titulo.Contains(paginationParams.Search));
        }

        query = paginationParams.SortDirection?.ToLower() == "desc"
            ? query.OrderByDescending(l => l.Titulo)
            : query.OrderBy(l => l.Titulo);

        var totalItems = await query.CountAsync();

        var items = await query
            .Skip((paginationParams.PageNumber - 1)
                * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();

        return new PagedResult<Link>
        {
            Items = items,
            TotalItems = totalItems,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize
        };
    }

    public async Task<IEnumerable<Link>> GetAllAsync()
    {
        return await _context.Links
            .Include(l => l.TrilhaCarreira)
            .ToListAsync();
    }

    public async Task<Link?> GetByIdAsync(int id)
    {
        return await _context.Links
            .Include(l => l.TrilhaCarreira)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<Link> CreateAsync(Link link)
    {
        _context.Links.Add(link);

        await _context.SaveChangesAsync();

        return link;
    }

    public async Task UpdateAsync(Link link)
    {
        _context.Links.Update(link);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var link = await _context.Links.FindAsync(id);

        if (link != null)
        {
            _context.Links.Remove(link);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Links
            .AnyAsync(l => l.Id == id);
    }
}