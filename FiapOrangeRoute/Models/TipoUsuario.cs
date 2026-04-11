using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiapOrangeRoute.Models;

[Table("T_OR_TIPO_USUARIO")]
public class TipoUsuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_tipo_usuario")]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Column("nm_tipo_usuario")]
    public string Nome { get; set; } = string.Empty;

    public ICollection<Usuario>? Usuarios { get; set; }
}