using Aplication.Models.Grid;

namespace Aplication.Models.Request.Projeto;

public class ProjetoGridRequest : BaseGridRequest
{
    public int IdUsuario { get; set; }
    public bool OnlyAbertos { get; set; }
    public bool OnlyAdmin { get; set; }
}