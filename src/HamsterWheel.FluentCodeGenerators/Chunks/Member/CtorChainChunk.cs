using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Member;

public abstract class CtorChainChunk(ICodeChunk chainedCtorKeyword, ParametersValuesListChunk? parameters) : ICodeChunk
{
    public bool AppendChunks(StringBuilder stringBuilder)
    {
        chainedCtorKeyword.AppendChunks(stringBuilder);
        if (parameters != null)
        {
            parameters.AppendChunks(stringBuilder);
        }
        else
        {
            stringBuilder.Append("()");
        }

        return true;
    }
}