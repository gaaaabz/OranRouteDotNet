namespace FiapOrangeRoute.DTOs.TagCarreira;

public class TagCarreiraResponseDTO
{
    public int Id { get; set; }

    public int IdTag { get; set; }

    public string? TagNome { get; set; }

    public int IdTrilhaCarreira { get; set; }

    public string? TrilhaTitulo { get; set; }
}