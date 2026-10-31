using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Providers;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Providers;

public class AssemblyVisitorUnitTests
{
    [Fact]
    public void GetAllNamespacesBy_WhenNoPredicate_ReturnsAllNamespaces()
    {
        // arrange
        const string namespace1 = "Namespace1";
        const string namespace2 = "Namespace2";
        var testAssembly = CreateTestAssembly("TestAssembly", [namespace1, namespace2]);

        // act
        var result = testAssembly.GetAllNamespacesBy();

        // assert
        var namespacesNames = result.Select(n => n.Name).ToList();
        namespacesNames.Should().HaveCount(3);
        namespacesNames.Should().Contain(namespace1);
        namespacesNames.Should().Contain(namespace2);
    }

    [Fact]
    public void GetAllNamespacesBy_WithPredicate_ReturnsFilteredNamespaces()
    {
        // arrange
        const string namespace1 = "Namespace1";
        const string namespace2 = "Namespace2";
        var testAssembly = CreateTestAssembly("TestAssembly", [namespace1, namespace2]);

        // act
        var result = testAssembly.GetAllNamespacesBy(ns => ns.Name == namespace1);

        // assert
        var namespacesNames = result.Select(n => n.Name).ToList();
        namespacesNames.Should().HaveCount(1);
        namespacesNames.Should().Contain(namespace1);
        namespacesNames.Should().NotContain(namespace2);
    }

    [Fact]
    public void GetAllNamespacesBy_WithNoMatchingPredicate_ReturnsEmpty()
    {
        // arrange
        const string namespace1 = "Namespace1";
        const string namespace2 = "Namespace2";
        var testAssembly = CreateTestAssembly("TestAssembly", [namespace1, namespace2]);

        // act
        var result = testAssembly.GetAllNamespacesBy(ns => ns.Name == "NonExistent");

        // assert
        var namespacesNames = result.Select(n => n.Name).ToList();
        namespacesNames.Should().BeEmpty();
    }

    [Fact]
    public void GetAllTypesBy_WithPredicate_ReturnsFilteredTypes()
    {
        // arrange
        const string type1 = "Type1";
        const string type2 = "Type2";
        var testAssembly = CreateTestAssembly("TestAssembly", [
            new DummyNamespaceSymbol("Namespace1", [
                new DummyNamedTypeSymbol("Type1", "Namespace1")
                {
                    Name = type1
                }
            ]),
            new DummyNamespaceSymbol("Namespace1", [
                new DummyNamedTypeSymbol("Type2", "Namespace2")
                {
                    Name = type2
                }
            ])
        ]);

        // act
        var result = testAssembly.GetAllTypesBy(t => t.Name == type1);

        // assert
        var types = result.Select(n => n.Name).ToList();
        types.Should().ContainSingle();
        types.Should().Contain(type1);
        types.Should().NotContain(type2);
    }

    [Fact]
    public void GetAllTypesBy_WithNoMatchingPredicate_ReturnsEmpty()
    {
        // arrange
        var testAssembly = CreateTestAssembly("TestAssembly", [
            new DummyNamespaceSymbol("Namespace1", [
                new DummyNamedTypeSymbol("Type1", "Namespace1")
                {
                    Name = "Type1"
                }
            ])
        ]);

        // act
        var result = testAssembly.GetAllTypesBy(t => t.Name == "NonExistent");

        // assert
        var types = result.Select(n => n.Name).ToList();
        types.Should().BeEmpty();
    }

    [Fact]
    public void IterateSymbolsAndItsMembers_YieldsNamespaceSymbol()
    {
        // arrange
        var namespaceSymbol = new DummyNamespaceSymbol("TestNamespace");

        // act
        var result = namespaceSymbol.IterateSymbolsAndItsMembers();

        // assert
        var results = result.ToList();
        results.Should().Contain(namespaceSymbol);
    }

    [Fact]
    public void IterateSymbolsAndItsMembers_YieldsNestedNamespace()
    {
        // arrange
        const string nameSpace = "OuterNamespace";
        const string innerNamespace = "InnerNamespace";
        var outerNamespace = new DummyNamespaceSymbol(nameSpace, [new DummyNamespaceSymbol(innerNamespace)]);

        // act
        var result = outerNamespace.IterateSymbolsAndItsMembers();

        // assert
        var results = result.Select(n => n.Name).ToList();
        results.Should().Contain(nameSpace);
        results.Should().Contain(innerNamespace);
    }

    [Fact]
    public void IterateSymbolsAndItsMembers_YieldsNamedTypeSymbol()
    {
        // arrange
        const string nameSpace = "TestNamespace";
        const string typeName = "TestType";
        var namespaceSymbol = new DummyNamespaceSymbol(nameSpace, [
            new DummyNamedTypeSymbol(typeName, nameSpace)
        ]);

        // act
        var result = namespaceSymbol.IterateSymbolsAndItsMembers();

        // assert
        var results = result.Select(n => n.Name).ToList();
        results.Should().Contain(nameSpace);
        results.Should().Contain(typeName);
    }

    [Fact]
    public void IterateSymbolsAndItsMembers_DeeplyNestedNamespaces_YieldsAll()
    {
        // arrange
        const string nameSpace = "OuterNamespace";
        const string middleNamespace = "Middle";
        const string innerNamespace = "InnerNamespace";
        var outerNamespace = new DummyNamespaceSymbol(nameSpace, [
            new DummyNamespaceSymbol(middleNamespace, [
                new DummyNamespaceSymbol(innerNamespace)
            ])
        ]);

        // act
        var result = outerNamespace.IterateSymbolsAndItsMembers();

        // assert
        var results = result.Select(n => n.Name).ToList();
        results.Should().Contain(nameSpace);
        results.Should().Contain(middleNamespace);
        results.Should().Contain(innerNamespace);
    }

    private static DummyAssemblySymbol CreateTestAssembly(string assemblyName, string[] nestedNamespaces) =>
        new(assemblyName, nestedNamespaces: [.. nestedNamespaces.Select(s => new DummyNamespaceSymbol(s))]);

    private static DummyAssemblySymbol
        CreateTestAssembly(string assemblyName, DummyNamespaceSymbol[] nestedNamespaces) =>
        new(assemblyName, nestedNamespaces: [.. nestedNamespaces]);
}