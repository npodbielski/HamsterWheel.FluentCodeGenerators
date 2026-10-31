using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;

public class DummyAttributeData : AttributeData
{
    public DummyAttributeData(string name, IEnumerable<TypedConstant>? constants = null, Dictionary<string, TypedConstant>? namedArgs = null)
    {
        Class = new DummyNamedTypeSymbol(name, null!);
        if (constants != null)
        {
            CommonConstructorArguments = [.. constants];
        }

        if (namedArgs != null)
        {
            CommonNamedArguments = [.. namedArgs];
        }
    }

    public INamedTypeSymbol? Class { get; set; }
    protected override INamedTypeSymbol? CommonAttributeClass => Class;
    protected override IMethodSymbol? CommonAttributeConstructor { get; }
    protected override SyntaxReference? CommonApplicationSyntaxReference { get; }
    protected override ImmutableArray<TypedConstant> CommonConstructorArguments { get; }
    protected override ImmutableArray<KeyValuePair<string, TypedConstant>> CommonNamedArguments { get; }
}