using System.Collections.Immutable;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Generators;
using HamsterWheel.FluentCodeGenerators.Providers.Data;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Demo;

public class DemoSourceCodeGenerator(
    SourceProductionContext sourceProductionContext,
    ImmutableArray<AdditionalTextFileData> files)
    : SourceCodeFileGeneratorBase(sourceProductionContext, "Demo", "Demo".ToNamespace())
{
    protected override void Configure(IFileScopedNamespaceContext context)
    {
        foreach (var file in files)
        {
            context.WithClass(Path.GetFileNameWithoutExtension(file.FileName),
                c => c.WithMethod("Log",
                    m => m.WithBody(b => b.Append($"Console.WriteLine({file.Content.TripleQuote()});"))));
        }
    }
}