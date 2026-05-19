using System.ComponentModel.DataAnnotations;

namespace FiapOrangeRoute.DTOs.Tag;

public class TagUpdateDTO
{
    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;
}