using System.ComponentModel.DataAnnotations;

namespace FiapOrangeRoute.DTOs.TipoUsuario;

public class TipoUsuarioCreateDTO
{
    [Required]
    [StringLength(50)]
    public string Nome { get; set; } = string.Empty;
}