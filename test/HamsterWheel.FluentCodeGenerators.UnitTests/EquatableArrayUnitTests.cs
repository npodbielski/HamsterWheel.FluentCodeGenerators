using System.Collections;
using System.Collections.Immutable;
using FluentAssertions;

namespace HamsterWheel.FluentCodeGenerators.UnitTests;

public class EquatableArrayUnitTests
{
    [Fact]
    public void Ctor_WhenCalledWithImmutableArray_ThenStoresValues()
    {
        //arrange
        var array = ImmutableArray.Create(1, 2, 3);

        //act
        var equatable = new EquatableArray<int>(array);

        //assert
        equatable.Length.Should().Be(3);
        equatable[0].Should().Be(1);
        equatable[1].Should().Be(2);
        equatable[2].Should().Be(3);
    }

    [Fact]
    public void ImplicitOperator_WhenCalledWithImmutableArray_ThenCreatesEquatableArray()
    {
        //arrange
        var array = ImmutableArray.Create("a", "b");

        //act
        EquatableArray<string> equatable = array;

        //assert
        equatable.Length.Should().Be(2);
        equatable[0].Should().Be("a");
    }

    [Fact]
    public void ImplicitOperator_WhenCalledWithNullArray_ThenCreatesEmptyEquatableArray()
    {
        //arrange
        int[]? array = null;

        //act
        EquatableArray<int> equatable = array;

        //assert
        equatable.Length.Should().Be(0);
    }

    [Fact]
    public void ImplicitOperator_WhenCalledWithEmptyArray_ThenCreatesEmptyEquatableArray()
    {
        //arrange
        int[]? array = [];

        //act
        EquatableArray<int> equatable = array;

        //assert
        equatable.Length.Should().Be(0);
    }

    [Fact]
    public void ImplicitOperator_WhenCalledWithNonNullArray_ThenCreatesEquatableArray()
    {
        //arrange
        string[]? array = ["x", "y", "z"];

        //act
        EquatableArray<string> equatable = array;

        //assert
        equatable.Length.Should().Be(3);
        equatable[2].Should().Be("z");
    }

    [Fact]
    public void Count_WhenCalled_ThenReturnsSameAsLength()
    {
        //arrange
        var equatable = new EquatableArray<int>(ImmutableArray.Create(1, 2));

        //act & assert
        equatable.Count.Should().Be(equatable.Length);
    }

    [Fact]
    public void Equals_WhenCalledWithSameValues_ThenReturnsTrue()
    {
        //arrange
        var a = new EquatableArray<int>(ImmutableArray.Create(1, 2, 3));
        var b = new EquatableArray<int>(ImmutableArray.Create(1, 2, 3));

        //act & assert
        a.Equals(b).Should().BeTrue();
    }

    [Fact]
    public void Equals_WhenCalledWithDifferentValues_ThenReturnsFalse()
    {
        //arrange
        var a = new EquatableArray<int>(ImmutableArray.Create(1, 2, 3));
        var b = new EquatableArray<int>(ImmutableArray.Create(1, 2, 4));

        //act & assert
        a.Equals(b).Should().BeFalse();
    }

    [Fact]
    public void Equals_WhenCalledWithDifferentLengths_ThenReturnsFalse()
    {
        //arrange
        var a = new EquatableArray<int>(ImmutableArray.Create(1, 2));
        var b = new EquatableArray<int>(ImmutableArray.Create(1, 2, 3));

        //act & assert
        a.Equals(b).Should().BeFalse();
    }

    [Fact]
    public void Equals_WhenCalledWithNull_ThenReturnsFalse()
    {
        //arrange
        var a = new EquatableArray<int>(ImmutableArray.Create(1));

        //act & assert
        a.Equals((object?)null).Should().BeFalse();
    }

    [Fact]
    public void Equals_WhenCalledWithDifferentType_ThenReturnsFalse()
    {
        //arrange
        var a = new EquatableArray<int>(ImmutableArray.Create(1));

        //act & assert
        a.Equals("string").Should().BeFalse();
    }

    [Fact]
    public void Equals_Operator_WhenCalledWithSameValues_ThenReturnsTrue()
    {
        //arrange
        var a = new EquatableArray<int>(ImmutableArray.Create(1, 2));
        var b = new EquatableArray<int>(ImmutableArray.Create(1, 2));

        //act & assert
        (a == b).Should().BeTrue();
    }

    [Fact]
    public void NotEquals_Operator_WhenCalledWithDifferentValues_ThenReturnsTrue()
    {
        //arrange
        var a = new EquatableArray<int>(ImmutableArray.Create(1));
        var b = new EquatableArray<int>(ImmutableArray.Create(2));

        //act & assert
        (a != b).Should().BeTrue();
    }

    [Fact]
    public void GetHashCode_WhenCalled_ThenReturnsConsistentHash()
    {
        //arrange
        var a = new EquatableArray<int>(ImmutableArray.Create(1, 2, 3));

        //act & assert
        a.GetHashCode().Should().Be(a.GetHashCode());
    }

    [Fact]
    public void GetHashCode_WhenCalledWithSameValues_ThenReturnsSameHash()
    {
        //arrange
        var a = new EquatableArray<int>(ImmutableArray.Create(1, 2, 3));
        var b = new EquatableArray<int>(ImmutableArray.Create(1, 2, 3));

        //act & assert
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Fact]
    public void GetEnumerator_WhenCalled_ThenIteratesOverElements()
    {
        //arrange
        var equatable = new EquatableArray<int>(ImmutableArray.Create(10, 20, 30));

        //act
        var collected = equatable.ToList();

        //assert
        collected.Should().Equal(10, 20, 30);
    }

    [Fact]
    public void GetEnumerator_NonGeneric_WhenCalled_ThenIteratesOverElements()
    {
        //arrange
        IEnumerable equatable = new EquatableArray<int>(ImmutableArray.Create(5, 15));

        //act
        var enumerator = equatable.GetEnumerator();

        //assert
        enumerator.MoveNext().Should().BeTrue();
        ((int)enumerator.Current).Should().Be(5);
    }

    [Fact]
    public void Indexer_WhenCalledWithDefaultArray_ThenDoesNotThrow()
    {
        //arrange
        EquatableArray<int> equatable = new EquatableArray<int>(default);

        //act & assert
        equatable.Length.Should().Be(0);
    }
}
