using System;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IParametersDefinitionsBagContext
{
    public CamelCaseName[] ParametersNames { get; }
}

public interface IParametersDefinitionsBagContext<out TContext> : IParametersDefinitionsBagContext
{
    TContext WithParameter(Action<IParameterDefinitionContext> configure);
}

