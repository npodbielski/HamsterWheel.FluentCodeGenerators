using System;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface ITypeUsage<out TContext> : IUsingsAppender
{
    public TContext From(IName typeName, INamespace? typeNamespace = null, int? numberOfGenericArguments = null);
    TContext WithGenericArgument(Action<ITypeUsageContext> configure);
}
