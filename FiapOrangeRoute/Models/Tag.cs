using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("T_OR_TAG")]
public class Tag
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_tag")]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Column("nm_tag")]
    public string Nome { get; set; } = string.Empty;

    public ICollection<TagCarreira>? TagCarreiras { get; set; }
}