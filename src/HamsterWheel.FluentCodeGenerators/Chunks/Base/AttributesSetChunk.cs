using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Base;

public abstract class AttributesSetChunk(bool inline = true) : ICodeChunk
{
    private readonly AttributesDefinitionChunk _attributes = new(inline);

    public virtual bool AppendChunks(StringBuilder stringBuilder) => _attributes.AppendChunks(stringBuilder);

    public void AddAttribute(AttributeDefinitionChunk newAttrs) => _attributes.Add(newAttrs);
}