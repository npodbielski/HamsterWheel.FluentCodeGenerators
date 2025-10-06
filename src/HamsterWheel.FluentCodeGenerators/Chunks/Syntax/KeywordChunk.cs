using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Body;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class KeywordChunk(string keyword, bool addSpaceAfter = true) : BodyChunk
{
    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        stringBuilder.Append(keyword);
        if (addSpaceAfter)
        {
            stringBuilder.AppendSingleSpace();
        }

        return true;
    }
}

public class UsingKeywordChunk() : KeywordChunk(Keyword)
{
    public const string Keyword = "using";
}

public class StaticKeywordChunk() : KeywordChunk(Keyword)
{
    public const string Keyword = "static";
}

public class AsyncKeywordChunk() : KeywordChunk(Keyword)
{
    public const string Keyword = "async";
}

public class OverrideKeywordChunk() : KeywordChunk(Keyword)
{
    public const string Keyword = "override";
}

public class VirtualKeywordChunk() : KeywordChunk(Keyword)
{
    public const string Keyword = "virtual";
}

public class SealedKeywordChunk() : KeywordChunk(Keyword)
{
    public const string Keyword = "sealed";
}

public class AbstractKeywordChunk() : KeywordChunk(Keyword)
{
    public const string Keyword = "abstract";
}

public class ClassKeywordChunk() : KeywordChunk(Keyword)
{
    public const string Keyword = "class";
}

public class EnumKeywordChunk() : KeywordChunk(Keyword)
{
    public const string Keyword = "enum";
}

public class NewKeywordChunk() : KeywordChunk(Keyword, false)
{
    public const string Keyword = "new";
}

public class PartialKeywordChunk() : KeywordChunk(Keyword)
{
    public const string Keyword = "partial";
}

public class ReturnKeywordChunk() : KeywordChunk(Keyword)
{
    public const string Keyword = "return";
}

public class DefaultKeywordChunk() : KeywordChunk(Keyword, false)
{
    public const string Keyword = "default";
}

public class NameOfKeywordChunk() : KeywordChunk(Keyword, false)
{
    public const string Keyword = "nameof";
}

public class TypeOfKeywordChunk() : KeywordChunk(Keyword, false)
{
    public const string Keyword = "typeof";
}

public class NullableKeywordChunk() : KeywordChunk(Keyword, false)
{
    public const string Keyword = "nullable";
}

public class DisableKeywordChunk() : KeywordChunk(Keyword, false)
{
    public const string Keyword = "disable";
}

public class EnableKeywordChunk() : KeywordChunk(Keyword, false)
{
    public const string Keyword = "enable";
}

public class ThisKeywordChunk() : KeywordChunk(Keyword, false)
{
    public const string Keyword = "this";
}

public class BaseKeywordChunk() : KeywordChunk(Keyword, false)
{
    public const string Keyword = "base";
}