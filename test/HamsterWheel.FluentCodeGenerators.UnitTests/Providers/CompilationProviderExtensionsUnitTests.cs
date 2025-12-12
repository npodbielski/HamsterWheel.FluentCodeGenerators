using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Providers;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Providers;

using TypeResolverDelegate = Func<string, INamedTypeSymbol?>;

public class CompilationProviderExtensionsUnitTests
{
    [Fact]
    public void GetAssemblyName_WhenCompilationHaveAssemblyName_ThenSourceIsAssemblyName()
    {
        //arrange
        var testGenerator = GetGeneratorFor(c => c.CompilationProvider.GetAssemblyName("Fallback"));
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .WithUpdatedAnalyzerConfigOptions(
                new DummyAnalyzerConfigOptionsProvider(nameof(AnalyzerConfigOptionsProviderExtensionsUnitTests)));
        var expected = nameof(testGenerator);
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
        var expected = nameof(testGenerator);
        var compilation = CSharpCompilation.Create(null);

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.Should().Be(fallback);
    }

    [Fact]
    public void GetTypeResolver_WhenGeneratorRegistersAnOutput_ThenSourceIsFallback()
    {
        //arrange
        var currentType = GetType();
        INamedTypeSymbol? resolvedType = null;
        var testGenerator = GetGeneratorFor(c => c.CompilationProvider.GetTypeResolver(),
            (_, s) => resolvedType = s(currentType.FullName));
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create(null, references: new List<MetadataReference>
        {
            MetadataReference.CreateFromFile(GetType().Assembly.Location)
        });

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

    private static DummyGenerator<T> GetGeneratorFor<T>(
        Func<IncrementalGeneratorInitializationContext, IncrementalValueProvider<T>> providerBuilder,
        Action<SourceProductionContext, T>? generatorCallback = null
    ) => new(providerBuilder, generatorCallback);
}