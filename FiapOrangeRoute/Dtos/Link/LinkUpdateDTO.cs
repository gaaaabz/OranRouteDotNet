using System.ComponentModel.DataAnnotations;

namespace FiapOrangeRoute.DTOs.Link;

public class LinkUpdateDTO
{
    [Required]
    [StringLength(150)]
    public string Titulo { get; set; } = string.Empty;

    public string? Conteudo { get; set; }

    [Required]
    public int IdTrilhaCarreira { get; set; }
}