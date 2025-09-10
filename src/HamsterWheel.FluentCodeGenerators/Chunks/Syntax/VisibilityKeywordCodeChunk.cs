namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class VisibilityKeywordCodeChunk(MemberVisibility visibility) : KeywordChunk(MapVisibility(visibility))
{
    private static string MapVisibility(MemberVisibility visibility)
    {
        return (visibility switch
        {
            MemberVisibility.Public => "public",
            MemberVisibility.Private => "private",
            MemberVisibility.Protected => "protected",
            MemberVisibility.Internal => "internal",
            MemberVisibility.ProtectedInternal => "protected internal",
            MemberVisibility.PrivateProtected => "protected private",
            _ => throw new ArgumentOutOfRangeException()
        });
    }
}