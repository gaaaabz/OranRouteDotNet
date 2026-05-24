namespace FiapOrangeRoute.HATEOAS;

public class HateoasResponse<T>
{
    public T Data { get; set; }

    public List<LinkDTO> Links { get; set; }
        = new();
}