using FiapOrangeRoute.Helpers;

namespace FiapOrangeRoute.Repositories.Interfaces;

public interface ITagCarreiraRepository
{
    Task<PagedResult<TagCarreira>> GetPagedAsync(
        PaginationParams paginationParams);

    Task<IEnumerable<TagCarreira>> GetAllAsync();

    Task<TagCarreira?> GetByIdAsync(int id);

    Task<TagCarreira> CreateAsync(TagCarreira tagCarreira);

    Task UpdateAsync(TagCarreira tagCarreira);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}