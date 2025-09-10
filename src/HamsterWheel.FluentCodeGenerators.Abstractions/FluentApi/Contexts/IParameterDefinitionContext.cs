namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IParameterDefinitionContext : IContext,
    IAttributeTarget<IParameterDefinitionContext>,
    ITypeMemberWithType<IParameterDefinitionContext>
{
    void PushToEnd();
    IParameterDefinitionContext Named(string name);
    IParameterDefinitionContext MakeNullable();
}