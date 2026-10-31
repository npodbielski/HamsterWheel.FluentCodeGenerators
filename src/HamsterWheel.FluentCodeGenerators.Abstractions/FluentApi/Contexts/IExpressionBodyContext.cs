using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IExpressionBodyContext : IBodyBuilderContext
{
    CamelCaseName[] ParameterNames { get; }
}