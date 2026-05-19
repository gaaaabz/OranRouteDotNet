using System.ComponentModel.DataAnnotations;

namespace FiapOrangeRoute.DTOs.Tag;

public class TagCreateDTO
{
    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;
}