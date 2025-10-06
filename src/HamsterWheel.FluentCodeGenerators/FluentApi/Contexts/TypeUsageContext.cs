using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class TypeUsageContext : CodeBuilderContextBase, ITypeUsageContext
{
    private readonly TypeNameChunk _typeChunk;

    public TypeUsageContext(CodeBuilderContextBase previous, TypeNameChunk typeChunk,
        int? numberOfGenericArguments = null) : base(previous)
    {
        _typeChunk = typeChunk;
        _typeChunk.ExpectedNumberOfTypeArguments = numberOfGenericArguments;
    }

    public ITypeUsageContext From(IName typeName, INamespace? typeNamespace = null,
        int? numberOfGenericArguments = null)
    {
        if (typeNamespace != null)
        {
            UsingsAppender.AddUsing(typeNamespace);
        }

        _typeChunk.ReplaceName((PascalCaseName)typeName);
        _typeChunk.ExpectedNumberOfTypeArguments = numberOfGenericArguments;
        return this;
    }

    public ITypeUsageContext WithGenericArgument(Action<ITypeUsageContext> configure)
    {
        var chunk = TypeNameChunk.Object;
        var context = From(this, chunk);
        configure(context);
        _typeChunk.AddGenericArgument(chunk);
        return this;
    }

    public ITypeUsageContext MakeArray()
    {
        _typeChunk.MakeArray();
        return this;
    }

    public ITypeUsageContext MakeNullable()
    {
        _typeChunk.MakeNullable();
        return this;
    }

    public static TypeUsageContext From(CodeBuilderContextBase previous, TypeNameChunk type) =>
        new(previous, type);
}

public static class TypeDefinitionContextExtensions
{
    public static ITypeUsageContext From<T>(this ITypeUsageContext context) => context.From(typeof(T));

    public static ITypeUsageContext WithGenericArgument<TArg>(this ITypeUsageContext context,
        Action<ITypeUsageContext>? configure = null) =>
        context.WithGenericArgument(typeof(TArg), configure);
}