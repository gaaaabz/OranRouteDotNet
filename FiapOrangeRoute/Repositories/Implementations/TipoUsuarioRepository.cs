using FiapOrangeRoute.Data;
using FiapOrangeRoute.Models;
using FiapOrangeRoute.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiapOrangeRoute.Repositories.Implementations;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly AppDbContext _context;

    public TipoUsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TipoUsuario>> GetAllAsync()
    {
        return await _context.TiposUsuario.ToListAsync();
    }

    public async Task<TipoUsuario?> GetByIdAsync(int id)
    {
        return await _context.TiposUsuario
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TipoUsuario> CreateAsync(TipoUsuario tipoUsuario)
    {
        _context.TiposUsuario.Add(tipoUsuario);

        await _context.SaveChangesAsync();

        return tipoUsuario;
    }

    public async Task UpdateAsync(TipoUsuario tipoUsuario)
    {
        _context.TiposUsuario.Update(tipoUsuario);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tipo = await _context.TiposUsuario.FindAsync(id);

        if (tipo != null)
        {
            _context.TiposUsuario.Remove(tipo);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.TiposUsuario.AnyAsync(t => t.Id == id);
    }
}