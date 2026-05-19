using System.ComponentModel.DataAnnotations;

namespace FiapOrangeRoute.DTOs.Comentario;

public class ComentarioCreateDTO
{
    [Required]
    public string Conteudo { get; set; } = string.Empty;

    [Required]
    public int IdUsuario { get; set; }

    [Required]
    public int IdTrilhaCarreira { get; set; }
}