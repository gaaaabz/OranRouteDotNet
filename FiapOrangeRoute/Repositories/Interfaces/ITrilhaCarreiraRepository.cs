using FiapOrangeRoute.Helpers;

public interface ITrilhaCarreiraRepository
{
    Task<PagedResult<TrilhaCarreira>> GetPagedAsync(
        PaginationParams paginationParams);

    Task<IEnumerable<TrilhaCarreira>> GetAllAsync();

    Task<TrilhaCarreira?> GetByIdAsync(int id);

    Task<TrilhaCarreira> CreateAsync(TrilhaCarreira trilha);

    Task UpdateAsync(TrilhaCarreira trilha);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}