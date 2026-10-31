using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class InterfaceImplementationContext(CodeBuilderContextBase previous, TypeNameChunk type)
    : CodeBuilderContextBase(previous), IInterfaceImplementationContext
{
    private TypeUsageContext? _typeUsageContext;

    public IInterfaceImplementationContext From(IName typeName, INamespace? typeNamespace = null,
        int? numberOfGenericArguments = null)
    {
        EnsureBaseTypeContext().From(typeName, typeNamespace, numberOfGenericArguments);
        return this;
    }

    public IInterfaceImplementationContext WithGenericArgument(Action<ITypeUsageContext> configure)
    {
        EnsureBaseTypeContext().WithGenericArgument(configure);
        return this;
    }

    private TypeUsageContext EnsureBaseTypeContext()
    {
        if (_typeUsageContext is not null)
        {
            return _typeUsageContext;
        }

        return _typeUsageContext = new TypeUsageContext(this, type);
    }
}

public static class InterfaceImplementationContextExtensions
{
    extension(IInterfaceImplementationContext context)
    {
        public IInterfaceImplementationContext From<T>() =>
            context.From(typeof(T));

        public IInterfaceImplementationContext WithGenericArgument<TArg>(Action<ITypeUsageContext>? configure = null) =>
            context.WithGenericArgument(typeof(TArg), configure);
    }
}