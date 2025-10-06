using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class SummaryCommentContext(CodeBuilderContextBase previous, SummaryCommentChunk chunk)
    : CodeBuilderContextBase(previous), ISummaryCommentContext
{
    public ISummaryCommentContext Append(string comment)
    {
        chunk.Append(comment);
        return this;
    }

    public static ISummaryCommentContext From<T>(T previous, SummaryCommentChunk chunk)
        where T : CodeBuilderContextBase => new SummaryCommentContext(previous, chunk);
}