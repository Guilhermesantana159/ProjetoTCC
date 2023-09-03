namespace Aplication.Models.Response.Menu;

public class AutoCompleteMenuResponse
{
    public List<PageMenu> Pages { get; set; }
}
public class PageMenu
{
    public string? Nome { get; set; }
    public string? Url { get; set; }
}


