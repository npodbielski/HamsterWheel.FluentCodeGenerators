using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Providers.Transformations;
using HamsterWheel.FluentCodeGenerators.UnitTests.Dummies;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Providers.Transformations;

public class AdditionalTextSelectorsUnitTests
{
    [Fact]
    public void GetFileContent_WhenCalledOnAdditionalFile_ThenReturnsContent()
    {
        //arrange
        const string expected = "Compilation should have an access to this content!";
        var additionalFile = new TestAdditionalFile("/root/files/compilation.txt", expected);

        //act
        var content = AdditionalTextSelectors.FileContent(additionalFile, CancellationToken.None);

        //assert
        content.Should().Be(expected);
    }

    [Fact]
    public void ContentToEnum_WhenCalledOnContentThatIsValid_ThenReturnsEnumValue()
    {
        //arrange
        var expected = $"{nameof(HttpKeepAlivePingPolicy.WithActiveRequests)}";

        //act
        var content = AdditionalTextSelectors.ContentToEnum<HttpKeepAlivePingPolicy>(expected, CancellationToken.None);

        //assert
        content.Should().Be(HttpKeepAlivePingPolicy.WithActiveRequests);
    }

    [Fact]
    public void GetFileNameAndContent_WhenCalledOnAdditionalFile_ThenReturnsFileNameAndContent()
    {
        //arrange
        const string content = "This is file content";
        var additionalFile = new TestAdditionalFile("/root/files/compilation.txt", content);

        //act
        var data = AdditionalTextSelectors.FileNamePathAndContent(additionalFile, CancellationToken.None);

        //assert
        data.Should().NotBeNull();
        data.FilePath.Should().Be(additionalFile.Path);
        data.Content.Should().Be(content);
    }
}