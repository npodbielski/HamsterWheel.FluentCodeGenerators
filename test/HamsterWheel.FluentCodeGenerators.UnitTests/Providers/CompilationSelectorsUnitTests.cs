using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Providers.Transformations;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Providers;

public class CompilationSelectorsUnitTests
{
    private static Compilation GetTestCompilation()
    {
        var objectReference = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
        var consoleReference = MetadataReference.CreateFromFile(typeof(Console).Assembly.Location);
        return CSharpCompilation.Create(
            "TestAssembly",
            references: [objectReference, consoleReference]);
    }

    [Fact]
    public void TypeResolverService_WhenCalledWithCompilation_ThenReturnsWorkingService()
    {
        // arrange
        var compilation = GetTestCompilation();

        // act
        var service = CompilationSelectors.TypeResolverService(compilation, CancellationToken.None);

        // assert
        service.Should().NotBeNull();
        service.Should().BeOfType<TypeResolverService>();
    }

    [Fact]
    public void TypeResolverService_WhenCalledWithCompilation_ThenResolvesTypesCorrectly()
    {
        // arrange
        var compilation = GetTestCompilation();

        // act
        var service = CompilationSelectors.TypeResolverService(compilation, CancellationToken.None);
        var result = service.GetExternalTypeOrNull("System.Object");

        // assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Object");
    }

    [Fact]
    public void TypeResolverService_WhenCalledWithCompilation_ThenReturnsNullForNonExistentType()
    {
        // arrange
        var compilation = GetTestCompilation();

        // act
        var service = CompilationSelectors.TypeResolverService(compilation, CancellationToken.None);
        var result = service.GetExternalTypeOrNull("NonExistentType12345");

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public void TypeResolver_WhenCalledWithCompilation_ThenReturnsTypeResolverDelegate()
    {
        // arrange
        var compilation = GetTestCompilation();

        // act
        var resolver = CompilationSelectors.TypeResolver(compilation, CancellationToken.None);
        var result = resolver("System.Object");

        // assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Object");
    }

    [Fact]
    public void GetAssemblyName_WhenCompilationHasAssemblyName_ThenReturnsAssemblyName()
    {
        // arrange
        var compilation = GetTestCompilation();

        // act
        var result = CompilationSelectors.GetAssemblyName(compilation, "Fallback", CancellationToken.None);

        // assert
        result.Should().Be("TestAssembly");
    }

    [Fact]
    public void GetAssemblyName_WhenCompilationHasNoAssemblyName_ThenReturnsCompilationName()
    {
        // arrange
        var compilation = CSharpCompilation.Create("TestAssembly");

        // act
        var result = CompilationSelectors.GetAssemblyName(compilation, "FallbackValue", CancellationToken.None);

        // assert
        result.Should().Be("TestAssembly");
    }

    [Fact]
    public void GetAssemblyOrModuleSymbolResolver_WhenCalledWithCompilation_ThenReturnsResolverDelegate()
    {
        // arrange
        var compilation = GetTestCompilation();

        // act
        var resolver = CompilationSelectors.GetAssemblyOrModuleSymbolResolver(compilation, CancellationToken.None);
        var result = resolver(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));

        // assert
        result.Should().BeNull();
    }
}
