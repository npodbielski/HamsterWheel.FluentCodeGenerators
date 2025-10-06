using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.Configurators;

public interface IContextConfigurator<in TContext> where TContext : IContext
{
    public void Configure(TContext context);
}