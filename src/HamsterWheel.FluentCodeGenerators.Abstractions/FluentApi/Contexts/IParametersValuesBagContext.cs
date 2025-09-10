using System;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IParametersValuesBagContext<out TContext>
{
    CamelCaseName [] ParametersNames { get; }
    TContext WithParameter(Action<IParameterValueContext> configure);
}