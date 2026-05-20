public interface ITagCarreiraRepository
{
    Task<IEnumerable<TagCarreira>> GetAllAsync();

    Task<TagCarreira?> GetByIdAsync(int id);

    Task<TagCarreira> CreateAsync(TagCarreira tagCarreira);

    Task UpdateAsync(TagCarreira tagCarreira);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}