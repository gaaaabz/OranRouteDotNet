using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("T_OR_LINK")]
public class Link
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_link")]
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    [Column("tt_link")]
    public string Titulo { get; set; } = string.Empty;

    [Column("cd_link")]
    public string? Conteudo { get; set; }

    public int IdTrilhaCarreira { get; set; }

    [ForeignKey("IdTrilhaCarreira")]
    public TrilhaCarreira? TrilhaCarreira { get; set; }
}