using HamsterWheel.FluentCodeGenerators.Configurators;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class CodeBuilderContextExtensions
{
    public static T ConfigureUsing<T>(this T context, IContextConfigurator<T> configurator)
        where T : IContext
    {
        configurator.Configure(context);
        return context;
    }
}