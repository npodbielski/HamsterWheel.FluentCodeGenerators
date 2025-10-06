using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IPrimaryCtorValuesBagContext
{
    CamelCaseName [] PrimaryCtorParametersNames { get; }
}