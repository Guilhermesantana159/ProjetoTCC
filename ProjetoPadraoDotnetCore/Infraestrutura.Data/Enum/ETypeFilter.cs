using System.ComponentModel;

namespace Infraestrutura.Enum;

public enum ETypeFilter
{
    [Description("Contains")]
    Contains = 0,
    [Description("Equals")]
    Equals = 1,
    [Description("Greater")]
    Greater = 1,
}