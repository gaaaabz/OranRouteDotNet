using System.ComponentModel.DataAnnotations;

namespace FiapOrangeRoute.DTOs.TagCarreira;

public class TagCarreiraUpdateDTO
{
    [Required]
    public int IdTag { get; set; }

    [Required]
    public int IdTrilhaCarreira { get; set; }
}