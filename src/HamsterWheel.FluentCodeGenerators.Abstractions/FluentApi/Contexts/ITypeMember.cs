namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface ITypeMember<out TContext>
{
    TContext MakeStatic();
}