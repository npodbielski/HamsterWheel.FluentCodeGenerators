using System;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IMethodContext :
    IContext,
    IMemberWithAccessModifier<IMethodContext>,
    ITypeMember<IMethodContext>,
    IParametersDefinitionsBagContext<IMethodContext>,
    IMemberWithTheBody<IMethodContext>
{
    IMethodContext Named(string name);
    IMethodContext WithReturnType(Action<ITypeUsageContext> configure);
    IMethodContext MakeOverride();
    IMethodContext MakeSealed();
    IMethodContext MakeAsync();
    IMethodContext MakeVirtual();
    IMethodContext MakePartial();
}