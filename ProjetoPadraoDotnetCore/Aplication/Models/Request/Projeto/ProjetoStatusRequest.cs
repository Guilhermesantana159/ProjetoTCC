using Infraestrutura.Enum;

namespace Aplication.Models.Request.Projeto;

public class ProjetoStatusRequest
{
    public int IdProjeto { get; set; }
    public EStatusProjeto Status { get; set; }
}