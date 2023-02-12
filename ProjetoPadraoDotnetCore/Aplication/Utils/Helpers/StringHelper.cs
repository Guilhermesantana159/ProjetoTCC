using System.Globalization;

namespace Aplication.Utils.Helpers;

public static class StringHelper
{
    public static string ToFormatCpf(this string? source)
    {
        if (source == null || source.Length != 11)
            return "";
        
        return Convert.ToUInt64(source).ToString(@"000\.000\.000\-00");
    }
    
    public static string ToFormatCnpj(this string? source)
    {
        if (source == null || source.Length != 14)
            return "";

        return Convert.ToUInt64(source).ToString(@"00\.000\.000\/0000\-00");
    }
    
    public static string FormatMoney(this decimal? source)
    {
        return string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", source);
    }
    
    public static string FormatTelefone(this string? source)
    {
        if (source == null || source.Length != 11)
            return "";
        
        return Convert.ToUInt64(source).ToString(@"\(00\)\ 00000\-0000");
    }
    
    public static string FormatDateBr(this DateTime source)
    {
        return DateTime.Parse(source.ToString("d")).ToString("dd/MM/yyyy");
    }
}