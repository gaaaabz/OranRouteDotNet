using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("T_OR_COMENTARIO")]
public class Comentario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_comentario")]
    public int Id { get; set; }

    [Required]
    [Column("cd_comentario")]
    public string Conteudo { get; set; } = string.Empty;

    [StringLength(1)]
    [Column("at_comentario")]
    public string? Ativo { get; set; }

    [Required]
    public int IdUsuario { get; set; }

    [Required]
    public int IdTrilhaCarreira { get; set; }

    [ForeignKey("IdUsuario")]
    public Usuario? Usuario { get; set; }

    [ForeignKey("IdTrilhaCarreira")]
    public TrilhaCarreira? TrilhaCarreira { get; set; }
}