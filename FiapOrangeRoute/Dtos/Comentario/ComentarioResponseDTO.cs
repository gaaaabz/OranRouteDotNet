namespace FiapOrangeRoute.DTOs.Comentario;

public class ComentarioResponseDTO
{
    public int Id { get; set; }

    public string Conteudo { get; set; } = string.Empty;

    public string? Ativo { get; set; }

    public int IdUsuario { get; set; }

    public string? UsuarioNome { get; set; }

    public int IdTrilhaCarreira { get; set; }

    public string? TrilhaTitulo { get; set; }
}