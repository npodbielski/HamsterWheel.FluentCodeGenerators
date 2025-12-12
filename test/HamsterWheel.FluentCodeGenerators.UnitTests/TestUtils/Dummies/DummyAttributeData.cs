using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;

public class DummyAttributeData(string name) : AttributeData
{
    public INamedTypeSymbol? Class { get; set; } = new DummyNamedTypeSymbol(name, null);
    protected override INamedTypeSymbol? CommonAttributeClass => Class;
    protected override IMethodSymbol? CommonAttributeConstructor { get; }
    protected override SyntaxReference? CommonApplicationSyntaxReference { get; }
    protected override ImmutableArray<TypedConstant> CommonConstructorArguments { get; }
    protected override ImmutableArray<KeyValuePair<string, TypedConstant>> CommonNamedArguments { get; }
}