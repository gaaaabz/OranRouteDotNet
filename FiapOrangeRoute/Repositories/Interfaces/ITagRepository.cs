using FiapOrangeRoute.Helpers;

namespace FiapOrangeRoute.Repositories.Interfaces;

public interface ITagRepository
{
    Task<PagedResult<Tag>> GetPagedAsync(
        PaginationParams paginationParams);

    Task<IEnumerable<Tag>> GetAllAsync();

    Task<Tag?> GetByIdAsync(int id);

    Task<Tag> CreateAsync(Tag tag);

    Task UpdateAsync(Tag tag);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}