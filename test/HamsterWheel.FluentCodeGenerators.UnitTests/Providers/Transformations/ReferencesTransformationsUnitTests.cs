using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Providers;
using HamsterWheel.FluentCodeGenerators.Providers.Transformations;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Providers.Transformations;

public class ReferencesTransformationsUnitTests
{
    [Fact]
    public void ReferenceToSymbol_WhenCalledWithReferences_ThenReturnsAssemblySymbols()
    {
        //arrange
        var currentAssembly = GetType().Assembly.Location;
        IAssemblySymbol? resolvedAssembly = null;
        var testGenerator = new DummyGenerator<IEnumerable<IAssemblySymbol>>(
            c => c.CompilationProvider.GetReferencesAsSymbolsBy(refs => refs),
            (_, symbols) => resolvedAssembly = symbols.FirstOrDefault());
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create("TestAssembly",
            references: [MetadataReference.CreateFromFile(currentAssembly)]);

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        var symbols = testGenerator.Source;
        symbols.Should().ContainSingle();
        symbols.First().Name.Should().NotBeNull();
    }

    [Fact]
    public void ReferenceToSymbol_WhenCalledWithNoReferences_ThenReturnsEmpty()
    {
        //arrange
        var testGenerator = new DummyGenerator<IEnumerable<IAssemblySymbol>>(
            c => c.CompilationProvider.GetReferencesAsSymbolsBy());
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create("TestAssembly");

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.Should().BeEmpty();
    }

    [Fact]
    public void ReferenceToSymbol_DirectCall_WhenResolverReturnsAssemblySymbols_ThenFiltersCorrectly()
    {
        //arrange
        var currentAssembly = GetType().Assembly.Location;
        var compilation = CSharpCompilation.Create("TestAssembly",
            references: [MetadataReference.CreateFromFile(currentAssembly)]);
        var reference = compilation.References.First();

        var resolver = (Func<MetadataReference, ISymbol?>)(r =>
            compilation.GetAssemblyOrModuleSymbol(r));
        var input = (ToSymbol: resolver, References: new[] { reference });

        //act
        var result = ReferencesTransformations.ReferenceToSymbol(input, CancellationToken.None);

        //assert
        result.Should().ContainSingle();
    }

    [Fact]
    public void ReferenceToSymbol_DirectCall_WhenResolverReturnsNull_ThenFiltersOut()
    {
        //arrange
        var currentAssembly = GetType().Assembly.Location;
        var reference = MetadataReference.CreateFromFile(currentAssembly);
        var resolver = (Func<MetadataReference, ISymbol?>)(r => null);
        var input = (ToSymbol: resolver, References: new[] { reference });

        //act
        var result = ReferencesTransformations.ReferenceToSymbol(input, CancellationToken.None);

        //assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ReferenceToSymbol_DirectCall_WhenResolverReturnsNonAssembly_ThenFiltersOut()
    {
        //arrange
        var currentAssembly = GetType().Assembly.Location;
        var compilation = CSharpCompilation.Create("TestAssembly",
            references: [MetadataReference.CreateFromFile(currentAssembly)]);
        var reference = MetadataReference.CreateFromFile(currentAssembly);

        // Return a non-assembly symbol (e.g. the assembly itself cast to ISymbol but not IAssemblySymbol)
        // We use the compilation's type resolver to get a type symbol instead
        var someType = compilation.GetTypeByMetadataName(typeof(string).FullName!);
        var resolver = (Func<MetadataReference, ISymbol?>)(r => someType);

        var input = (ToSymbol: resolver, References: new[] { reference });

        //act
        var result = ReferencesTransformations.ReferenceToSymbol(input, CancellationToken.None);

        //assert
        result.Should().BeEmpty();
    }
}
