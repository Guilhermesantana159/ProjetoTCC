namespace Aplication.Models.Response.Menu;

public class EstrututuraMenuResponse
{
    public List<ItemMenu> Menu { get; set; }
}

public class ItemMenu
{
    public int Id { get; set; }
    public string? Label { get; set; }
    public string? Icon { get; set; }
    public string? Link { get; set; }
    public int ParentId { get; set; }
    public bool IsTitle { get; set; }
    public bool IsLayout { get; set; }
    public List<ItemMenu>? SubItems { get; set; }
    public Badgde? Badgde { get; set; }
    public string? Collapseid { get; set; }
}

public class Badgde
{
    public string? Variant { get; set; }
    public string? Text { get; set; }
}


