using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Members;

public record MethodDefinitionRecord(
    NameInNamespace? DeclaringType,
    string Name,
    EquatableArray<ParameterDefinition> Parameters);
