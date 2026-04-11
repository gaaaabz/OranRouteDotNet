using FiapOrangeRoute.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("T_OR_USUARIO")]
public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_usuario")]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Column("nm_usuario")]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(150)]
    public string Senha { get; set; } = string.Empty;

    public byte[]? Foto { get; set; }

    [Column("at_usuario")]
    [StringLength(1)]
    public string? Ativo { get; set; }

    [Required]
    [Column("id_tipo_usuario")]
    public int TipoUsuarioId { get; set; }

    [ForeignKey("TipoUsuarioId")]
    public TipoUsuario? TipoUsuario { get; set; }

    public ICollection<Comentario>? Comentarios { get; set; }
    public ICollection<Favorito>? Favoritos { get; set; }
}