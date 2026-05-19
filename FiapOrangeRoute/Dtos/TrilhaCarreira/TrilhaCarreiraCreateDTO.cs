using System.ComponentModel.DataAnnotations;

namespace FiapOrangeRoute.DTOs.TrilhaCarreira;

public class TrilhaCarreiraCreateDTO
{
    [Required]
    [StringLength(150)]
    public string Titulo { get; set; } = string.Empty;

    public string? Conteudo { get; set; }
}