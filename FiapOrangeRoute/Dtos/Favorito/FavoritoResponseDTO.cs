namespace FiapOrangeRoute.DTOs.Favorito;

public class FavoritoResponseDTO
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public string? UsuarioNome { get; set; }

    public int IdTrilhaCarreira { get; set; }

    public string? TrilhaTitulo { get; set; }
}