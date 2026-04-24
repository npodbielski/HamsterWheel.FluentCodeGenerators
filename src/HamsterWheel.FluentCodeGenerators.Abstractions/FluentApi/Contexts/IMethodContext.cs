namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IMethodContext :
    ICommentTarget<IMethodContext>,
    IContext,
    IMemberWithAccessModifier<IMethodContext>,
    ITypeMember<IMethodContext>,
    IParametersDefinitionsBagContext<IMethodContext>,
    IMemberWithTheBody<IMethodContext>,
    IAttributeTarget<IMethodContext>
{
    IMethodContext Named(string name);
    IMethodContext WithReturnType(Action<ITypeUsageContext> configure);
    IMethodContext MakeOverride();
    IMethodContext MakeSealed();
    IMethodContext MakeAsync();
    IMethodContext MakeAsyncValueTask();
    IMethodContext MakeVirtual();
    IMethodContext MakePartial();
}