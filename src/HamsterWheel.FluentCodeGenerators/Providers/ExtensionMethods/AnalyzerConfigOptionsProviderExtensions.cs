using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace HamsterWheel.FluentCodeGenerators.Providers;

public static class AnalyzerConfigOptionsProviderExtensions
{
    public static IncrementalValueProvider<AnalyzerConfigOptions> GetGlobalOptions(
        this IncrementalValueProvider<AnalyzerConfigOptionsProvider> analyzerConfigOptionsProvider)
        => analyzerConfigOptionsProvider.Select((c, _) => c.GlobalOptions);
}