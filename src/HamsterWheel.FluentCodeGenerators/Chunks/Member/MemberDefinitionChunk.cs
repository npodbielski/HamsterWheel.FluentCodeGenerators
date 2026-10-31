using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Base;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Member;

public abstract class MemberDefinitionChunk : AttributesWithVisibilityChunk
{
    internal MemberDefinitionChunk(string name) : base(true) => Name = name;

    internal string Name { get; private set; }

    private bool ShouldEmit { get; set; } = true;

    public void ReplaceName(string name) => Name = name;

    public void SuppressEmit() => ShouldEmit = false;

    public sealed override bool AppendChunks(StringBuilder stringBuilder)
    {
        if (!ShouldEmit)
            return false;

        return RenderComment(stringBuilder) | RenderAttributesAndVisibility(stringBuilder) |
               RenderMember(stringBuilder);
    }

    protected virtual bool RenderAttributesAndVisibility(StringBuilder stringBuilder) => base.AppendChunks(stringBuilder);

    protected virtual bool RenderComment(StringBuilder stringBuilder) => false;

    protected abstract bool RenderMember(StringBuilder stringBuilder);
}