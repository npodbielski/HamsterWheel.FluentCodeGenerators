using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Members;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class ClassDefinitionContext
{
    public static ClassDefinitionRecord BuildDefinition(CodeFileChunks fileChunks, PascalCaseName className) =>
        new(fileChunks.Namespace is not null ? NameInNamespace.From(className.ToString(), fileChunks.Namespace.ToString()) : null, className.ToString());
}
