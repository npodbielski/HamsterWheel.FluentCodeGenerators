using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.UnitTests;

public class INameInNamespaceTokenExtensionsUnitTests
{
    [Fact]
    public void GetFullName_WhenCalled_ThenReturnsCorrectData()
    {
        //arrange
        var expected = "HamsterWheel.FluentCodeGenerators.INameInNamespaceTokenExtensions";

        //act
        var actual = new NameInNamespace(nameof(INameInNamespaceTokenExtensions).ToPascalCaseName(),
            "HamsterWheel.FluentCodeGenerators".ToNamespace()).GetFullName();

        //assert
        actual.Should().Be(expected);
    }
}