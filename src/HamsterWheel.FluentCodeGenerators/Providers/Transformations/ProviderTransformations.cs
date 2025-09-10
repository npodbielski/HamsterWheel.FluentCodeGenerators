using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Providers.Transformations;

public static class ProviderTransformations
{
    public static IncrementalValueProvider<T> First<T>(this IncrementalValuesProvider<T> source) =>
        source.Collect().Select((c, _) => c.First());

    public static IncrementalValueProvider<T> FirstOrDefault<T>
        (this IncrementalValuesProvider<T> source, T defaultVal) =>
        source.Collect().Select((c, _) => c.FirstOrDefault() ?? defaultVal);
}