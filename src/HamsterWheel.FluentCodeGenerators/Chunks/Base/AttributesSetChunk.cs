using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Base;

public abstract class AttributesSetChunk(bool inline = true) : ICodeChunk
{
    // ReSharper disable once MemberCanBePrivate.Global - this is abstract class having this as protected is part of the API
    protected AttributesDefinitionChunk Attributes { get; } = new(inline);

    public virtual bool AppendChunks(StringBuilder stringBuilder) => Attributes.AppendChunks(stringBuilder);

    public void AddAttribute(AttributeDefinitionChunk newAttrs) => Attributes.Add(newAttrs);
}