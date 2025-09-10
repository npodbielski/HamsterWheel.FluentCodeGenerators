using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Exceptions;
using HamsterWheel.FluentCodeGenerators.Providers.Transformations;
using HamsterWheel.FluentCodeGenerators.UnitTests.Dummies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Providers.Transformations;

public class SourceProductionContextExtensionsUnitTests
{
    [Fact]
    public void ReportException_WhenCalledDuringCompilation_ThenReportDiagnostic()
    {
        //arrange
        var testGenerator = new DummyGenerator<AdditionalText>(c => c.AdditionalTextsProvider.First(),
            (c, _) => c.ReportException(new GeneratorException("Exception happened")));
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }")]);
        var compilation = CSharpCompilation.Create(nameof(testGenerator));

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out var diagnostics);

        //assert
        diagnostics.Should().NotBeEmpty()
            .And.HaveCount(1)
            .And.Subject.First()
            .ToString().Should().Contain("error exception: Exception happened");
    }

    [Fact]
    public void ReportWarning_WhenCalledDuringCompilation_ThenReportDiagnostic()
    {
        //arrange
        var testGenerator = new DummyGenerator<AdditionalText>(c => c.AdditionalTextsProvider.First(),
            (c, _) => c.ReportWarning("Warning happened"));
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }")]);
        var compilation = CSharpCompilation.Create(nameof(testGenerator));

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out var diagnostics);

        //assert
        diagnostics.Should().NotBeEmpty()
            .And.HaveCount(1)
            .And.Subject.First()
            .ToString().Should().Contain("warning warning: Warning happened");
    }

    [Fact]
    public void ReportInformation_WhenCalledDuringCompilation_ThenReportDiagnostic()
    {
        //arrange
        var testGenerator = new DummyGenerator<AdditionalText>(c => c.AdditionalTextsProvider.First(),
            (c, _) => c.ReportInformation("information happened"));
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }")]);
        var compilation = CSharpCompilation.Create(nameof(testGenerator));

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out var diagnostics);

        //assert
        diagnostics.Should().NotBeEmpty()
            .And.HaveCount(1)
            .And.Subject.First()
            .ToString().Should().Contain("info information: information happened");
    }
}