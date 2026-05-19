namespace FiapOrangeRoute.DTOs.Usuario;

public class UsuarioResponseDTO
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? Ativo { get; set; }

    public int TipoUsuarioId { get; set; }

    public string? TipoUsuarioNome { get; set; }
}