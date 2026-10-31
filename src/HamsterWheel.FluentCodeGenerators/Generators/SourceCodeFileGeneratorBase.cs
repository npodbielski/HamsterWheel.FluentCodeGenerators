using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Generators;

public abstract class SourceCodeFileGeneratorBase(
    SourceProductionContext sourceProductionContext,
    string directory,
    Namespace fileNamespace)
    : ISourceCodeFileGenerator
{
    protected SourceCodeFileGeneratorBase(SourceProductionContext sourceProductionContext, string directory,
        string fileNamespace) : this(sourceProductionContext, directory, fileNamespace.ToNamespace())
    {
    }

    public void GenerateAndAdd()
    {
        var context = GetContext(fileNamespace);
        Configure(context);
        var codeFile = context.GetCodeFile();
        sourceProductionContext.AddSource(
            $"{Path.Combine(directory, codeFile.firstTypeName?.NameAsString ?? GetType().Name)}.g", codeFile.content);
    }

    protected virtual IFileScopedNamespaceContext GetContext(INamespace @namespace) =>
        SourceCodeFileContext.NewWithNullabilityAndFileScopeNamespace(@namespace);

    protected abstract void Configure(IFileScopedNamespaceContext context);
}