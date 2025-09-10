using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Tokens;

public class INameInNamespaceTokenExtensionsUnitTests
{
    [Fact]
    public void Append_WhenCalledWithoutNamespace_ThenAppendsSuffix()
    {
        //arrange
        //act
        var actual = new PascalCaseName("MyClass").InNamespace("MyNamespace".ToNamespace()).Append("AndYours");

        //assert
        actual.ToString().Should().Be("MyClassAndYours");
        actual.Namespace.ToString().Should().Be("MyNamespace");
    }
}