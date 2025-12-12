using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Providers;
using HamsterWheel.FluentCodeGenerators.Providers.Transformations;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Providers.Transformations;

public class IncrementalValueProviderExtensionsUnitTests
{
    [Fact]
    public void MultiCombineOfT3_WhenCalled_ThenCombinesInToOneTuple()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<(AdditionalText, AdditionalText, AdditionalText)>(c =>
            {
                var file1 = c.AdditionalTextsProvider.First();
                var file2 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("second.json")).First();
                var file3 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("third.json")).First();
                return file1.MultiCombine(file2, file3);
            });
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([
                new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }"),
                new TestAdditionalFile("second.json", "{ \"myGenerator\": \"2\" }"),
                new TestAdditionalFile("third.json", "{ \"myGenerator\": \"3\" }")
            ]);
        var compilation = CSharpCompilation.Create(nameof(testGenerator));

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.Item1.Path.Should().Be("first.json");
        testGenerator.Source.Item2.Path.Should().Be("second.json");
        testGenerator.Source.Item3.Path.Should().Be("third.json");
    }

    [Fact]
    public void MultiCombineOfT4_WhenCalled_ThenCombinesInToOneTuple()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<(AdditionalText, AdditionalText, AdditionalText, AdditionalText)>(c =>
            {
                var file1 = c.AdditionalTextsProvider.First();
                var file2 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("second.json")).First();
                var file3 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("third.json")).First();
                var file4 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fourth.json")).First();
                return file1.MultiCombine(file2, file3, file4);
            });
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([
                new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }"),
                new TestAdditionalFile("second.json", "{ \"myGenerator\": \"2\" }"),
                new TestAdditionalFile("third.json", "{ \"myGenerator\": \"3\" }"),
                new TestAdditionalFile("fourth.json", "{ \"myGenerator\": \"4\" }")
            ]);
        var compilation = CSharpCompilation.Create(nameof(testGenerator));

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.Item1.Path.Should().Be("first.json");
        testGenerator.Source.Item2.Path.Should().Be("second.json");
        testGenerator.Source.Item3.Path.Should().Be("third.json");
        testGenerator.Source.Item4.Path.Should().Be("fourth.json");
    }

    [Fact]
    public void MultiCombineOfT5_WhenCalled_ThenCombinesInToOneTuple()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<(AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText)>(c =>
            {
                var file1 = c.AdditionalTextsProvider.First();
                var file2 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("second.json")).First();
                var file3 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("third.json")).First();
                var file4 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fourth.json")).First();
                var file5 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fifth.json")).First();
                return file1.MultiCombine(file2, file3, file4, file5);
            });
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([
                new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }"),
                new TestAdditionalFile("second.json", "{ \"myGenerator\": \"2\" }"),
                new TestAdditionalFile("third.json", "{ \"myGenerator\": \"3\" }"),
                new TestAdditionalFile("fourth.json", "{ \"myGenerator\": \"4\" }"),
                new TestAdditionalFile("fifth.json", "{ \"myGenerator\": \"5\" }")
            ]);
        var compilation = CSharpCompilation.Create(nameof(testGenerator));

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.Item1.Path.Should().Be("first.json");
        testGenerator.Source.Item2.Path.Should().Be("second.json");
        testGenerator.Source.Item3.Path.Should().Be("third.json");
        testGenerator.Source.Item4.Path.Should().Be("fourth.json");
        testGenerator.Source.Item5.Path.Should().Be("fifth.json");
    }

    [Fact]
    public void MultiCombineOfT6_WhenCalled_ThenCombinesInToOneTuple()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<(AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText,
                AdditionalText)>(c =>
            {
                var file1 = c.AdditionalTextsProvider.First();
                var file2 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("second.json")).First();
                var file3 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("third.json")).First();
                var file4 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fourth.json")).First();
                var file5 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fifth.json")).First();
                var file6 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("sixth.json")).First();
                return file1.MultiCombine(file2, file3, file4, file5, file6);
            });
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([
                new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }"),
                new TestAdditionalFile("second.json", "{ \"myGenerator\": \"2\" }"),
                new TestAdditionalFile("third.json", "{ \"myGenerator\": \"3\" }"),
                new TestAdditionalFile("fourth.json", "{ \"myGenerator\": \"4\" }"),
                new TestAdditionalFile("fifth.json", "{ \"myGenerator\": \"5\" }"),
                new TestAdditionalFile("sixth.json", "{ \"myGenerator\": \"6\" }")
            ]);
        var compilation = CSharpCompilation.Create(nameof(testGenerator));

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.Item1.Path.Should().Be("first.json");
        testGenerator.Source.Item2.Path.Should().Be("second.json");
        testGenerator.Source.Item3.Path.Should().Be("third.json");
        testGenerator.Source.Item4.Path.Should().Be("fourth.json");
        testGenerator.Source.Item5.Path.Should().Be("fifth.json");
        testGenerator.Source.Item6.Path.Should().Be("sixth.json");
    }

    [Fact]
    public void MultiCombineOfT7_WhenCalled_ThenCombinesInToOneTuple()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<(AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText,
                AdditionalText, AdditionalText)>(c =>
            {
                var file1 = c.AdditionalTextsProvider.First();
                var file2 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("second.json")).First();
                var file3 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("third.json")).First();
                var file4 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fourth.json")).First();
                var file5 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fifth.json")).First();
                var file6 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("sixth.json")).First();
                var file7 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("seventh.json"))
                    .First();
                return file1.MultiCombine(file2, file3, file4, file5, file6, file7);
            });
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([
                new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }"),
                new TestAdditionalFile("second.json", "{ \"myGenerator\": \"2\" }"),
                new TestAdditionalFile("third.json", "{ \"myGenerator\": \"3\" }"),
                new TestAdditionalFile("fourth.json", "{ \"myGenerator\": \"4\" }"),
                new TestAdditionalFile("fifth.json", "{ \"myGenerator\": \"5\" }"),
                new TestAdditionalFile("sixth.json", "{ \"myGenerator\": \"6\" }"),
                new TestAdditionalFile("seventh.json", "{ \"myGenerator\": \"7\" }")
            ]);
        var compilation = CSharpCompilation.Create(nameof(testGenerator));

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        //assert
        testGenerator.Provider.Should().NotBeNull();
        testGenerator.Source.Should().NotBeNull();
        testGenerator.Source.Item1.Path.Should().Be("first.json");
        testGenerator.Source.Item2.Path.Should().Be("second.json");
        testGenerator.Source.Item3.Path.Should().Be("third.json");
        testGenerator.Source.Item4.Path.Should().Be("fourth.json");
        testGenerator.Source.Item5.Path.Should().Be("fifth.json");
        testGenerator.Source.Item6.Path.Should().Be("sixth.json");
        testGenerator.Source.Item7.Path.Should().Be("seventh.json");
    }
}