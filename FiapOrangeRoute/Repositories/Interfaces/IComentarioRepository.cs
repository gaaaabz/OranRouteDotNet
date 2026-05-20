public interface IComentarioRepository
{
    Task<IEnumerable<Comentario>> GetAllAsync();

    Task<Comentario?> GetByIdAsync(int id);

    Task<Comentario> CreateAsync(Comentario comentario);

    Task UpdateAsync(Comentario comentario);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}