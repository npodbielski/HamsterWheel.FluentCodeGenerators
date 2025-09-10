using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class ImplementationsWithPrimaryCtorContext<TContext>(
    TContext previous,
    ImplementationsListChunk implementationsList)
    : CodeBuilderContextBase(previous), IImplementationsWithPrimaryCtorContext
    where TContext : CodeBuilderContextBase, IPrimaryCtorValuesBagContext, IContext
{
    private TypeUsageContext? _typeUsageContext;

    public IName[] ParametersNames => previous.PrimaryCtorParametersNames.Cast<IName>().ToArray();

    public IImplementationsWithPrimaryCtorContext From(IName typeName, INamespace? typeNamespace = null,
        int? numberOfGenericArguments = null)
    {
        EnsureBaseTypeContext().From(typeName, typeNamespace, numberOfGenericArguments);
        return this;
    }

    public IImplementationsWithPrimaryCtorContext WithGenericArgument(Action<ITypeUsageContext> configure)
    {
        EnsureBaseTypeContext().WithGenericArgument(configure);
        return this;
    }

    public IImplementationsWithPrimaryCtorContext WithCtorCall(Action<IPrimaryConstructorCallContext> configure)
    {
        //if type does not have primary ctor add empty one `()` i.e. via `previous` context that is being a class `previous.WithPrimaryCtor()`
        var chunk = new PrimaryConstructorCallChunk();
        implementationsList.AddPrimaryCtorCall(chunk);
        var context = PrimaryConstructorCallContext.From(this, chunk);
        configure(context);
        return this;
    }

    private TypeUsageContext EnsureBaseTypeContext()
    {
        if (_typeUsageContext is not null)
        {
            return _typeUsageContext;
        }

        var chunk = TypeNameChunk.Object;
        implementationsList.ReplaceBaseType(chunk);
        return _typeUsageContext = new TypeUsageContext(this, chunk);
    }

    public static ImplementationsWithPrimaryCtorContext<T> From<T>(T previous,
        ImplementationsListChunk implementations) where T : CodeBuilderContextBase, IPrimaryCtorValuesBagContext =>
        new(previous, implementations);
}

public static class ImplementationsWithPrimaryCtorContextExtensions
{
    public static IImplementationsWithPrimaryCtorContext From<T>(this IImplementationsWithPrimaryCtorContext context) =>
        context.From(typeof(T));

    public static IImplementationsWithPrimaryCtorContext WithGenericArgument<TArg>(
        this IImplementationsWithPrimaryCtorContext context,
        Action<ITypeUsageContext>? configure = null) =>
        context.WithGenericArgument(typeof(TArg), configure);
}