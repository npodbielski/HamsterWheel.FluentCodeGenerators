using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Base;

public abstract class AttributesSetChunk(bool inline = true) : ICodeChunk
{
    protected AttributesDefinitionChunk Attributes { get; } = new(inline);

    public virtual bool AppendChunks(StringBuilder stringBuilder) => Attributes.AppendChunks(stringBuilder);

    public void AddAttribute(AttributeDefinitionChunk newAttrs) => Attributes.Add(newAttrs);
}