using OpenTelemetry.Trace;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("T_OR_TRILHA_CARREIRA")]
public class TrilhaCarreira
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_trilha_carreira")]
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    [Column("tt_trilha_carreira")]
    public string Titulo { get; set; } = string.Empty;

    [Column("cd_trilha_carreira")]
    public string? Conteudo { get; set; }

    public ICollection<Comentario>? Comentarios { get; set; }
    public ICollection<Favorito>? Favoritos { get; set; }
    public ICollection<TagCarreira>? Tags { get; set; }
    public ICollection<Link>? Links { get; set; }
}