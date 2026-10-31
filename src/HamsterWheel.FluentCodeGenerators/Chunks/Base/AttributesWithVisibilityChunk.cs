using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Base;

public class AttributesWithVisibilityChunk(bool attributesInline = false) : AttributesSetChunk(attributesInline)
{
    protected MemberVisibility Visibility { get; private set; } = MemberVisibility.Public;

    public void SetVisibility(MemberVisibility value) => Visibility = value;

    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        if (base.AppendChunks(stringBuilder))
        {
            stringBuilder.AppendLine();
        }

        stringBuilder.Append(new VisibilityKeywordCodeChunk(Visibility));

        return true;
    }
}