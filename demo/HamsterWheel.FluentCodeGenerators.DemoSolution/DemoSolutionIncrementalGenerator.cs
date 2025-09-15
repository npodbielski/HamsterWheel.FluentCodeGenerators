using HamsterWheel.FluentCodeGenerators.Providers.Transformations;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.DemoSolution;

[Generator(LanguageNames.CSharp)]
public class DemoSolutionIncrementalGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var additionalFilesProvider = context.AdditionalTextsProvider
            .Where(AdditionalTextPredicates.FileNameExtensionIs(".txt")).Collect();
        context.RegisterSourceOutput(additionalFilesProvider,
            (pc, files) => new DemoSolutionSourceCodeGenerator(pc, files).GenerateAndAdd());
    }
}