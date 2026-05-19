using System.ComponentModel.DataAnnotations;

namespace FiapOrangeRoute.DTOs.TipoUsuario;

public class TipoUsuarioUpdateDTO
{
    [Required]
    [StringLength(50)]
    public string Nome { get; set; } = string.Empty;
}