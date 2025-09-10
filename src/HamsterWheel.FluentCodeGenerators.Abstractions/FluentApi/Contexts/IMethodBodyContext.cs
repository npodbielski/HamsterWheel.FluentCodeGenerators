using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IMethodBodyContext : IBodyBuilderContext
{
    CamelCaseName[] ParameterNames { get; }
}