using FiapOrangeRoute.Data;
using FiapOrangeRoute.Helpers;
using FiapOrangeRoute.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiapOrangeRoute.Repositories.Implementations;

public class TagRepository : ITagRepository
{
    private readonly AppDbContext _context;

    public TagRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Tag>> GetPagedAsync(
        PaginationParams paginationParams)
    {
        var query = _context.Tags.AsQueryable();

        if (!string.IsNullOrEmpty(paginationParams.Search))
        {
            query = query.Where(t =>
                t.Nome.Contains(paginationParams.Search));
        }

        query = paginationParams.SortDirection?.ToLower() == "desc"
            ? query.OrderByDescending(t => t.Nome)
            : query.OrderBy(t => t.Nome);

        var totalItems = await query.CountAsync();

        var items = await query
            .Skip((paginationParams.PageNumber - 1)
                * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();

        return new PagedResult<Tag>
        {
            Items = items,
            TotalItems = totalItems,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize
        };
    }

    public async Task<IEnumerable<Tag>> GetAllAsync()
    {
        return await _context.Tags.ToListAsync();
    }

    public async Task<Tag?> GetByIdAsync(int id)
    {
        return await _context.Tags
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Tag> CreateAsync(Tag tag)
    {
        _context.Tags.Add(tag);

        await _context.SaveChangesAsync();

        return tag;
    }

    public async Task UpdateAsync(Tag tag)
    {
        _context.Tags.Update(tag);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tag = await _context.Tags.FindAsync(id);

        if (tag != null)
        {
            _context.Tags.Remove(tag);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Tags
            .AnyAsync(t => t.Id == id);
    }
}