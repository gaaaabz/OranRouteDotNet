namespace FiapOrangeRoute.HATEOAS;

public class HateoasService
{
    public List<LinkDTO> GenerateLinks(
        string controller,
        int id)
    {
        return new List<LinkDTO>
        {
            new LinkDTO
            {
                Href = $"/api/{controller}/{id}",
                Rel = "self",
                Method = "GET"
            },

            new LinkDTO
            {
                Href = $"/api/{controller}/{id}",
                Rel = "update",
                Method = "PUT"
            },

            new LinkDTO
            {
                Href = $"/api/{controller}/{id}",
                Rel = "delete",
                Method = "DELETE"
            }
        };
    }
}