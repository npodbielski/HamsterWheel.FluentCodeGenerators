using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Base;

public class AttributesWithVisibilityChunk : AttributesSetChunk
{
    private MemberVisibility _visibility = MemberVisibility.Public;
    public void SetVisibility(MemberVisibility value) => _visibility = value;

    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        base.AppendChunks(stringBuilder);
        stringBuilder.AppendLine();
        stringBuilder.Append(new VisibilityKeywordCodeChunk(_visibility));

        return true;
    }
}