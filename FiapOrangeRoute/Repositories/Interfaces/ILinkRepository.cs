using FiapOrangeRoute.Helpers;

namespace FiapOrangeRoute.Repositories.Interfaces;

public interface ILinkRepository
{
    Task<PagedResult<Link>> GetPagedAsync(
        PaginationParams paginationParams);

    Task<IEnumerable<Link>> GetAllAsync();

    Task<Link?> GetByIdAsync(int id);

    Task<Link> CreateAsync(Link link);

    Task UpdateAsync(Link link);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}