using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Providers.Transformations;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Providers.Transformations;

public class ProviderTransformationsUnitTests
{
    [Fact]
    public void First_WhenCalledOnArray_ThenReturnsFirstValue()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<AdditionalText>(c => c.AdditionalTextsProvider.First());
        var expected = "./settings/compilation.json";
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([new TestAdditionalFile(expected, "{ \"myGenerator\": \"2\" }")]);
        var compilation = CSharpCompilation.Create(nameof(testGenerator));

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.Path.Should().Be(expected);
    }

    [Fact]
    public void First_WhenCalledOnEmptyArray_ThenThrows()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<AdditionalText>(c => c.AdditionalTextsProvider.First());
        var expected = "./settings/compilation.json";
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create(nameof(testGenerator));

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out var diagnostics);

        //assert
        diagnostics.Should().NotBeNullOrEmpty()
            .And.ContainSingle()
            .Subject.ToString().Should().Contain("InvalidOperationException")
            .And.Contain("Sequence contains no elements");
    }

    [Fact]
    public void FirstOrDefault_WhenCalledOnArray_ThenReturnsFirstValue()
    {
        //arrange
        const string expected = "default";
        var testGenerator =
            new DummyGenerator<AdditionalText>(c =>
                c.AdditionalTextsProvider.FirstOrDefault(new TestAdditionalFile(expected, "default")));
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create(nameof(testGenerator));

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.Path.Should().Be("default");
    }
}