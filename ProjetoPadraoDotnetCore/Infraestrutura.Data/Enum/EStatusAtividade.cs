using System.ComponentModel;

namespace Infraestrutura.Enum;

public enum EStatusAtividade
{
    [Description("Progresso")] 
    Progresso = 0,
    [Description("Completo")] 
    Completo = 1,
    //Nao usar para save
    [Description("Atrasado")] 
    Atrasado = 2
}