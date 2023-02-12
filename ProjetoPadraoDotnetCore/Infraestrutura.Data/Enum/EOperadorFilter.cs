using System.ComponentModel;

namespace Infraestrutura.Enum;

public enum EOperadorFilter
{
    [Description("Contains")]
    Contains = 0,
    [Description("GreaterThen")]
    GreaterThen = 1,
    [Description("LessThen")]
    LessThen = 2,
    [Description("Equals")]
    Equals = 3
}