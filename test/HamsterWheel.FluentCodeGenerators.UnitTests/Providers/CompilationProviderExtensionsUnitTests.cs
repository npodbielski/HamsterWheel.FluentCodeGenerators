using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Providers;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Providers;

using TypeResolverDelegate = Func<string, INamedTypeSymbol?>;
using AssemblyOrModuleResolverDelegate = Func<MetadataReference, ISymbol?>;

public class CompilationProviderExtensionsUnitTests
{
    [Fact]
    public void GetAssemblyName_WhenCompilationHaveAssemblyName_ThenSourceIsAssemblyName()
    {
        //arrange
        const string expected = "ExpectedAssemblyName";
        var testGenerator = GetGeneratorFor(c => c.CompilationProvider.GetAssemblyName("Fallback"));
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .WithUpdatedAnalyzerConfigOptions(
                new DummyAnalyzerConfigOptionsProvider(nameof(AnalyzerConfigOptionsProviderExtensionsUnitTests)));
        var compilation = CSharpCompilation.Create(expected);

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.Should().Be(expected);
    }

    [Fact]
    public void GetAssemblyName_WhenCompilationDoesNotHaveAssemblyName_ThenSourceIsFallback()
    {
        //arrange
        var fallback = "Fallback";
        var testGenerator = GetGeneratorFor(c => c.CompilationProvider.GetAssemblyName(fallback));
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create(null);

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.Should().Be(fallback);
    }

    [Fact]
    public void GetTypeResolver_WhenGeneratorRegistersAnOutput_ThenResolvesTypeFromFullName()
    {
        //arrange
        var currentType = GetType();
        var fullName = GetType().FullName.Should().NotBeNull().And.Subject;
        INamedTypeSymbol? resolvedType = null;
        var testGenerator = GetGeneratorFor(c => c.CompilationProvider.GetTypeResolver(),
            (_, s) => resolvedType = s(fullName));
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create(null, references: (List<MetadataReference>)
            [MetadataReference.CreateFromFile(GetType().Assembly.Location)]);

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.Should().BeOfType<TypeResolverDelegate>();
        resolvedType.Should().NotBeNull();
        resolvedType.Name.Should().Be(nameof(CompilationProviderExtensionsUnitTests));
        resolvedType.ContainingNamespace.ToString().Should().Be(currentType.Namespace);
    }

    [Fact]
    public void GetTypeResolverService_WhenGeneratorRegistersAnOutput_ThenReturnsWorkingService()
    {
        //arrange
        var fullName = GetType().FullName.Should().NotBeNull().And.Subject;
        ITypeResolverService? resolverService = null;
        var testGenerator = GetGeneratorFor(c => c.CompilationProvider.GetTypeResolverService(),
            (_, s) => resolverService = s);
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create(null, references: (List<MetadataReference>)
            [MetadataReference.CreateFromFile(GetType().Assembly.Location)]);

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.Should().BeAssignableTo<ITypeResolverService>();
        resolverService.Should().NotBeNull();
        resolverService!.GetExternalTypeOrNull(fullName).Should().NotBeNull();
    }

    [Fact]
    public void GetAssemblyOrModuleResolver_WhenGeneratorRegistersAnOutput_ThenReturnsResolverDelegate()
    {
        //arrange
        var objectAssemblyLocation = typeof(object).Assembly.Location;
        AssemblyOrModuleResolverDelegate? resolver = null;
        var testGenerator = GetGeneratorFor(c => c.CompilationProvider.GetAssemblyOrModuleResolver(),
            (_, s) => resolver = s);
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create(null, references: (List<MetadataReference>)
            [MetadataReference.CreateFromFile(objectAssemblyLocation)]);

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.Should().BeAssignableTo<AssemblyOrModuleResolverDelegate>();
        resolver.Should().NotBeNull();
    }

    [Fact]
    public void GetReferencesAsSymbolsBy_WhenGeneratorRegistersAnOutput_ThenReturnsAssemblySymbols()
    {
        //arrange
        var objectAssemblyLocation = typeof(object).Assembly.Location;
        IEnumerable<IAssemblySymbol>? symbols = null;
        var testGenerator = GetGeneratorFor(c => c.CompilationProvider.GetReferencesAsSymbolsBy(),
            (_, s) => symbols = s);
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create(null, references: (List<MetadataReference>)
            [MetadataReference.CreateFromFile(objectAssemblyLocation)]);

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        symbols.Should().NotBeNull();
        symbols!.Should().ContainSingle();
        symbols.First().Name.Should().Be("System.Private.CoreLib");
    }

    [Fact]
    public void GetReferencesAsSymbolsBy_WhenSelectorProvided_ThenFiltersReferences()
    {
        //arrange
        var objectAssemblyLocation = typeof(object).Assembly.Location;
        var consoleAssemblyLocation = typeof(Console).Assembly.Location;
        IEnumerable<IAssemblySymbol>? symbols = null;
        var testGenerator = GetGeneratorFor(c => c.CompilationProvider.GetReferencesAsSymbolsBy(
            refs => refs.Where(r => r.Display?.Contains("System.Private.CoreLib") == true)),
            (_, s) => symbols = s);
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create(null, references: (List<MetadataReference>)
            [MetadataReference.CreateFromFile(objectAssemblyLocation),
             MetadataReference.CreateFromFile(consoleAssemblyLocation)]);

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        symbols.Should().NotBeNull();
        symbols!.Should().ContainSingle();
        symbols.First().Name.Should().Be("System.Private.CoreLib");
    }

    private static DummyGenerator<T> GetGeneratorFor<T>(
        Func<IncrementalGeneratorInitializationContext, IncrementalValueProvider<T>> providerBuilder,
        Action<SourceProductionContext, T>? generatorCallback = null
    ) => new(providerBuilder, generatorCallback);
}