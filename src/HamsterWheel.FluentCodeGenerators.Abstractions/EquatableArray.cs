using System.Collections;
using System.Collections.Immutable;

namespace HamsterWheel.FluentCodeGenerators;

// ReSharper disable once UnusedType.Global //public API
public readonly struct EquatableArray<T>(ImmutableArray<T> array) : IEquatable<EquatableArray<T>>, IReadOnlyList<T>
    where T : IEquatable<T>
{
    public static implicit operator EquatableArray<T>(ImmutableArray<T> array) => new(array);

    public static implicit operator EquatableArray<T>(T[]? array) =>
        new(array is null ? ImmutableArray<T>.Empty : [.. array]);

    private ImmutableArray<T> Items => array.IsDefault ? ImmutableArray<T>.Empty : array;

    public int Length => Items.Length;

    public int Count => Items.Length;

    public T this[int index] => Items[index];

    public bool Equals(EquatableArray<T> other) =>
        Items.Length == other.Items.Length &&
        !Items.Where((t, i) => !EqualityComparer<T>.Default.Equals(t, other.Items[i])).Any();

    public override bool Equals(object? obj) => obj is EquatableArray<T> other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            return Enumerable.Aggregate(Items, 17, (current, item) => (current * 31) + (item?.GetHashCode() ?? 0));
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static bool operator ==(EquatableArray<T> left, EquatableArray<T> right) => left.Equals(right);

    public static bool operator !=(EquatableArray<T> left, EquatableArray<T> right) => !left.Equals(right);

    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)Items).GetEnumerator();
}