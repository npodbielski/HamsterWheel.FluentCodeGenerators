using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface ISourceCodeFileContext : IContext
{
    ISourceCodeFileContext AddPragma(Action<IPragmaContext> configure);
    ISourceCodeFileContext WithAssemblyAttribute(Action<ISingleAttributeContext> configure);

    IFileScopedNamespaceContext WithFileScopedNamespace(INamespace @namespace);
    ISourceCodeFileContext AddExternAlias(string alias);
}