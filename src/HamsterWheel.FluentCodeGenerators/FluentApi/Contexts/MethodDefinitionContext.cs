using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Members;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class MethodDefinitionContext
{
    public static MethodDefinitionRecord BuildDefinition(CodeFileChunks fileChunks, MethodDefinitionChunk chunk, PascalCaseName? className) =>
        new(className is not null ? NameInNamespace.From(className.ToString(), fileChunks.Namespace?.ToString() ?? "") : null, chunk.Name,
            chunk.ParametersList
                .Select(p => new ParameterDefinition(p.Name.ToString()))
                .ToArray());
}