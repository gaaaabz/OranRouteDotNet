namespace FiapOrangeRoute.DTOs.Link;

public class LinkResponseDTO
{
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string? Conteudo { get; set; }

    public int IdTrilhaCarreira { get; set; }

    public string? TrilhaTitulo { get; set; }
}