namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IFieldContext : ITypeMemberWithType<IFieldContext>, ITypeMember<IFieldContext>, IContext
{
    IFieldContext Named(string name);
}