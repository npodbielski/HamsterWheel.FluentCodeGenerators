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
        this IncrementalValueProvider<AnalyzerConfigOptionsProvider> provider, string fallbackNamespace) =>
        provider.Select((a, _) => a.GlobalOptions.GetRootNamespace() ?? fallbackNamespace.ToNamespace());

    public static IncrementalValueProvider<Namespace?> GetRootNamespace(
        this IncrementalValueProvider<AnalyzerConfigOptionsProvider> provider) =>
        provider.Select((a, _) => a.GlobalOptions.GetRootNamespace());
}