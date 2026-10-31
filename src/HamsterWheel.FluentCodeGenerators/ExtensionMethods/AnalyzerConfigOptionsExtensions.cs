using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis.Diagnostics;

namespace HamsterWheel.FluentCodeGenerators;

public static class AnalyzerConfigOptionsExtensions
{
    public const string RootNamespaceKey = "build_property.rootnamespace";

    public static Namespace? GetRootNamespace(this AnalyzerConfigOptions options) =>
        options.TryGetValue(RootNamespaceKey, out var rootNamespace)
            ? rootNamespace.ToNamespace()
            : null;
}