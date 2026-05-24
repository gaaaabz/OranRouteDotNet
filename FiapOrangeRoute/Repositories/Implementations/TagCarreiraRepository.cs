// Repositories/Implementations/TagCarreiraRepository.cs

using FiapOrangeRoute.Data;
using FiapOrangeRoute.Helpers;
using FiapOrangeRoute.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiapOrangeRoute.Repositories.Implementations;

public class TagCarreiraRepository : ITagCarreiraRepository
{
    private readonly AppDbContext _context;

    public TagCarreiraRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<TagCarreira>> GetPagedAsync(
        PaginationParams paginationParams)
    {
        var query = _context.TagsCarreira
            .Include(t => t.Tag)
            .Include(t => t.TrilhaCarreira)
            .AsQueryable();

        query = paginationParams.SortDirection?.ToLower() == "desc"
            ? query.OrderByDescending(t => t.Id)
            : query.OrderBy(t => t.Id);

        var totalItems = await query.CountAsync();

        var items = await query
            .Skip((paginationParams.PageNumber - 1)
                * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();

        return new PagedResult<TagCarreira>
        {
            Items = items,
            TotalItems = totalItems,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize
        };
    }

    public async Task<IEnumerable<TagCarreira>> GetAllAsync()
    {
        return await _context.TagsCarreira
            .Include(t => t.Tag)
            .Include(t => t.TrilhaCarreira)
            .ToListAsync();
    }

    public async Task<TagCarreira?> GetByIdAsync(int id)
    {
        return await _context.TagsCarreira
            .Include(t => t.Tag)
            .Include(t => t.TrilhaCarreira)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TagCarreira> CreateAsync(
        TagCarreira tagCarreira)
    {
        _context.TagsCarreira.Add(tagCarreira);

        await _context.SaveChangesAsync();

        return tagCarreira;
    }

    public async Task UpdateAsync(TagCarreira tagCarreira)
    {
        _context.TagsCarreira.Update(tagCarreira);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tagCarreira = await _context.TagsCarreira
            .FindAsync(id);

        if (tagCarreira != null)
        {
            _context.TagsCarreira.Remove(tagCarreira);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.TagsCarreira
            .AnyAsync(t => t.Id == id);
    }
}