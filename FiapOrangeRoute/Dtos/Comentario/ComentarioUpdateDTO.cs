using System.ComponentModel.DataAnnotations;

namespace FiapOrangeRoute.DTOs.Comentario;

public class ComentarioUpdateDTO
{
    [Required]
    public string Conteudo { get; set; } = string.Empty;

    [StringLength(1)]
    public string? Ativo { get; set; }
}