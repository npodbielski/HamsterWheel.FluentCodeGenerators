using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Exceptions;

namespace HamsterWheel.FluentCodeGenerators.UnitTests;

public class TypeExtensionsUnitTests
{
    [Fact]
    public void IsGenericOf_WhenCalledWithNotGenericType_ThenThrows()
    {
        //arrange
        var action = () => typeof(IEnumerable<int>).IsGenericOf(typeof(int));
        //act
        var actual = action.Should().Throw<ArgumentException>();

        //assert
        actual.WithMessage("*should be generic type*");
    }
}