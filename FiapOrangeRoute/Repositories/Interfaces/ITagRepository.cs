public interface ITagRepository
{
    Task<IEnumerable<Tag>> GetAllAsync();

    Task<Tag?> GetByIdAsync(int id);

    Task<Tag> CreateAsync(Tag tag);

    Task UpdateAsync(Tag tag);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}