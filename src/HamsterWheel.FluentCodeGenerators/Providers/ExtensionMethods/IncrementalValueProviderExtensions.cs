using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Providers;

public static class IncrementalValueProviderExtensions
{
    public static IncrementalValueProvider<(T1 First, T2 Second, T3 Third)> MultiCombine<T1, T2, T3>
    (this IncrementalValueProvider<T1> provider1,
        IncrementalValueProvider<T2> provider2,
        IncrementalValueProvider<T3> provider3) =>
        provider1.Combine(provider2)
            .Combine(provider3)
            .Select((c, _) =>
                (
                    c.Left.Left,
                    c.Left.Right,
                    c.Right
                )
            );

    public static IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth)> MultiCombine<T1, T2, T3, T4>
    (this IncrementalValueProvider<T1> provider1,
        IncrementalValueProvider<T2> provider2,
        IncrementalValueProvider<T3> provider3,
        IncrementalValueProvider<T4> provider4) =>
        provider1.Combine(provider2)
            .Combine(provider3)
            .Combine(provider4)
            .Select((c, _) =>
                (
                    c.Left.Left.Left,
                    c.Left.Left.Right,
                    c.Left.Right,
                    c.Right
                )
            );

    public static IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth)> MultiCombine<T1, T2,
        T3, T4, T5>
    (this IncrementalValueProvider<T1> provider1,
        IncrementalValueProvider<T2> provider2,
        IncrementalValueProvider<T3> provider3,
        IncrementalValueProvider<T4> provider4,
        IncrementalValueProvider<T5> provider5) =>
        provider1.Combine(provider2)
            .Combine(provider3)
            .Combine(provider4)
            .Combine(provider5)
            .Select((c, _) =>
                (
                    c.Left.Left.Left.Left,
                    c.Left.Left.Left.Right,
                    c.Left.Left.Right,
                    c.Left.Right,
                    c.Right
                )
            );

    public static IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth, T6 Sixth)> MultiCombine<
        T1, T2, T3, T4, T5, T6>
    (this IncrementalValueProvider<T1> provider1,
        IncrementalValueProvider<T2> provider2,
        IncrementalValueProvider<T3> provider3,
        IncrementalValueProvider<T4> provider4,
        IncrementalValueProvider<T5> provider5,
        IncrementalValueProvider<T6> provider6) =>
        provider1.Combine(provider2)
            .Combine(provider3)
            .Combine(provider4)
            .Combine(provider5)
            .Combine(provider6)
            .Select((c, _) =>
                (
                    c.Left.Left.Left.Left.Left,
                    c.Left.Left.Left.Left.Right,
                    c.Left.Left.Left.Right,
                    c.Left.Left.Right,
                    c.Left.Right,
                    c.Right
                )
            );

    public static IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth, T6 Sixth, T7 Seventh)>
        MultiCombine<T1, T2, T3, T4, T5, T6, T7>
        (this IncrementalValueProvider<T1> provider1,
            IncrementalValueProvider<T2> provider2,
            IncrementalValueProvider<T3> provider3,
            IncrementalValueProvider<T4> provider4,
            IncrementalValueProvider<T5> provider5,
            IncrementalValueProvider<T6> provider6,
            IncrementalValueProvider<T7> provider7) =>
        provider1.Combine(provider2)
            .Combine(provider3)
            .Combine(provider4)
            .Combine(provider5)
            .Combine(provider6)
            .Combine(provider7)
            .Select((c, _) =>
                (
                    c.Left.Left.Left.Left.Left.Left,
                    c.Left.Left.Left.Left.Left.Right,
                    c.Left.Left.Left.Left.Right,
                    c.Left.Left.Left.Right,
                    c.Left.Left.Right,
                    c.Left.Right,
                    c.Right
                )
            );
}