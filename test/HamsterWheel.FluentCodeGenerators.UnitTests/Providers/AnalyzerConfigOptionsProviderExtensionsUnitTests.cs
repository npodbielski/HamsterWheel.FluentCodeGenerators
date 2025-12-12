using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Providers;
using HamsterWheel.FluentCodeGenerators.Tokens;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Providers;

public class AnalyzerConfigOptionsProviderExtensionsUnitTests
{
    [Fact]
    public void GetGlobalOptions_WhenGeneratorRegistersOutput_ThenAnalyzerOptionsContainsRootNamespace()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<AnalyzerConfigOptions>(c => c.AnalyzerConfigOptionsProvider.GetGlobalOptions());
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .WithUpdatedAnalyzerConfigOptions(
                new DummyAnalyzerConfigOptionsProvider(nameof(AnalyzerConfigOptionsProviderExtensionsUnitTests)));
        var compilation = CSharpCompilation.Create(nameof(testGenerator),
            options: new CSharpCompilationOptions(outputKind: OutputKind.ConsoleApplication, moduleName: "testModule"));

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.TryGetValue("build_property.rootnamespace", out var value).Should().BeTrue();
        value.Should().Be(nameof(AnalyzerConfigOptionsProviderExtensionsUnitTests));
    }

    [Fact]
    public void GetRootNamespace_WhenGeneratorRegistersOutput_ThenAnalyzerOptionsContainsRootNamespace()
    {
        //arrange
        const string fallbackValue = "FallbackNamespace";
        var testGenerator =
            new DummyGenerator<Namespace>(c => c.AnalyzerConfigOptionsProvider.GetRootNamespace(fallbackValue));
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .WithUpdatedAnalyzerConfigOptions(
                new DummyAnalyzerConfigOptionsProvider(nameof(AnalyzerConfigOptionsProviderExtensionsUnitTests)));
        var compilation = CSharpCompilation.Create(nameof(testGenerator),
            options: new CSharpCompilationOptions(outputKind: OutputKind.ConsoleApplication, moduleName: "testModule"));

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.Should().Be(nameof(AnalyzerConfigOptionsProviderExtensionsUnitTests).ToNamespace());
    }
}

