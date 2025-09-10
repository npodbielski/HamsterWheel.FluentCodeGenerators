using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Providers;
using HamsterWheel.FluentCodeGenerators.UnitTests.Dummies;
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
}

