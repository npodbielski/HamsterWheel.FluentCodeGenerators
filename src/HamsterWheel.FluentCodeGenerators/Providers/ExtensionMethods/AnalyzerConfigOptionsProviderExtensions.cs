using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace HamsterWheel.FluentCodeGenerators.Providers;

public static class AnalyzerConfigOptionsProviderExtensions
{
    public static IncrementalValueProvider<AnalyzerConfigOptions> GetGlobalOptions(
        this IncrementalValueProvider<AnalyzerConfigOptionsProvider> provider) =>
        provider.Select((c, _) => c.GlobalOptions);

    public static IncrementalValueProvider<Namespace> GetRootNamespace(
        this IncrementalValueProvider<AnalyzerConfigOptionsProvider> provider, string fallbackNamespace)
    {
        return provider.Select((a, _) =>
            a.GlobalOptions.TryGetValue("build_property.rootnamespace", out var projectFileNamespace)
                ? projectFileNamespace.ToNamespace()
                : fallbackNamespace.ToNamespace());
    }
}