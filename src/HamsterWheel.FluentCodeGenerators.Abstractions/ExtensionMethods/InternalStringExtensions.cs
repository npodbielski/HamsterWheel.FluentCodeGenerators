using System.Diagnostics.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators;

internal static class InternalStringExtensions
{
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str) => string.IsNullOrWhiteSpace(str);

    extension(string? str)
    {
        public string ToSingular()
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

        public string ToPlural()
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

        public string ToPascalCase()
        {
            if (str is null || str.Length <= 0)
            {
                return str ?? "";
            }

            var firstLetter = str[0].ToString();
            return $"{firstLetter.ToUpper()}{str[1..]}";
        }

        public string ToCamelCase()
        {
            if (str is null || str.Length <= 0)
            {
                return str ?? "";
            }

            var firstLetter = str[0].ToString();
            return $"{firstLetter.ToLower()}{str[1..]}";
        }

        public string ToScreamingSnakeCase()
        {
            if (str.IsNullOrWhiteSpace())
            {
                return str ?? "";
            }

            // Split on camelCase boundaries and uppercase with underscores
            var result = new System.Text.StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                if (char.IsUpper(str[i]))
                {
                    if (i > 0)
                        result.Append('_');
                    result.Append(char.ToUpperInvariant(str[i]));
                }
                else if (char.IsLetterOrDigit(str[i]))
                {
                    result.Append(char.ToUpperInvariant(str[i]));
                }
            }

            return result.ToString();
        }
    }
}