using System.ComponentModel;

namespace Infraestrutura.Enum;

public enum EPerfil
{
    [Description("Administrador")]
    Administrador = 0,
    [Description("Comum")]
    Comum = 1,
}