using HamsterWheel.Data;
using Microsoft.CodeAnalysis.Diagnostics;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Dummies;

public class DummyAnalyzerOptions(Map map) : AnalyzerConfigOptions
{
    private readonly Dictionary<string, string> _options = map.ToDictionary();

    public override bool TryGetValue(string key, out string value) => _options.TryGetValue(key, out value!);
}