namespace Aplication.Models.Request.ModuloMenu;

public class MenuRequest
{
    public int? IdMenu { get; set; }
    public string? Nome { get; set; }
    public string? Link { get; set; }
    public int IdSubModulo { get; set; }
    public bool OnlyAdmin { get; set; }
}