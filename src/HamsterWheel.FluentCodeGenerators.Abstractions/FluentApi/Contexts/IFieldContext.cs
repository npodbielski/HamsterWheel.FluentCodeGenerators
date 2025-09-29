namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IFieldContext : ITypeMemberWithType<IFieldContext>, 
    ITypeMember<IFieldContext>,
    IMemberWithAccessModifier<IFieldContext>,
    IContext
{
    IFieldContext Named(string name);
    IFieldContext MakeNullable();
    IFieldContext DisableNullabilityWarning();
    IFieldContext WithInitializer(string value);
}