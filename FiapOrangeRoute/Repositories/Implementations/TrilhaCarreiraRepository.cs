using FiapOrangeRoute.Data;
using FiapOrangeRoute.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiapOrangeRoute.Repositories.Implementations;

public class TrilhaCarreiraRepository : ITrilhaCarreiraRepository
{
    private readonly AppDbContext _context;

    public TrilhaCarreiraRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TrilhaCarreira>> GetAllAsync()
    {
        return await _context.TrilhasCarreira
            .Include(t => t.Links)
            .ToListAsync();
    }

    public async Task<TrilhaCarreira?> GetByIdAsync(int id)
    {
        return await _context.TrilhasCarreira
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TrilhaCarreira> CreateAsync(TrilhaCarreira trilha)
    {
        _context.TrilhasCarreira.Add(trilha);

        await _context.SaveChangesAsync();

        return trilha;
    }

    public async Task UpdateAsync(TrilhaCarreira trilha)
    {
        _context.TrilhasCarreira.Update(trilha);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var trilha = await _context.TrilhasCarreira.FindAsync(id);

        if (trilha != null)
        {
            _context.TrilhasCarreira.Remove(trilha);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.TrilhasCarreira.AnyAsync(t => t.Id == id);
    }
}