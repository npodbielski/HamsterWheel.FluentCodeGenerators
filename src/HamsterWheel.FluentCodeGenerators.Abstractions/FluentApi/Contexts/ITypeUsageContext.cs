namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface ITypeUsageContext : IContext, ITypeUsage<ITypeUsageContext>
{
    ITypeUsageContext MakeNullable();
    ITypeUsageContext MakeArray();
}