using System.ComponentModel;

namespace Infraestrutura.Enum;

public enum ETipoArquivo
{
    [Description("application/excel")]
    Excel = 1,
    [Description("application/pdf")]
    PDF = 2,
    [Description("application/word")]
    Word = 3
}