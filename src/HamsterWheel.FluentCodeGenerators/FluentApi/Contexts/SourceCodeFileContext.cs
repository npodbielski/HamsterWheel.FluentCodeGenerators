using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class SourceCodeFileContext() : CodeBuilderContextBase(new CodeFileChunks()), ISourceCodeFileContext
{
    public ISourceCodeFileContext AddPragma(Action<IPragmaContext> configure)
    {
        var codeChunk = new PragmaChunk();
        FileChunks.AddPragma(codeChunk);
        var context = new PragmaContext(this, codeChunk);
        configure(context);
        return this;
    }

    public ISourceCodeFileContext AddExternAlias(string alias)
    {
        var codeChunk = new ExternAliasChunk(alias);
        FileChunks.AddAlias(codeChunk);
        return this;
    }

    public ISourceCodeFileContext WithAssemblyAttribute(Action<ISingleAttributeContext> configure)
    {
        var chunk = AttributeDefinitionChunk.AssemblyAttribute();
        FileChunks.AddAssemblyAttribute(chunk);
        configure(SingleAttributeContext.From(this, chunk));
        return this;
    }

    public IFileScopedNamespaceContext WithFileScopedNamespace(INamespace @namespace)
    {
        FileChunks.AddFileScopeNamespace(FileScopeNamespaceChunk.From(@namespace));
        return new FileScopedNamespaceContext(this);
    }

    public static ISourceCodeFileContext NewWithNullability() => new SourceCodeFileContext().EnableNullability();

    public static IFileScopedNamespaceContext NewWithNullabilityAndFileScopeNamespace(INamespace @namespace) =>
        new SourceCodeFileContext().EnableNullability().WithFileScopedNamespace(@namespace);
}

public static class SourceCodeFileContextExtensions
{
    extension(ISourceCodeFileContext context)
    {
        public ISourceCodeFileContext WithUsings<T1>() =>
            context.WithUsings(typeof(T1));

        public ISourceCodeFileContext WithUsings<T1, T2>() =>
            context.WithUsings(typeof(T1), typeof(T2));

        public ISourceCodeFileContext WithUsings<T1, T2, T3>() =>
            context.WithUsings(typeof(T1), typeof(T2), typeof(T3));

        public ISourceCodeFileContext WithUsings<T1, T2, T3, T4>() =>
            context.WithUsings(typeof(T1), typeof(T2), typeof(T3), typeof(T4));

        public ISourceCodeFileContext WithUsings<T1, T2, T3, T4, T5>() =>
            context.WithUsings(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));

        public ISourceCodeFileContext WithUsings(params Type[] types)
        {
            foreach (var type in types)
            {
                context.AddUsing(type);
            }

            return context;
        }

        public ISourceCodeFileContext EnableNullability() =>
            context.AddPragma(p => p.Nullability(n => n.Enable()));

        public IFileScopedNamespaceContext WithFileScopedNamespace(string @namespace) =>
            context.WithFileScopedNamespace(@namespace.ToNamespace());

        public ISourceCodeFileContext ConfigureUsing<T>()
            where T : IContextConfigurator<ISourceCodeFileContext>, new() => context.ConfigureUsing(new T());

        public ISourceCodeFileContext WithAssemblyAttribute<TAttr>(Action<ISingleAttributeContext>? configure = null)
            where TAttr : Attribute =>
            context.WithAssemblyAttribute(c =>
            {
                //TODO: validate that attribute can be applied to assembly
                c.From<TAttr>();
                configure?.Invoke(c);
            });

        public ISourceCodeFileContext WithAssemblyAttribute(INameInNamespaceToken type,
            Action<ISingleAttributeContext>? configure = null) =>
            context.WithAssemblyAttribute(c =>
            {
                c.From(type);
                configure?.Invoke(c);
            });
    }
}