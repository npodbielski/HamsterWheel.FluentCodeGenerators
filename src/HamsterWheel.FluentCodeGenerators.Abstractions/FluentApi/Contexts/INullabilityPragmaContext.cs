namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface INullabilityPragmaContext : IContext
{
    IContext Enable();
    IContext Disable();
}