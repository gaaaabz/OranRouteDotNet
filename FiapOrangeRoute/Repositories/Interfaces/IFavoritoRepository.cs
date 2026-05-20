public interface IFavoritoRepository
{
    Task<IEnumerable<Favorito>> GetAllAsync();

    Task<Favorito?> GetByIdAsync(int id);

    Task<Favorito> CreateAsync(Favorito favorito);

    Task UpdateAsync(Favorito favorito);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}