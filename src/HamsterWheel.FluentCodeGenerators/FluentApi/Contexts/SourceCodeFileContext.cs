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
    public static ISourceCodeFileContext WithUsings<T1>(this ISourceCodeFileContext context) =>
        context.WithUsings(typeof(T1));

    public static ISourceCodeFileContext WithUsings<T1, T2>(this ISourceCodeFileContext context) =>
        context.WithUsings(typeof(T1), typeof(T2));

    public static ISourceCodeFileContext WithUsings<T1, T2, T3>(this ISourceCodeFileContext context) =>
        context.WithUsings(typeof(T1), typeof(T2), typeof(T3));

    public static ISourceCodeFileContext WithUsings<T1, T2, T3, T4>(this ISourceCodeFileContext context) =>
        context.WithUsings(typeof(T1), typeof(T2), typeof(T3), typeof(T4));

    public static ISourceCodeFileContext WithUsings<T1, T2, T3, T4, T5>(this ISourceCodeFileContext context) =>
        context.WithUsings(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));

    public static ISourceCodeFileContext WithUsings(this ISourceCodeFileContext context, params Type[] types)
    {
        foreach (var type in types)
        {
            context.AddUsing(type);
        }

        return context;
    }

    public static ISourceCodeFileContext EnableNullability(this ISourceCodeFileContext context) =>
        context.AddPragma(p => p.Nullability(n => n.Enable()));

    public static IFileScopedNamespaceContext WithFileScopedNamespace(this ISourceCodeFileContext context,
        string @namespace) => context.WithFileScopedNamespace(@namespace.ToNamespace());

    public static ISourceCodeFileContext ConfigureUsing<T>(this ISourceCodeFileContext context)
        where T : IContextConfigurator<ISourceCodeFileContext>, new() => context.ConfigureUsing(new T());

    public static ISourceCodeFileContext WithAssemblyAttribute<TAttr>(this ISourceCodeFileContext context,
        Action<ISingleAttributeContext>? configure = null)
        where TAttr : Attribute =>
        context.WithAssemblyAttribute(c =>
        {
            //TODO: validate that attribute can be applied to assembly
            c.From<TAttr>();
            configure?.Invoke(c);
        });
}