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