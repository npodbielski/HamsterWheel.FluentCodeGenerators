using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;

public class DummyGenerator<T>(
    Func<IncrementalGeneratorInitializationContext, IncrementalValueProvider<T>> providerBuilder,
    Action<SourceProductionContext, T>? generatorCallback = null)
    : IIncrementalGenerator
{
    public IncrementalValueProvider<T> Provider { get; set; }
    public T? Source { get; set; }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        Provider = providerBuilder(context);
        context.RegisterSourceOutput(Provider, (context, source) =>
        {
            Source = source;
            generatorCallback?.Invoke(context, source);
        });
    }
}