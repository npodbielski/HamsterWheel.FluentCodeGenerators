using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Providers.Data;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Providers.Data;

public class AttributeTextFileDataUnitTests
{
    [Fact]
    public void FileName_WhenCalled_ThenReturnsCorrectData()
    {
        //arrange
        const string expected = "FileName";
        var sut = new AdditionalTextFileData(expected, "/tmp", "dummy content");

        //act
        var actual = sut.FileName;

        //assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void DirName_WhenCalled_ThenReturnsCorrectData()
    {
        //arrange
        const string expected = "dir1";
        var sut = new AdditionalTextFileData(expected, "/tmp/dir1/somefile.txt", "dummy content");

        //act
        var actual = sut.DirName;

        //assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void DirPath_WhenCalled_ThenReturnsCorrectData()
    {
        //arrange
        const string expected = "/tmp/dir1";
        var sut = new AdditionalTextFileData(expected, "/tmp/dir1/somefile.txt", "dummy content");

        //act
        var actual = sut.DirPath;

        //assert
        actual.Should().Be(expected);
    }
}