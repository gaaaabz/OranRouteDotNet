using Microsoft.EntityFrameworkCore;
using FiapOrangeRoute.Models;

namespace FiapOrangeRoute.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<TipoUsuario> TiposUsuario { get; set; }
    public DbSet<TrilhaCarreira> TrilhasCarreira { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
    public DbSet<Favorito> Favoritos { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TagCarreira> TagsCarreira { get; set; }
    public DbSet<Link> Links { get; set; }
}