using System.Reflection;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Chunks;

public class DelimiterUnitTests
{
    [Fact]
    public void Ctor_WhenCalledWithNull_Then()
    {
        //arrange
        var privateCtor = typeof(Delimiter).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault().Should()
            .NotBeNull().And.Subject;
        var action = () => privateCtor.Invoke([null, null]);

        //act
        var actual = action.Should().Throw<TargetInvocationException>();

        //assert
        actual.WithInnerExceptionExactly<ArgumentException>().WithMessage("*parameter needs to be set!");
    }
}