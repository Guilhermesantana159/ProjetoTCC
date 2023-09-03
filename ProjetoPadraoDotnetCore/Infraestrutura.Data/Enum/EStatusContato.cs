using System.ComponentModel;

namespace Infraestrutura.Enum;

public enum StatusContato
{
    [Description("Disponivel")]
    Disponivel = 0,
    [Description("Silenciado")]
    Silenciado = 1,
    [Description("Bloqueado")]
    Bloqueado = 2,
}