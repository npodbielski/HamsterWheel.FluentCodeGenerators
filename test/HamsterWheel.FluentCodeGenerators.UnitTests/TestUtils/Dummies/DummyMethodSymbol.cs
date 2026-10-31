using System.Collections.Immutable;
using System.Globalization;
using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;

#pragma warning disable CS8618
#pragma warning disable RS1009
public class DummyMethodSymbol(string name, IEnumerable<AttributeData>? attribute = null) : IMethodSymbol
{
    public bool Equals(ISymbol? other)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<AttributeData> GetAttributes()
    {
        if (attribute != null)
            return [.. attribute];
        return [];
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

    public ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts(SemanticModel semanticModel, int position,
        SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public bool Equals(ISymbol? other, SymbolEqualityComparer equalityComparer)
    {
        throw new NotImplementedException();
    }

    public SymbolKind Kind { get; }
    public string Language { get; }
    public string Name { get; } = name;
    public string MetadataName { get; }
    public int MetadataToken { get; }
    public ISymbol ContainingSymbol { get; }
    public IAssemblySymbol ContainingAssembly { get; }
    public IModuleSymbol ContainingModule { get; }
    public INamedTypeSymbol ContainingType { get; }
    public INamespaceSymbol ContainingNamespace { get; }
    public bool IsDefinition { get; }
    public bool IsStatic { get; }
    public bool IsVirtual { get; }
    public bool IsOverride { get; }
    public bool IsAbstract { get; }
    public bool IsSealed { get; }
    public bool IsExtern { get; }
    public bool IsImplicitlyDeclared { get; }
    public bool CanBeReferencedByName { get; }
    public ImmutableArray<Location> Locations { get; }
    public ImmutableArray<SyntaxReference> DeclaringSyntaxReferences { get; }
    public Accessibility DeclaredAccessibility { get; }
    public IMethodSymbol OriginalDefinition { get; }
    public IMethodSymbol? OverriddenMethod { get; }
    public ITypeSymbol? ReceiverType { get; }
    public NullableAnnotation ReceiverNullableAnnotation { get; }
    public IMethodSymbol? ReducedFrom { get; }
    public ImmutableArray<IMethodSymbol> ExplicitInterfaceImplementations { get; }
    public ImmutableArray<CustomModifier> ReturnTypeCustomModifiers { get; }
    public ImmutableArray<CustomModifier> RefCustomModifiers { get; }
    public SignatureCallingConvention CallingConvention { get; }
    public ImmutableArray<INamedTypeSymbol> UnmanagedCallingConventionTypes { get; }
    public ISymbol? AssociatedSymbol { get; }
    public IMethodSymbol? PartialDefinitionPart { get; }
    public IMethodSymbol? PartialImplementationPart { get; }
    public MethodImplAttributes MethodImplementationFlags { get; }
    public bool IsPartialDefinition { get; }
    public INamedTypeSymbol? AssociatedAnonymousDelegate { get; }
    public bool IsConditional { get; }

    public ITypeSymbol? GetTypeInferredDuringReduction(ITypeParameterSymbol reducedFromTypeParameter)
    {
        throw new NotImplementedException();
    }

    public IMethodSymbol? ReduceExtensionMethod(ITypeSymbol receiverType)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<AttributeData> GetReturnTypeAttributes()
    {
        throw new NotImplementedException();
    }

    public IMethodSymbol Construct(params ITypeSymbol[] typeArguments)
    {
        throw new NotImplementedException();
    }

    public IMethodSymbol Construct(ImmutableArray<ITypeSymbol> typeArguments,
        ImmutableArray<NullableAnnotation> typeArgumentNullableAnnotations)
    {
        throw new NotImplementedException();
    }

    public DllImportData? GetDllImportData()
    {
        throw new NotImplementedException();
    }

    public MethodKind MethodKind { get; }
    public int Arity { get; }
    public bool IsGenericMethod { get; }
    public bool IsExtensionMethod { get; }
    public bool IsAsync { get; }
    public bool IsVararg { get; }
    public bool IsCheckedBuiltin { get; }
    public bool HidesBaseMethodsByName { get; }
    public bool ReturnsVoid { get; }
    public bool ReturnsByRef { get; }
    public bool ReturnsByRefReadonly { get; }
    public RefKind RefKind { get; }
    public ITypeSymbol ReturnType { get; }
    public NullableAnnotation ReturnNullableAnnotation { get; }
    public ImmutableArray<ITypeSymbol> TypeArguments { get; }
    public ImmutableArray<NullableAnnotation> TypeArgumentNullableAnnotations { get; }
    public ImmutableArray<ITypeParameterSymbol> TypeParameters { get; }
    public ImmutableArray<IParameterSymbol> Parameters { get; }
    public IMethodSymbol ConstructedFrom { get; }
    public bool IsReadOnly { get; }
    public bool IsInitOnly { get; }

    ISymbol ISymbol.OriginalDefinition => OriginalDefinition;

    public bool HasUnsupportedMetadata { get; }
}