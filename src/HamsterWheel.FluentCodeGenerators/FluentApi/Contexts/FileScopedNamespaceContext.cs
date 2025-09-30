using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Enums;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class FileScopedNamespaceContext(CodeBuilderContextBase previous)
    : CodeBuilderContextBase(previous), IFileScopedNamespaceContext
{
    public IFileScopedNamespaceContext WithClass(Action<IClassContext> configure)
    {
        var classChunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName());
        FileChunks.AddType(classChunk);
        var classContext = new ClassContext(this, classChunk).WithGeneratedCodeAttr();
        configure(classContext);
        return this;
    }

    public IFileScopedNamespaceContext WithEnum(Action<IEnumContext> configurator)
    {
        var enumChunk = new EnumChunk();
        FileChunks.AddType(enumChunk);
        var context = EnumContext.From(this, enumChunk).WithGeneratedCodeAttr();
        configurator(context);
        return this;
    }
}

public static class FileScopedNamespaceContextExtensions
{
    public static IFileScopedNamespaceContext WithSealedClass(this IFileScopedNamespaceContext context, string className,
        Action<IClassContext>? configure = null)
    {
        return context.WithClass(c =>
        {
            c.Named(className).MakeSealed();
            configure?.Invoke(c);
        });
    }

    public static IFileScopedNamespaceContext WithClass(this IFileScopedNamespaceContext context, string className,
        Action<IClassContext>? configure = null)
    {
        return context.WithClass(c =>
        {
            c.Named(className);
            configure?.Invoke(c);
        });
    }
}