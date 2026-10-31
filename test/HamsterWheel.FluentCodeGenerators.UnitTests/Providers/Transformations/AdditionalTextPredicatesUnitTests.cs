using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Providers.Transformations;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Providers.Transformations;

public class AdditionalTextPredicatesUnitTests
{
    [Theory]
    [InlineData("main", "main", true)]
    [InlineData("files", "main", false)]
    public void InDirectory_WhenCalledOnAdditionalFile_ThenReturnsTrueIfDirectoriesMatch(string fileDir,
        string predicateDir, bool matches)
    {
        //arrange
        var testAdditionalFile = new TestAdditionalFile($"/root/{fileDir}/myfile.txt", "You should not see this!");
        var predicate = AdditionalTextPredicates.InDirectory(predicateDir);

        //act
        var actual = predicate(testAdditionalFile);

        //assert
        actual.Should().Be(matches);
    }

    [Theory]
    [InlineData("main", "main", true)]
    [InlineData("files", "main", false)]
    public void InSecondLevelDirectory_WhenCalledOnAdditionalFile_ThenReturnsTrueIfDirectoriesMatch(string fileDir,
        string predicateDir, bool matches)
    {
        //arrange
        var testAdditionalFile = new TestAdditionalFile($"/{fileDir}/main/myfile.txt", "You should not see this!");
        var predicate = AdditionalTextPredicates.InSecondLevelDirectory(predicateDir);

        //act
        var actual = predicate(testAdditionalFile);

        //assert
        actual.Should().Be(matches);
    }

    [Theory]
    [InlineData("myfile.txt", "myfile.txt", true)]
    [InlineData("otherfile.txt", "lookingFor.json", false)]
    public void FileNameIs_WhenCalledOnAdditionalFile_ThenReturnsTrueIfNamesMatch(string fileName,
        string predicateFileName, bool matches)
    {
        //arrange
        var testAdditionalFile = new TestAdditionalFile($"/root/main/{fileName}", "You should not see this!");
        var predicate = AdditionalTextPredicates.FileNameIs(predicateFileName);

        //act
        var actual = predicate(testAdditionalFile);

        //assert
        actual.Should().Be(matches);
    }

    [Theory]
    [InlineData("myfile.txt", ".txt", true)]
    [InlineData("otherfile.txt", ".json", false)]
    public void FileNameExtensionIs_WhenCalledOnAdditionalFile_ThenReturnsTrueIfExtensionsMatch(string fileName,
        string predicateFileName, bool matches)
    {
        //arrange
        var testAdditionalFile = new TestAdditionalFile($"/root/main/{fileName}", "You should not see this!");
        var predicate = AdditionalTextPredicates.FileNameExtensionIs(predicateFileName);

        //act
        var actual = predicate(testAdditionalFile);

        //assert
        actual.Should().Be(matches);
    }

    [Theory]
    [InlineData("myfile.txt", ".txt", true)]
    [InlineData("myfile.txt", "file.txt", true)]
    [InlineData("myfile.txt", "yourfile.txt", false)]
    [InlineData("otherfile.txt", "something.json", false)]
    public void FileNameEndsWith_WhenCalledOnAdditionalFile_ThenReturnsTrueIfExtensionsMatch(string fileName,
        string predicateFileName, bool matches)
    {
        //arrange
        var testAdditionalFile = new TestAdditionalFile($"/root/main/{fileName}", "You should not see this!");
        var predicate = AdditionalTextPredicates.FileNameEndsWith(predicateFileName);

        //act
        var actual = predicate(testAdditionalFile);

        //assert
        actual.Should().Be(matches);
    }
}