using System.Collections.Immutable;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Generators;
using HamsterWheel.FluentCodeGenerators.Providers.Transformations;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Demo;

[Generator(LanguageNames.CSharp)]
public class DemoIncrementalGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var additionalFilesProvider = context.AdditionalTextsProvider
            .Where(AdditionalTextPredicates.FileNameExtensionIs(".txt")).Collect();
        context.RegisterSourceOutput(additionalFilesProvider,
            (pc, files) => new DemoSourceCodeGenerator(pc, files).GenerateAndAdd());
    }
}

public class DemoSourceCodeGenerator(
    SourceProductionContext sourceProductionContext,
    ImmutableArray<AdditionalText> files)
    : SourceCodeFileGeneratorBase(sourceProductionContext, "Demo", "Demo".ToNamespace())
{
    protected override void Configure(IFileScopedNamespaceContext context)
    {
        foreach (var file in files)
        {
            context.WithClass(Path.GetFileName(file.Path),
                c => c.WithMethod("Log",
                    m => m.WithBody(b => b.Append($"Console.WriteLine({file.GetTextAsString().TripleQuote()});"))));
        }
    }
}