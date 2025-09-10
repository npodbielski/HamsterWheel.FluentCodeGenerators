using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IParameterValueContext
{
    IParameterValueContext MakeNamed(string name);
    IParameterValueContext UseStringValue(string value);
    IParameterValueContext UseExpression(string value);
    IParameterValueContext UseConst(object value);
}

public static class IParameterValueContextExtensions
{
    public static IParameterValueContext UseExpression(this IParameterValueContext context, IName name) =>
        context.UseExpression(name.ToString());
}