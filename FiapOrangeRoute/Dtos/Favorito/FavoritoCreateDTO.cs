using System.ComponentModel.DataAnnotations;

namespace FiapOrangeRoute.DTOs.Favorito;

public class FavoritoCreateDTO
{
    [Required]
    public int IdUsuario { get; set; }

    [Required]
    public int IdTrilhaCarreira { get; set; }
}