using FiapOrangeRoute.Data;
using FiapOrangeRoute.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiapOrangeRoute.Repositories.Implementations;

public class ComentarioRepository : IComentarioRepository
{
    private readonly AppDbContext _context;

    public ComentarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Comentario>> GetAllAsync()
    {
        return await _context.Comentarios
            .Include(c => c.Usuario)
            .Include(c => c.TrilhaCarreira)
            .ToListAsync();
    }

    public async Task<Comentario?> GetByIdAsync(int id)
    {
        return await _context.Comentarios
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Comentario> CreateAsync(Comentario comentario)
    {
        _context.Comentarios.Add(comentario);

        await _context.SaveChangesAsync();

        return comentario;
    }

    public async Task UpdateAsync(Comentario comentario)
    {
        _context.Comentarios.Update(comentario);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var comentario = await _context.Comentarios.FindAsync(id);

        if (comentario != null)
        {
            _context.Comentarios.Remove(comentario);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Comentarios.AnyAsync(c => c.Id == id);
    }
}