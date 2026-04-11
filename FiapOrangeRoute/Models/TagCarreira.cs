using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("T_OR_TAG_CARREIRA")]
public class TagCarreira
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_tag_carreira")]
    public int Id { get; set; }

    public int IdTrilhaCarreira { get; set; }
    public int IdTag { get; set; }

    [ForeignKey("IdTrilhaCarreira")]
    public TrilhaCarreira? TrilhaCarreira { get; set; }

    [ForeignKey("IdTag")]
    public Tag? Tag { get; set; }
}