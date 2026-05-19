using System.ComponentModel.DataAnnotations;

namespace FiapOrangeRoute.DTOs.Favorito;

public class FavoritoUpdateDTO
{
    [Required]
    public int IdUsuario { get; set; }

    [Required]
    public int IdTrilhaCarreira { get; set; }
}