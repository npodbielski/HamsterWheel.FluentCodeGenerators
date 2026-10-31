using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IParameterValueContext : IContext
{
    IParameterValueContext MakeNamed(string name);
    IParameterValueContext UseStringValue(string value);
    IParameterValueContext UseConst(object value);
}

public static class IParameterValueContextExtensions
{
    public static IParameterValueContext UseName(this IParameterValueContext context, IName name) =>
        context.UseConst(name.ToString());
}