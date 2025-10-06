using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class PlainValueChunk : ICodeChunk
{
    private readonly StringBuilder? _stringBuilder;
    private readonly string? _value;

    public PlainValueChunk(StringBuilder stringBuilder) => _stringBuilder = stringBuilder;

    public PlainValueChunk([StringSyntax("C#")] string value) => _value = value;

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        if (_value is not null)
        {
            stringBuilder.Append(_value);
            return true;
        }

        if (_stringBuilder is null)
        {
            return false;
        }

        stringBuilder.Append(_stringBuilder);
        return true;
    }

    public static PlainValueChunk FromObject(object value, CultureInfo? defaultCulture = null)
    {
        defaultCulture ??= CultureInfo.InvariantCulture;

        if (value is IConvertible convertible)
        {
            return new PlainValueChunk(convertible.ToString(defaultCulture));
        }

        return new PlainValueChunk(value.ToString());
    }
}