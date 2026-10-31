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

    [Fact]
    public void MultiCombineOfT8_WhenCalled_ThenCombinesInToOneTuple()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<(AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText,
                AdditionalText, AdditionalText, AdditionalText)>(c =>
            {
                var file1 = c.AdditionalTextsProvider.First();
                var file2 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("second.json")).First();
                var file3 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("third.json")).First();
                var file4 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fourth.json")).First();
                var file5 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fifth.json")).First();
                var file6 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("sixth.json")).First();
                var file7 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("seventh.json")).First();
                var file8 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("eighth.json")).First();
                return file1.MultiCombine(file2, file3, file4, file5, file6, file7, file8);
            });
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([
                new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }"),
                new TestAdditionalFile("second.json", "{ \"myGenerator\": \"2\" }"),
                new TestAdditionalFile("third.json", "{ \"myGenerator\": \"3\" }"),
                new TestAdditionalFile("fourth.json", "{ \"myGenerator\": \"4\" }"),
                new TestAdditionalFile("fifth.json", "{ \"myGenerator\": \"5\" }"),
                new TestAdditionalFile("sixth.json", "{ \"myGenerator\": \"6\" }"),
                new TestAdditionalFile("seventh.json", "{ \"myGenerator\": \"7\" }"),
                new TestAdditionalFile("eighth.json", "{ \"myGenerator\": \"8\" }")
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
        testGenerator.Source.Item8.Path.Should().Be("eighth.json");
    }

    [Fact]
    public void MultiCombineOfT9_WhenCalled_ThenCombinesInToOneTuple()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<(AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText,
                AdditionalText, AdditionalText, AdditionalText, AdditionalText)>(c =>
            {
                var file1 = c.AdditionalTextsProvider.First();
                var file2 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("second.json")).First();
                var file3 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("third.json")).First();
                var file4 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fourth.json")).First();
                var file5 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fifth.json")).First();
                var file6 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("sixth.json")).First();
                var file7 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("seventh.json")).First();
                var file8 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("eighth.json")).First();
                var file9 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("ninth.json")).First();
                return file1.MultiCombine(file2, file3, file4, file5, file6, file7, file8, file9);
            });
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([
                new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }"),
                new TestAdditionalFile("second.json", "{ \"myGenerator\": \"2\" }"),
                new TestAdditionalFile("third.json", "{ \"myGenerator\": \"3\" }"),
                new TestAdditionalFile("fourth.json", "{ \"myGenerator\": \"4\" }"),
                new TestAdditionalFile("fifth.json", "{ \"myGenerator\": \"5\" }"),
                new TestAdditionalFile("sixth.json", "{ \"myGenerator\": \"6\" }"),
                new TestAdditionalFile("seventh.json", "{ \"myGenerator\": \"7\" }"),
                new TestAdditionalFile("eighth.json", "{ \"myGenerator\": \"8\" }"),
                new TestAdditionalFile("ninth.json", "{ \"myGenerator\": \"9\" }")
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
        testGenerator.Source.Item8.Path.Should().Be("eighth.json");
        testGenerator.Source.Item9.Path.Should().Be("ninth.json");
    }

    [Fact]
    public void MultiCombineOfT10_WhenCalled_ThenCombinesInToOneTuple()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<(AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText,
                AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText)>(c =>
            {
                var file1 = c.AdditionalTextsProvider.First();
                var file2 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("second.json")).First();
                var file3 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("third.json")).First();
                var file4 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fourth.json")).First();
                var file5 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fifth.json")).First();
                var file6 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("sixth.json")).First();
                var file7 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("seventh.json")).First();
                var file8 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("eighth.json")).First();
                var file9 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("ninth.json")).First();
                var file10 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("tenth.json")).First();
                return file1.MultiCombine(file2, file3, file4, file5, file6, file7, file8, file9, file10);
            });
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([
                new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }"),
                new TestAdditionalFile("second.json", "{ \"myGenerator\": \"2\" }"),
                new TestAdditionalFile("third.json", "{ \"myGenerator\": \"3\" }"),
                new TestAdditionalFile("fourth.json", "{ \"myGenerator\": \"4\" }"),
                new TestAdditionalFile("fifth.json", "{ \"myGenerator\": \"5\" }"),
                new TestAdditionalFile("sixth.json", "{ \"myGenerator\": \"6\" }"),
                new TestAdditionalFile("seventh.json", "{ \"myGenerator\": \"7\" }"),
                new TestAdditionalFile("eighth.json", "{ \"myGenerator\": \"8\" }"),
                new TestAdditionalFile("ninth.json", "{ \"myGenerator\": \"9\" }"),
                new TestAdditionalFile("tenth.json", "{ \"myGenerator\": \"10\" }")
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
        testGenerator.Source.Item8.Path.Should().Be("eighth.json");
        testGenerator.Source.Item9.Path.Should().Be("ninth.json");
        testGenerator.Source.Item10.Path.Should().Be("tenth.json");
    }

    [Fact]
    public void MultiCombineOfT11_WhenCalled_ThenCombinesInToOneTuple()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<(AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText,
                AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText)>(c =>
            {
                var file1 = c.AdditionalTextsProvider.First();
                var file2 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("second.json")).First();
                var file3 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("third.json")).First();
                var file4 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fourth.json")).First();
                var file5 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fifth.json")).First();
                var file6 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("sixth.json")).First();
                var file7 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("seventh.json")).First();
                var file8 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("eighth.json")).First();
                var file9 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("ninth.json")).First();
                var file10 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("tenth.json")).First();
                var file11 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("eleventh.json")).First();
                return file1.MultiCombine(file2, file3, file4, file5, file6, file7, file8, file9, file10, file11);
            });
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([
                new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }"),
                new TestAdditionalFile("second.json", "{ \"myGenerator\": \"2\" }"),
                new TestAdditionalFile("third.json", "{ \"myGenerator\": \"3\" }"),
                new TestAdditionalFile("fourth.json", "{ \"myGenerator\": \"4\" }"),
                new TestAdditionalFile("fifth.json", "{ \"myGenerator\": \"5\" }"),
                new TestAdditionalFile("sixth.json", "{ \"myGenerator\": \"6\" }"),
                new TestAdditionalFile("seventh.json", "{ \"myGenerator\": \"7\" }"),
                new TestAdditionalFile("eighth.json", "{ \"myGenerator\": \"8\" }"),
                new TestAdditionalFile("ninth.json", "{ \"myGenerator\": \"9\" }"),
                new TestAdditionalFile("tenth.json", "{ \"myGenerator\": \"10\" }"),
                new TestAdditionalFile("eleventh.json", "{ \"myGenerator\": \"11\" }")
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
        testGenerator.Source.Item8.Path.Should().Be("eighth.json");
        testGenerator.Source.Item9.Path.Should().Be("ninth.json");
        testGenerator.Source.Item10.Path.Should().Be("tenth.json");
        testGenerator.Source.Item11.Path.Should().Be("eleventh.json");
    }

    [Fact]
    public void MultiCombineOfT12_WhenCalled_ThenCombinesInToOneTuple()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<(AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText,
                AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText)>(c =>
            {
                var file1 = c.AdditionalTextsProvider.First();
                var file2 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("second.json")).First();
                var file3 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("third.json")).First();
                var file4 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fourth.json")).First();
                var file5 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fifth.json")).First();
                var file6 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("sixth.json")).First();
                var file7 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("seventh.json")).First();
                var file8 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("eighth.json")).First();
                var file9 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("ninth.json")).First();
                var file10 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("tenth.json")).First();
                var file11 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("eleventh.json")).First();
                var file12 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("twelfth.json")).First();
                return file1.MultiCombine(file2, file3, file4, file5, file6, file7, file8, file9, file10, file11, file12);
            });
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([
                new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }"),
                new TestAdditionalFile("second.json", "{ \"myGenerator\": \"2\" }"),
                new TestAdditionalFile("third.json", "{ \"myGenerator\": \"3\" }"),
                new TestAdditionalFile("fourth.json", "{ \"myGenerator\": \"4\" }"),
                new TestAdditionalFile("fifth.json", "{ \"myGenerator\": \"5\" }"),
                new TestAdditionalFile("sixth.json", "{ \"myGenerator\": \"6\" }"),
                new TestAdditionalFile("seventh.json", "{ \"myGenerator\": \"7\" }"),
                new TestAdditionalFile("eighth.json", "{ \"myGenerator\": \"8\" }"),
                new TestAdditionalFile("ninth.json", "{ \"myGenerator\": \"9\" }"),
                new TestAdditionalFile("tenth.json", "{ \"myGenerator\": \"10\" }"),
                new TestAdditionalFile("eleventh.json", "{ \"myGenerator\": \"11\" }"),
                new TestAdditionalFile("twelfth.json", "{ \"myGenerator\": \"12\" }")
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
        testGenerator.Source.Item8.Path.Should().Be("eighth.json");
        testGenerator.Source.Item9.Path.Should().Be("ninth.json");
        testGenerator.Source.Item10.Path.Should().Be("tenth.json");
        testGenerator.Source.Item11.Path.Should().Be("eleventh.json");
        testGenerator.Source.Item12.Path.Should().Be("twelfth.json");
    }

    [Fact]
    public void MultiCombineOfT13_WhenCalled_ThenCombinesInToOneTuple()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<(AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText,
                AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText)>(c =>
            {
                var file1 = c.AdditionalTextsProvider.First();
                var file2 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("second.json")).First();
                var file3 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("third.json")).First();
                var file4 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fourth.json")).First();
                var file5 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fifth.json")).First();
                var file6 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("sixth.json")).First();
                var file7 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("seventh.json")).First();
                var file8 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("eighth.json")).First();
                var file9 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("ninth.json")).First();
                var file10 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("tenth.json")).First();
                var file11 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("eleventh.json")).First();
                var file12 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("twelfth.json")).First();
                var file13 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("thirteenth.json")).First();
                return file1.MultiCombine(file2, file3, file4, file5, file6, file7, file8, file9, file10, file11, file12, file13);
            });
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([
                new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }"),
                new TestAdditionalFile("second.json", "{ \"myGenerator\": \"2\" }"),
                new TestAdditionalFile("third.json", "{ \"myGenerator\": \"3\" }"),
                new TestAdditionalFile("fourth.json", "{ \"myGenerator\": \"4\" }"),
                new TestAdditionalFile("fifth.json", "{ \"myGenerator\": \"5\" }"),
                new TestAdditionalFile("sixth.json", "{ \"myGenerator\": \"6\" }"),
                new TestAdditionalFile("seventh.json", "{ \"myGenerator\": \"7\" }"),
                new TestAdditionalFile("eighth.json", "{ \"myGenerator\": \"8\" }"),
                new TestAdditionalFile("ninth.json", "{ \"myGenerator\": \"9\" }"),
                new TestAdditionalFile("tenth.json", "{ \"myGenerator\": \"10\" }"),
                new TestAdditionalFile("eleventh.json", "{ \"myGenerator\": \"11\" }"),
                new TestAdditionalFile("twelfth.json", "{ \"myGenerator\": \"12\" }"),
                new TestAdditionalFile("thirteenth.json", "{ \"myGenerator\": \"13\" }")
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
        testGenerator.Source.Item8.Path.Should().Be("eighth.json");
        testGenerator.Source.Item9.Path.Should().Be("ninth.json");
        testGenerator.Source.Item10.Path.Should().Be("tenth.json");
        testGenerator.Source.Item11.Path.Should().Be("eleventh.json");
        testGenerator.Source.Item12.Path.Should().Be("twelfth.json");
        testGenerator.Source.Item13.Path.Should().Be("thirteenth.json");
    }

    [Fact]
    public void MultiCombineOfT14_WhenCalled_ThenCombinesInToOneTuple()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<(AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText,
                AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText)>(c =>
            {
                var file1 = c.AdditionalTextsProvider.First();
                var file2 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("second.json")).First();
                var file3 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("third.json")).First();
                var file4 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fourth.json")).First();
                var file5 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fifth.json")).First();
                var file6 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("sixth.json")).First();
                var file7 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("seventh.json")).First();
                var file8 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("eighth.json")).First();
                var file9 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("ninth.json")).First();
                var file10 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("tenth.json")).First();
                var file11 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("eleventh.json")).First();
                var file12 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("twelfth.json")).First();
                var file13 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("thirteenth.json")).First();
                var file14 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fourteenth.json")).First();
                return file1.MultiCombine(file2, file3, file4, file5, file6, file7, file8, file9, file10, file11, file12, file13, file14);
            });
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([
                new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }"),
                new TestAdditionalFile("second.json", "{ \"myGenerator\": \"2\" }"),
                new TestAdditionalFile("third.json", "{ \"myGenerator\": \"3\" }"),
                new TestAdditionalFile("fourth.json", "{ \"myGenerator\": \"4\" }"),
                new TestAdditionalFile("fifth.json", "{ \"myGenerator\": \"5\" }"),
                new TestAdditionalFile("sixth.json", "{ \"myGenerator\": \"6\" }"),
                new TestAdditionalFile("seventh.json", "{ \"myGenerator\": \"7\" }"),
                new TestAdditionalFile("eighth.json", "{ \"myGenerator\": \"8\" }"),
                new TestAdditionalFile("ninth.json", "{ \"myGenerator\": \"9\" }"),
                new TestAdditionalFile("tenth.json", "{ \"myGenerator\": \"10\" }"),
                new TestAdditionalFile("eleventh.json", "{ \"myGenerator\": \"11\" }"),
                new TestAdditionalFile("twelfth.json", "{ \"myGenerator\": \"12\" }"),
                new TestAdditionalFile("thirteenth.json", "{ \"myGenerator\": \"13\" }"),
                new TestAdditionalFile("fourteenth.json", "{ \"myGenerator\": \"14\" }")
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
        testGenerator.Source.Item8.Path.Should().Be("eighth.json");
        testGenerator.Source.Item9.Path.Should().Be("ninth.json");
        testGenerator.Source.Item10.Path.Should().Be("tenth.json");
        testGenerator.Source.Item11.Path.Should().Be("eleventh.json");
        testGenerator.Source.Item12.Path.Should().Be("twelfth.json");
        testGenerator.Source.Item13.Path.Should().Be("thirteenth.json");
        testGenerator.Source.Item14.Path.Should().Be("fourteenth.json");
    }

    [Fact]
    public void MultiCombineOfT15_WhenCalled_ThenCombinesInToOneTuple()
    {
        //arrange
        var testGenerator =
            new DummyGenerator<(AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText,
                AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText, AdditionalText)>(c =>
            {
                var file1 = c.AdditionalTextsProvider.First();
                var file2 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("second.json")).First();
                var file3 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("third.json")).First();
                var file4 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fourth.json")).First();
                var file5 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fifth.json")).First();
                var file6 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("sixth.json")).First();
                var file7 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("seventh.json")).First();
                var file8 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("eighth.json")).First();
                var file9 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("ninth.json")).First();
                var file10 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("tenth.json")).First();
                var file11 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("eleventh.json")).First();
                var file12 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("twelfth.json")).First();
                var file13 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("thirteenth.json")).First();
                var file14 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fourteenth.json")).First();
                var file15 = c.AdditionalTextsProvider.Where(AdditionalTextPredicates.FileNameIs("fifteenth.json")).First();
                return file1.MultiCombine(file2, file3, file4, file5, file6, file7, file8, file9, file10, file11, file12, file13, file14, file15);
            });
        var driver = CSharpGeneratorDriver.Create(testGenerator)
            .AddAdditionalTexts([
                new TestAdditionalFile("first.json", "{ \"myGenerator\": \"1\" }"),
                new TestAdditionalFile("second.json", "{ \"myGenerator\": \"2\" }"),
                new TestAdditionalFile("third.json", "{ \"myGenerator\": \"3\" }"),
                new TestAdditionalFile("fourth.json", "{ \"myGenerator\": \"4\" }"),
                new TestAdditionalFile("fifth.json", "{ \"myGenerator\": \"5\" }"),
                new TestAdditionalFile("sixth.json", "{ \"myGenerator\": \"6\" }"),
                new TestAdditionalFile("seventh.json", "{ \"myGenerator\": \"7\" }"),
                new TestAdditionalFile("eighth.json", "{ \"myGenerator\": \"8\" }"),
                new TestAdditionalFile("ninth.json", "{ \"myGenerator\": \"9\" }"),
                new TestAdditionalFile("tenth.json", "{ \"myGenerator\": \"10\" }"),
                new TestAdditionalFile("eleventh.json", "{ \"myGenerator\": \"11\" }"),
                new TestAdditionalFile("twelfth.json", "{ \"myGenerator\": \"12\" }"),
                new TestAdditionalFile("thirteenth.json", "{ \"myGenerator\": \"13\" }"),
                new TestAdditionalFile("fourteenth.json", "{ \"myGenerator\": \"14\" }"),
                new TestAdditionalFile("fifteenth.json", "{ \"myGenerator\": \"15\" }")
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
        testGenerator.Source.Item8.Path.Should().Be("eighth.json");
        testGenerator.Source.Item9.Path.Should().Be("ninth.json");
        testGenerator.Source.Item10.Path.Should().Be("tenth.json");
        testGenerator.Source.Item11.Path.Should().Be("eleventh.json");
        testGenerator.Source.Item12.Path.Should().Be("twelfth.json");
        testGenerator.Source.Item13.Path.Should().Be("thirteenth.json");
        testGenerator.Source.Item14.Path.Should().Be("fourteenth.json");
        testGenerator.Source.Item15.Path.Should().Be("fifteenth.json");
    }
}