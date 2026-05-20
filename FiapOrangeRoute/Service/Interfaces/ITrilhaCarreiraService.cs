using FiapOrangeRoute.DTOs.TrilhaCarreira;

namespace FiapOrangeRoute.Services.Interfaces;

public interface ITrilhaCarreiraService
{
    Task<IEnumerable<TrilhaCarreiraResponseDTO>> GetAllAsync();

    Task<TrilhaCarreiraResponseDTO?> GetByIdAsync(int id);

    Task<TrilhaCarreiraResponseDTO> CreateAsync(TrilhaCarreiraCreateDTO dto);

    Task<bool> UpdateAsync(int id, TrilhaCarreiraUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}