using System.ComponentModel.DataAnnotations;

namespace FiapOrangeRoute.DTOs.Usuario;

public class UsuarioUpdateDTO
{
    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(150)]
    public string Senha { get; set; } = string.Empty;

    public byte[]? Foto { get; set; }

    [StringLength(1)]
    public string? Ativo { get; set; }

    [Required]
    public int TipoUsuarioId { get; set; }
}