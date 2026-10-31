using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Members;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class PropertyDefinitionContext
{
    public static MemberDefinitionRecord BuildDefinition(CodeFileChunks fileChunks, PropertyDefinitionChunk chunk)
    {
        return new MemberDefinitionRecord(
            NameInNamespace.From(fileChunks.Namespace?.ToString() ?? "", ""),
            chunk.Name);
    }
}
