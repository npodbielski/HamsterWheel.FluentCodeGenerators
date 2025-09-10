using System.Diagnostics.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators;

internal static class StringExtensions
{
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str) => string.IsNullOrWhiteSpace(str);
    
    public static string? ToSingular(this string? str)
    {
        if (str.IsNullOrWhiteSpace())
        {
            return str ?? "";
        }

        if (str.EndsWith("ies"))
        {
            //cur 'ies' and add 'y' so proxies -> proxy
            return str[..^3] + "y";
        }

        if (str.EndsWith("es"))
        {
            //cut only 's' so modules -> module
            return str[..^1];
        }

        return str.EndsWith("s") ? str[..^1] : str;
    }

    public static string ToPlural(this string? str)
    {
        if (str.IsNullOrWhiteSpace())
        {
            return str ?? "";
        }

        if (str.EndsWith("y"))
        {
            return str[..^1] + "ies";
        }

        if (str.EndsWith("s") || str.EndsWith("x"))
        {
            return str + "es";
        }

        return str + "s";
    }

    public static string ToPascalCase(this string? str)
    {
        if (str is null || str.Length <= 0)
        {
            return str ?? "";
        }

        var firstLetter = str[0].ToString();
        return $"{firstLetter.ToUpper()}{str[1..]}";
    }

    public static string ToCamelCase(this string? str)
    {
        if (str is null || str.Length <= 0)
        {
            return str ?? "";
        }

        var firstLetter = str[0].ToString();
        return $"{firstLetter.ToLower()}{str[1..]}";
    }
}