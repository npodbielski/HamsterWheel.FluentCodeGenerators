using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Members;

public record ClassDefinitionRecord(
    NameInNamespace? DeclaringType,
    string Name);
