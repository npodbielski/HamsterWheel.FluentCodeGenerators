using System.Text;
using HamsterWheel.FluentCodeGenerators.Exceptions;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class ParameterValueChunk(ICodeChunk valueChunk)
    : ICodeChunk
{
    private string? _name;
    private string _separator = ": ";
    private ICodeChunk _valueChunk = valueChunk;

    public string Name => _name ?? throw new NotInitializedChunkException();

    public void AddName(string name, string? separator = null)
    {
        _name = name;
        _separator = separator ?? ": ";
    }

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        var added = false;
        if (_name is not null)
        {
            stringBuilder.Append(_name);
            stringBuilder.Append(_separator);
            added = true;
        }

        added |= _valueChunk.AppendChunks(stringBuilder);

        return added;
    }

    public void SetValue(ICodeChunk newValueChunk) => _valueChunk = newValueChunk;

    public void SetValue(object newValue) => _valueChunk = PlainValueChunk.FromObject(newValue);

    public static ParameterValueChunk From(object value) => From(PlainValueChunk.FromObject(value));

    public static ParameterValueChunk From(ICodeChunk value) => new(value);
}