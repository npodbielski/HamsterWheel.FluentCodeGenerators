using System.Collections.Immutable;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Generators;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Demo;

public class DemoSourceCodeGenerator(
    SourceProductionContext sourceProductionContext,
    ImmutableArray<AdditionalText> files)
    : SourceCodeFileGeneratorBase(sourceProductionContext, "Demo", "Demo".ToNamespace())
{
    protected override void Configure(IFileScopedNamespaceContext context)
    {
        foreach (var file in files)
        {
            context.WithClass(Path.GetFileNameWithoutExtension(file.Path),
                c => c.WithMethod("Log",
                    m => m.WithBody(b => b.Append($"Console.WriteLine({file.GetTextAsString().TripleQuote()});"))));
        }
    }
}