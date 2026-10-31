using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;

public class DummyAnalyzerConfigOptionsProvider(string? @namespace = null) : AnalyzerConfigOptionsProvider
{
    private readonly DummyAnalyzerOptions _options = new([("build_property.rootnamespace", @namespace ?? "HamsterWheel.FluentCodeGenerators.UnitTests")]);

    public override AnalyzerConfigOptions GlobalOptions => _options;

    public override AnalyzerConfigOptions GetOptions(SyntaxTree tree) => _options;

    public override AnalyzerConfigOptions GetOptions(AdditionalText textFile) => _options;
}