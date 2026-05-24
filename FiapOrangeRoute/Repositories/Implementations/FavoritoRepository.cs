// Repositories/Implementations/FavoritoRepository.cs

using FiapOrangeRoute.Data;
using FiapOrangeRoute.Helpers;
using FiapOrangeRoute.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiapOrangeRoute.Repositories.Implementations;

public class FavoritoRepository : IFavoritoRepository
{
    private readonly AppDbContext _context;

    public FavoritoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Favorito>> GetPagedAsync(
        PaginationParams paginationParams)
    {
        var query = _context.Favoritos
            .Include(f => f.Usuario)
            .Include(f => f.TrilhaCarreira)
            .AsQueryable();

        query = paginationParams.SortDirection?.ToLower() == "desc"
            ? query.OrderByDescending(f => f.Id)
            : query.OrderBy(f => f.Id);

        var totalItems = await query.CountAsync();

        var items = await query
            .Skip((paginationParams.PageNumber - 1)
                * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();

        return new PagedResult<Favorito>
        {
            Items = items,
            TotalItems = totalItems,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize
        };
    }

    public async Task<IEnumerable<Favorito>> GetAllAsync()
    {
        return await _context.Favoritos
            .Include(f => f.Usuario)
            .Include(f => f.TrilhaCarreira)
            .ToListAsync();
    }

    public async Task<Favorito?> GetByIdAsync(int id)
    {
        return await _context.Favoritos
            .Include(f => f.Usuario)
            .Include(f => f.TrilhaCarreira)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Favorito> CreateAsync(Favorito favorito)
    {
        _context.Favoritos.Add(favorito);

        await _context.SaveChangesAsync();

        return favorito;
    }

    public async Task UpdateAsync(Favorito favorito)
    {
        _context.Favoritos.Update(favorito);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var favorito = await _context.Favoritos.FindAsync(id);

        if (favorito != null)
        {
            _context.Favoritos.Remove(favorito);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Favoritos
            .AnyAsync(f => f.Id == id);
    }
}