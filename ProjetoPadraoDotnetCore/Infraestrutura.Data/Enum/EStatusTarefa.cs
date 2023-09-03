using System.ComponentModel;

namespace Infraestrutura.Enum;

public enum EStatusTarefa
{
    [Description("Aguardando")] 
    Aguardando = 0,
    [Description("Progresso")] 
    Progresso = 1,
    [Description("Completo")] 
    Completo = 2
}