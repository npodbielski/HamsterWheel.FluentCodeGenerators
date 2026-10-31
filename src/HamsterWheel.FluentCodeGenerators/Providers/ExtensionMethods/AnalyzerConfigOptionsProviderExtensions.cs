using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace HamsterWheel.FluentCodeGenerators.Providers;

public static class AnalyzerConfigOptionsProviderExtensions
{
    extension(IncrementalValueProvider<AnalyzerConfigOptionsProvider> provider)
    {
        public IncrementalValueProvider<AnalyzerConfigOptions> GetGlobalOptions() =>
            provider.Select((c, _) => c.GlobalOptions);

        public IncrementalValueProvider<Namespace> GetRootNamespace(string fallbackNamespace) =>
            provider.Select((a, _) => a.GlobalOptions.GetRootNamespace() ?? fallbackNamespace.ToNamespace());

        public IncrementalValueProvider<Namespace?> GetRootNamespace() =>
            provider.Select((a, _) => a.GlobalOptions.GetRootNamespace());
    }
}