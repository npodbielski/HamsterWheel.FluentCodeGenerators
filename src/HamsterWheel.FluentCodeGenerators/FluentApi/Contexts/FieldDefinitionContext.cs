using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Members;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class FieldDefinitionContext
{
    public static MemberDefinitionRecord BuildDefinition(CodeFileChunks fileChunks, FieldDefinitionChunk chunk)
    {
        return new MemberDefinitionRecord(
            NameInNamespace.From(fileChunks.Namespace?.ToString() ?? "", ""),
            chunk.Name);
    }
}
