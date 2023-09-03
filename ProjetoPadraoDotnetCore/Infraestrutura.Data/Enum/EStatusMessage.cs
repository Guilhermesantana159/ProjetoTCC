using System.ComponentModel;

namespace Infraestrutura.Enum;

public enum EStatusMessage
{
    [Description("Normal")]
    Normal = 0,
    [Description("Deletada")]
    Deletada = 1
}