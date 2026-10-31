using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Providers;

public static class IncrementalValueProviderExtensions
{
    extension<T1>(IncrementalValueProvider<T1> provider1)
    {
        public IncrementalValueProvider<(T1 First, T2 Second, T3 Third)> MultiCombine<T2, T3>
        (IncrementalValueProvider<T2> provider2,
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

        public IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth)> MultiCombine<T2, T3, T4>
        (IncrementalValueProvider<T2> provider2,
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

        public IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth)> MultiCombine<T2,
            T3, T4, T5>
        (IncrementalValueProvider<T2> provider2,
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

        public IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth, T6 Sixth)> MultiCombine<T2,
            T3, T4, T5, T6>
        (IncrementalValueProvider<T2> provider2,
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

        public IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth, T6 Sixth, T7 Seventh)>
            MultiCombine<T2, T3, T4, T5, T6, T7>
            (IncrementalValueProvider<T2> provider2,
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

        public IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth, T6 Sixth, T7 Seventh, T8 Eighth)>
            MultiCombine<T2, T3, T4, T5, T6, T7, T8>
            (IncrementalValueProvider<T2> provider2,
                IncrementalValueProvider<T3> provider3,
                IncrementalValueProvider<T4> provider4,
                IncrementalValueProvider<T5> provider5,
                IncrementalValueProvider<T6> provider6,
                IncrementalValueProvider<T7> provider7,
                IncrementalValueProvider<T8> provider8
                ) =>
            provider1.Combine(provider2)
                .Combine(provider3)
                .Combine(provider4)
                .Combine(provider5)
                .Combine(provider6)
                .Combine(provider7)
                .Combine(provider8)
                .Select((c, _) =>
                    (
                        c.Left.Left.Left.Left.Left.Left.Left,
                        c.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Right,
                        c.Left.Left.Right,
                        c.Left.Right,
                        c.Right
                    )
                );

        public IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth, T6 Sixth, T7 Seventh, T8 Eighth, T9 Ninth)>
            MultiCombine<T2, T3, T4, T5, T6, T7, T8, T9>
            (IncrementalValueProvider<T2> provider2,
                IncrementalValueProvider<T3> provider3,
                IncrementalValueProvider<T4> provider4,
                IncrementalValueProvider<T5> provider5,
                IncrementalValueProvider<T6> provider6,
                IncrementalValueProvider<T7> provider7,
                IncrementalValueProvider<T8> provider8,
                IncrementalValueProvider<T9> provider9
                ) =>
            provider1.Combine(provider2)
                .Combine(provider3)
                .Combine(provider4)
                .Combine(provider5)
                .Combine(provider6)
                .Combine(provider7)
                .Combine(provider8)
                .Combine(provider9)
                .Select((c, _) =>
                    (
                        c.Left.Left.Left.Left.Left.Left.Left.Left,
                        c.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Right,
                        c.Left.Left.Right,
                        c.Left.Right,
                        c.Right
                    )
                );

        public IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth, T6 Sixth, T7 Seventh, T8 Eighth, T9 Ninth, T10 Tenth)>
            MultiCombine<T2, T3, T4, T5, T6, T7, T8, T9, T10>
            (IncrementalValueProvider<T2> provider2,
                IncrementalValueProvider<T3> provider3,
                IncrementalValueProvider<T4> provider4,
                IncrementalValueProvider<T5> provider5,
                IncrementalValueProvider<T6> provider6,
                IncrementalValueProvider<T7> provider7,
                IncrementalValueProvider<T8> provider8,
                IncrementalValueProvider<T9> provider9,
                IncrementalValueProvider<T10> provider10
                ) =>
            provider1.Combine(provider2)
                .Combine(provider3)
                .Combine(provider4)
                .Combine(provider5)
                .Combine(provider6)
                .Combine(provider7)
                .Combine(provider8)
                .Combine(provider9)
                .Combine(provider10)
                .Select((c, _) =>
                    (
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Right,
                        c.Left.Left.Right,
                        c.Left.Right,
                        c.Right
                    )
                );

        public IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth, T6 Sixth, T7 Seventh, T8 Eighth, T9 Ninth, T10 Tenth, T11 Eleventh)>
            MultiCombine<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
            (IncrementalValueProvider<T2> provider2,
                IncrementalValueProvider<T3> provider3,
                IncrementalValueProvider<T4> provider4,
                IncrementalValueProvider<T5> provider5,
                IncrementalValueProvider<T6> provider6,
                IncrementalValueProvider<T7> provider7,
                IncrementalValueProvider<T8> provider8,
                IncrementalValueProvider<T9> provider9,
                IncrementalValueProvider<T10> provider10,
                IncrementalValueProvider<T11> provider11
                ) =>
            provider1.Combine(provider2)
                .Combine(provider3)
                .Combine(provider4)
                .Combine(provider5)
                .Combine(provider6)
                .Combine(provider7)
                .Combine(provider8)
                .Combine(provider9)
                .Combine(provider10)
                .Combine(provider11)
                .Select((c, _) =>
                    (
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Right,
                        c.Left.Left.Right,
                        c.Left.Right,
                        c.Right
                    )
                );

        public IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth, T6 Sixth, T7 Seventh, T8 Eighth, T9 Ninth, T10 Tenth, T11 Eleventh, T12 Twelfth)>
            MultiCombine<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
            (IncrementalValueProvider<T2> provider2,
                IncrementalValueProvider<T3> provider3,
                IncrementalValueProvider<T4> provider4,
                IncrementalValueProvider<T5> provider5,
                IncrementalValueProvider<T6> provider6,
                IncrementalValueProvider<T7> provider7,
                IncrementalValueProvider<T8> provider8,
                IncrementalValueProvider<T9> provider9,
                IncrementalValueProvider<T10> provider10,
                IncrementalValueProvider<T11> provider11,
                IncrementalValueProvider<T12> provider12
                ) =>
            provider1.Combine(provider2)
                .Combine(provider3)
                .Combine(provider4)
                .Combine(provider5)
                .Combine(provider6)
                .Combine(provider7)
                .Combine(provider8)
                .Combine(provider9)
                .Combine(provider10)
                .Combine(provider11)
                .Combine(provider12)
                .Select((c, _) =>
                    (
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Right,
                        c.Left.Left.Right,
                        c.Left.Right,
                        c.Right
                    )
                );

        public IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth, T6 Sixth, T7 Seventh, T8 Eighth, T9 Ninth, T10 Tenth, T11 Eleventh, T12 Twelfth, T13 Thirteenth)>
            MultiCombine<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
            (IncrementalValueProvider<T2> provider2,
                IncrementalValueProvider<T3> provider3,
                IncrementalValueProvider<T4> provider4,
                IncrementalValueProvider<T5> provider5,
                IncrementalValueProvider<T6> provider6,
                IncrementalValueProvider<T7> provider7,
                IncrementalValueProvider<T8> provider8,
                IncrementalValueProvider<T9> provider9,
                IncrementalValueProvider<T10> provider10,
                IncrementalValueProvider<T11> provider11,
                IncrementalValueProvider<T12> provider12,
                IncrementalValueProvider<T13> provider13
                ) =>
            provider1.Combine(provider2)
                .Combine(provider3)
                .Combine(provider4)
                .Combine(provider5)
                .Combine(provider6)
                .Combine(provider7)
                .Combine(provider8)
                .Combine(provider9)
                .Combine(provider10)
                .Combine(provider11)
                .Combine(provider12)
                .Combine(provider13)
                .Select((c, _) =>
                    (
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Right,
                        c.Left.Left.Right,
                        c.Left.Right,
                        c.Right
                    )
                );

        public IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth, T6 Sixth, T7 Seventh, T8 Eighth, T9 Ninth, T10 Tenth, T11 Eleventh, T12 Twelfth, T13 Thirteenth, T14 Fourteenth)>
            MultiCombine<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
            (IncrementalValueProvider<T2> provider2,
                IncrementalValueProvider<T3> provider3,
                IncrementalValueProvider<T4> provider4,
                IncrementalValueProvider<T5> provider5,
                IncrementalValueProvider<T6> provider6,
                IncrementalValueProvider<T7> provider7,
                IncrementalValueProvider<T8> provider8,
                IncrementalValueProvider<T9> provider9,
                IncrementalValueProvider<T10> provider10,
                IncrementalValueProvider<T11> provider11,
                IncrementalValueProvider<T12> provider12,
                IncrementalValueProvider<T13> provider13,
                IncrementalValueProvider<T14> provider14
                ) =>
            provider1.Combine(provider2)
                .Combine(provider3)
                .Combine(provider4)
                .Combine(provider5)
                .Combine(provider6)
                .Combine(provider7)
                .Combine(provider8)
                .Combine(provider9)
                .Combine(provider10)
                .Combine(provider11)
                .Combine(provider12)
                .Combine(provider13)
                .Combine(provider14)
                .Select((c, _) =>
                    (
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Right,
                        c.Left.Left.Right,
                        c.Left.Right,
                        c.Right
                    )
                );

        public IncrementalValueProvider<(T1 First, T2 Second, T3 Third, T4 Fourth, T5 Fifth, T6 Sixth, T7 Seventh, T8 Eighth, T9 Ninth, T10 Tenth, T11 Eleventh, T12 Twelfth, T13 Thirteenth, T14 Fourteenth, T15 Fifteenth)>
            MultiCombine<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
            (IncrementalValueProvider<T2> provider2,
                IncrementalValueProvider<T3> provider3,
                IncrementalValueProvider<T4> provider4,
                IncrementalValueProvider<T5> provider5,
                IncrementalValueProvider<T6> provider6,
                IncrementalValueProvider<T7> provider7,
                IncrementalValueProvider<T8> provider8,
                IncrementalValueProvider<T9> provider9,
                IncrementalValueProvider<T10> provider10,
                IncrementalValueProvider<T11> provider11,
                IncrementalValueProvider<T12> provider12,
                IncrementalValueProvider<T13> provider13,
                IncrementalValueProvider<T14> provider14,
                IncrementalValueProvider<T15> provider15
                ) =>
            provider1.Combine(provider2)
                .Combine(provider3)
                .Combine(provider4)
                .Combine(provider5)
                .Combine(provider6)
                .Combine(provider7)
                .Combine(provider8)
                .Combine(provider9)
                .Combine(provider10)
                .Combine(provider11)
                .Combine(provider12)
                .Combine(provider13)
                .Combine(provider14)
                .Combine(provider15)
                .Select((c, _) =>
                    (
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Left.Right,
                        c.Left.Left.Left.Right,
                        c.Left.Left.Right,
                        c.Left.Right,
                        c.Right
                    )
                );
    }
}