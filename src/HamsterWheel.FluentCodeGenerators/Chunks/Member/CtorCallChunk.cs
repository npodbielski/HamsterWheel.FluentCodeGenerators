using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Member;

public class CtorCallChunk(TypeNameChunk type, ParametersValuesListChunk? parameters, bool withNewKeyword = true)
    : ICodeChunk
{
    public bool AppendChunks(StringBuilder stringBuilder)
    {
        if (withNewKeyword)
        {
            new NewKeywordChunk().AppendChunks(stringBuilder);
        }

        type.AppendChunks(stringBuilder);
        if (withNewKeyword || parameters?.Parameters.Any() == true)
        {
            parameters?.AppendChunks(stringBuilder);
        }

        return true;
    }
}