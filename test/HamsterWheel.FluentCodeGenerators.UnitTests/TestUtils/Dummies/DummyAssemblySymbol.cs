#pragma warning disable RS1009
using System.Collections.Immutable;
using System.Globalization;
using Microsoft.CodeAnalysis;

// ReSharper disable UnassignedGetOnlyAutoProperty
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;

public class DummyAssemblySymbol : IAssemblySymbol
{
    private readonly string _name;

    public DummyAssemblySymbol(string name, INamespaceSymbol[]? nestedNamespaces = null,
        INamedTypeSymbol[]? namedTypeSymbols = null)
    {
        List<ISymbol> globalMembers = [];
        if (nestedNamespaces is not null)
        {
            globalMembers = [.. nestedNamespaces];
        }

        if (namedTypeSymbols is not null)
        {
            globalMembers = [.. globalMembers, .. namedTypeSymbols];
        }

        GlobalNamespace = new DummyNamespaceSymbol("test", [.. globalMembers]);
        _name = name;
    }

    public SymbolKind Kind { get; set; }
    public string Language { get; set; }
    public string Name { get; set; }
    public string MetadataName { get; set; }
    public int MetadataToken { get; set; }
    public ISymbol ContainingSymbol { get; set; }
    public IAssemblySymbol ContainingAssembly { get; set; }
    public IModuleSymbol ContainingModule { get; set; }
    public INamedTypeSymbol ContainingType { get; set; }
    public INamespaceSymbol ContainingNamespace { get; set; }
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
    public bool IsInteractive { get; set; }
    public AssemblyIdentity Identity { get; set; }
    public INamespaceSymbol GlobalNamespace { get; set; }
    public IEnumerable<IModuleSymbol> Modules { get; set; }
    public ICollection<string> TypeNames { get; set; }
    public ICollection<string> NamespaceNames { get; set; }
    public bool MightContainExtensionMethods { get; set; }

    public bool Equals(ISymbol? other) => throw new NotImplementedException();

    public ImmutableArray<AttributeData> GetAttributes() => throw new NotImplementedException();

    public void Accept(SymbolVisitor visitor) => throw new NotImplementedException();

    public TResult? Accept<TResult>(SymbolVisitor<TResult> visitor) => throw new NotImplementedException();

    public TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument) =>
        throw new NotImplementedException();

    public string? GetDocumentationCommentId() => throw new NotImplementedException();

    public string? GetDocumentationCommentXml(CultureInfo? preferredCulture = null, bool expandIncludes = false,
        CancellationToken cancellationToken = new CancellationToken()) =>
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

    public bool GivesAccessTo(IAssemblySymbol toAssembly) => throw new NotImplementedException();

    public INamedTypeSymbol? GetTypeByMetadataName(string fullyQualifiedMetadataName) =>
        throw new NotImplementedException();

    public INamedTypeSymbol? ResolveForwardedType(string fullyQualifiedMetadataName) =>
        throw new NotImplementedException();

    public ImmutableArray<INamedTypeSymbol> GetForwardedTypes() => throw new NotImplementedException();

    public AssemblyMetadata? GetMetadata() => throw new NotImplementedException();

    public override string ToString() => _name;
}