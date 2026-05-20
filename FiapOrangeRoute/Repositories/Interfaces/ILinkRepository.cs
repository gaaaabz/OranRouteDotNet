public interface ILinkRepository
{
    Task<IEnumerable<Link>> GetAllAsync();

    Task<Link?> GetByIdAsync(int id);

    Task<Link> CreateAsync(Link link);

    Task UpdateAsync(Link link);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}