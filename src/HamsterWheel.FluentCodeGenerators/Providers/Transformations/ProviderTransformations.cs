using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Providers.Transformations;

public static class ProviderTransformations
{
    extension<T>(IncrementalValuesProvider<T> source)
    {
        public IncrementalValueProvider<T> First() =>
            source.Collect().Select((c, _) => c.First());

        public IncrementalValueProvider<T> FirstOrDefault(T defaultVal) =>
            source.Collect().Select((c, _) => c.FirstOrDefault() ?? defaultVal);
    }
}