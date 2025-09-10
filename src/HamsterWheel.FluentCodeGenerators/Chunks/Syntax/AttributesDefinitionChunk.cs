using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class AttributesDefinitionChunk(bool inline = false) : ICodeChunk
{
    private readonly List<AttributeDefinitionChunk> _attributes = [];

    public void Add(AttributeDefinitionChunk chunk) => _attributes.Add(chunk);

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        if (!_attributes.Any())
        {
            return false;
        }

        if (inline)
        {
            new DelimitedJoinerChunk(Delimiter.Empty, _attributes).AppendChunks(stringBuilder);
        }
        else
        {
            new NewLineDelimitedJoinChunk(codeChunks: _attributes).AppendChunks(stringBuilder);
        }

        return true;
    }
}