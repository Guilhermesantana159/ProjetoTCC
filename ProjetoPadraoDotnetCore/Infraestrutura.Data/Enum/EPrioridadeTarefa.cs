using System.ComponentModel;

namespace Infraestrutura.Enum;

public enum EPrioridadeTarefa
{
    [Description("Não Informada")]
    NãoInformada = 0,
    [Description("Baixa")]
    Baixa = 1,
    [Description("Média")]
    Media = 2,
    [Description("Alta")]
    Alta = 3
}