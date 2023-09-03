using System.ComponentModel;

namespace Infraestrutura.Enum;

public enum EStatusProjeto
{
    [Description("Aberto")] 
    Aberto = 0,
    [Description("Cancelado")] 
    Cancelado = 1,
    [Description("Concluido")] 
    Concluido = 2,
    [Description("Atrasado")] 
    Atrasado = 3
}