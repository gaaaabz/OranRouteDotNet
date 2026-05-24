using FiapOrangeRoute.Data;
using FiapOrangeRoute.Helpers;
using FiapOrangeRoute.Models;
using FiapOrangeRoute.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiapOrangeRoute.Repositories.Implementations;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Usuario>> GetPagedAsync(
        PaginationParams paginationParams)
    {
        var query = _context.Usuarios
            .Include(u => u.TipoUsuario)
            .AsQueryable();

        // FILTER
        if (!string.IsNullOrEmpty(paginationParams.Search))
        {
            query = query.Where(u =>
                u.Nome.Contains(paginationParams.Search) ||
                u.Email.Contains(paginationParams.Search));
        }

        // SORT
        query = paginationParams.SortDirection?.ToLower() == "desc"
            ? query.OrderByDescending(u => u.Nome)
            : query.OrderBy(u => u.Nome);

        var totalItems = await query.CountAsync();

        var items = await query
            .Skip((paginationParams.PageNumber - 1)
                * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();

        return new PagedResult<Usuario>
        {
            Items = items,
            TotalItems = totalItems,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize
        };
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios
            .Include(u => u.TipoUsuario)
            .ToListAsync();
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios
            .Include(u => u.TipoUsuario)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios
            .Include(u => u.TipoUsuario)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);

        await _context.SaveChangesAsync();

        return usuario;
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Usuarios
            .AnyAsync(u => u.Id == id);
    }
}