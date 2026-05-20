using FiapOrangeRoute.DTOs.Link;

namespace FiapOrangeRoute.Services.Interfaces;

public interface ILinkService
{
    Task<IEnumerable<LinkResponseDTO>> GetAllAsync();

    Task<LinkResponseDTO?> GetByIdAsync(int id);

    Task<LinkResponseDTO> CreateAsync(LinkCreateDTO dto);

    Task<bool> UpdateAsync(int id, LinkUpdateDTO dto);

    Task<bool> DeleteAsync(int id);
}