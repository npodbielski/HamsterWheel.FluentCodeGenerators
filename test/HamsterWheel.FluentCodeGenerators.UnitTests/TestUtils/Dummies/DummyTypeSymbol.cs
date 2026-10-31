#pragma warning disable RS1009
using System.Collections.Immutable;
using System.Globalization;
using Microsoft.CodeAnalysis;
// ReSharper disable UnassignedGetOnlyAutoProperty
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;

public class DummyTypeSymbol : ITypeSymbol
{
    public bool Equals(ISymbol? other)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<AttributeData> GetAttributes()
    {
        throw new NotImplementedException();
    }

    public void Accept(SymbolVisitor visitor)
    {
        throw new NotImplementedException();
    }

    public TResult? Accept<TResult>(SymbolVisitor<TResult> visitor)
    {
        throw new NotImplementedException();
    }

    public TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument)
    {
        throw new NotImplementedException();
    }

    public string? GetDocumentationCommentId()
    {
        throw new NotImplementedException();
    }

    public string? GetDocumentationCommentXml(CultureInfo? preferredCulture = null, bool expandIncludes = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public string ToDisplayString(SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<SymbolDisplayPart> ToDisplayParts(SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public string ToMinimalDisplayString(SemanticModel semanticModel, int position, SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts(SemanticModel semanticModel, int position, SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public bool Equals(ISymbol? other, SymbolEqualityComparer equalityComparer)
    {
        throw new NotImplementedException();
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
    public ITypeSymbol OriginalDefinition { get; set; }
    public SpecialType SpecialType { get; set; }
    public bool IsRefLikeType { get; set; }
    public bool IsUnmanagedType { get; set; }
    public bool IsReadOnly { get; set; }
    public bool IsRecord { get; set; }
    public NullableAnnotation NullableAnnotation { get; set; }

    public ISymbol? FindImplementationForInterfaceMember(ISymbol interfaceMember)
    {
        throw new NotImplementedException();
    }

    public string ToDisplayString(NullableFlowState topLevelNullability, SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<SymbolDisplayPart> ToDisplayParts(NullableFlowState topLevelNullability, SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public string ToMinimalDisplayString(SemanticModel semanticModel, NullableFlowState topLevelNullability, int position,
        SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts(SemanticModel semanticModel, NullableFlowState topLevelNullability, int position,
        SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public ITypeSymbol WithNullableAnnotation(NullableAnnotation nullableAnnotation)
    {
        throw new NotImplementedException();
    }

    public TypeKind TypeKind { get; set; }
    public INamedTypeSymbol? BaseType { get; set; }
    public ImmutableArray<INamedTypeSymbol> Interfaces { get; set; }
    public ImmutableArray<INamedTypeSymbol> AllInterfaces { get; set; }
    public bool IsReferenceType { get; set; }
    public bool IsValueType { get; set; }
    public bool IsAnonymousType { get; set; }
    public bool IsTupleType { get; set; }
    public bool IsNativeIntegerType { get; set; }

    ISymbol ISymbol.OriginalDefinition => OriginalDefinition;

    public bool HasUnsupportedMetadata { get; set; }
    public ImmutableArray<ISymbol> GetMembers()
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<ISymbol> GetMembers(string name)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<INamedTypeSymbol> GetTypeMembers()
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<INamedTypeSymbol> GetTypeMembers(string name)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<INamedTypeSymbol> GetTypeMembers(string name, int arity)
    {
        throw new NotImplementedException();
    }

    public bool IsNamespace { get; set; }
    public bool IsType { get; set; }
}