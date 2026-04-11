using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("T_OR_FAVORITO")]
public class Favorito
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_favorito")]
    public int Id { get; set; }

    [Required]
    public int IdUsuario { get; set; }

    [Required]
    public int IdTrilhaCarreira { get; set; }

    [ForeignKey("IdUsuario")]
    public Usuario? Usuario { get; set; }

    [ForeignKey("IdTrilhaCarreira")]
    public TrilhaCarreira? TrilhaCarreira { get; set; }
}