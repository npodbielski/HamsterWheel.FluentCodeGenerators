using System.Collections.Immutable;
using System.Globalization;
using Microsoft.CodeAnalysis;

// ReSharper disable UnassignedGetOnlyAutoProperty
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;

#pragma warning disable RS1009
public class DummyNamespaceSymbol(string nameSpace, ISymbol[]? members = null) : INamespaceSymbol
#pragma warning restore RS1009
{
    private readonly List<ISymbol> _members = members is not null ? [.. members] : [];

    public override string ToString()
    {
        var parts = new List<string>();
        var current = this;
        while (current is not null && !current.IsGlobalNamespace)
        {
            parts.Add(current.Name);
            current = current.Parent as DummyNamespaceSymbol;
        }

        parts.Reverse();
        return string.Join(".", parts);
    }

    public SymbolKind Kind { get; set; }
    public string Language { get; set; }
    public string Name { get; set; } = nameSpace;
    public string MetadataName { get; set; }
    public int MetadataToken { get; set; }
    public ISymbol ContainingSymbol => Parent!;
    public IAssemblySymbol ContainingAssembly { get; set; }
    public IModuleSymbol ContainingModule { get; set; }
    public INamedTypeSymbol ContainingType { get; set; }
    public INamespaceSymbol Parent { get; set; }
    public INamespaceSymbol ContainingNamespace => Parent!;
    public bool IsDefinition { get; set; }
    public bool IsStatic { get; set; }
    public bool IsVirtual { get; set; }
    public bool IsOverride { get; set; }
    public bool IsAbstract { get; set; }
    public bool IsSealed { get; set; }
    public bool IsExtern { get; set; }
    public bool IsImplicitlyDeclared { get; set; }
    public bool CanBeReferencedByName { get; set; }
    public ImmutableArray<Location> Locations { get; set; }
    public ImmutableArray<SyntaxReference> DeclaringSyntaxReferences { get; set; }
    public Accessibility DeclaredAccessibility { get; set; }
    public ISymbol OriginalDefinition { get; set; }
    public bool HasUnsupportedMetadata { get; set; }
    public bool IsGlobalNamespace { get; set; }
    public NamespaceKind NamespaceKind { get; set; }
    public Compilation? ContainingCompilation { get; set; }
    public ImmutableArray<INamespaceSymbol> ConstituentNamespaces { get; set; }

    public bool IsNamespace { get; }
    public bool IsType { get; }

    public ImmutableArray<AttributeData> GetAttributes() => throw new NotImplementedException();

    public void Accept(SymbolVisitor visitor) => throw new NotImplementedException();

    public TResult? Accept<TResult>(SymbolVisitor<TResult> visitor) => throw new NotImplementedException();

    public TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument) =>
        throw new NotImplementedException();

    public string? GetDocumentationCommentId() => throw new NotImplementedException();

    public string? GetDocumentationCommentXml(CultureInfo? preferredCulture = null, bool expandIncludes = false,
        CancellationToken cancellationToken = new()) =>
        throw new NotImplementedException();

    public string ToDisplayString(SymbolDisplayFormat? format = null) => throw new NotImplementedException();

    public ImmutableArray<SymbolDisplayPart> ToDisplayParts(SymbolDisplayFormat? format = null) =>
        throw new NotImplementedException();

    public string
        ToMinimalDisplayString(SemanticModel semanticModel, int position, SymbolDisplayFormat? format = null) =>
        throw new NotImplementedException();

    public ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts(SemanticModel semanticModel, int position,
        SymbolDisplayFormat? format = null) =>
        throw new NotImplementedException();

    public bool Equals(ISymbol? other, SymbolEqualityComparer equalityComparer) => throw new NotImplementedException();

    ImmutableArray<ISymbol> INamespaceOrTypeSymbol.GetMembers() => [.. _members];

    IEnumerable<INamespaceOrTypeSymbol> INamespaceSymbol.GetMembers(string name) =>
        _members.OfType<INamespaceOrTypeSymbol>().Where(m => m.Name == name);

    public IEnumerable<INamespaceSymbol> GetNamespaceMembers() => throw new NotImplementedException();
    IEnumerable<INamespaceOrTypeSymbol> INamespaceSymbol.GetMembers() => throw new NotImplementedException();

    ImmutableArray<ISymbol> INamespaceOrTypeSymbol.GetMembers(string name) => throw new NotImplementedException();

    public ImmutableArray<INamedTypeSymbol> GetTypeMembers() => throw new NotImplementedException();

    public ImmutableArray<INamedTypeSymbol> GetTypeMembers(string name) => throw new NotImplementedException();

    public ImmutableArray<INamedTypeSymbol> GetTypeMembers(string name, int arity) =>
        throw new NotImplementedException();

    public bool Equals(ISymbol? other) => other is DummyNamespaceSymbol otherNs && otherNs.Name == Name;
}