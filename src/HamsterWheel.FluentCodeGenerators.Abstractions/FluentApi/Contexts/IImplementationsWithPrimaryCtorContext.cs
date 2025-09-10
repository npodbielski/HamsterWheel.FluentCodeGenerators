using System;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IImplementationsWithPrimaryCtorContext : IContext, ITypeUsage<IImplementationsWithPrimaryCtorContext>
{
    IImplementationsWithPrimaryCtorContext WithCtorCall(Action<IPrimaryConstructorCallContext> configure);
    IName[] ParametersNames { get; }
}