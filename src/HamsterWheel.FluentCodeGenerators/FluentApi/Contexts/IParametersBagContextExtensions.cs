namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class IParametersBagContextExtensions
{
    public static TContext WithParameter<TContext>(this IParametersValuesBagContext<TContext> context, string value,
        Action<IParameterValueContext>? configure = null)
    {
        return context.WithParameter(p =>
        {
            p.UseStringValue(value);
            configure?.Invoke(p);
        });
    }
}