using FiapOrangeRoute.Data;
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

    public async Task<IEnumerable<Link>> GetAllAsync()
    {
        return await _context.Links
            .Include(l => l.TrilhaCarreira)
            .ToListAsync();
    }

    public async Task<Link?> GetByIdAsync(int id)
    {
        return await _context.Links
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
        return await _context.Links.AnyAsync(l => l.Id == id);
    }
}