using System;
using System.Text;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IClassContext : IContext, IAttributeTarget<IClassContext>,
    ICommentTarget<IClassContext>,
    IMemberWithAccessModifier<IClassContext>,
    IInterfaceTarget<IClassContext>,
    IInheritingType<IClassContext>,
    IFieldTarget<IClassContext>,
    IPropTarget<IClassContext>,
    IMethodTarget<IClassContext>,
    IPrimaryCtorValuesBagContext
{
    IClassContext MakeSealed();
    IClassContext Named(string name);
    IClassContext MakePartial();
    IClassContext MakeStatic();
    IClassContext WithPrimaryCtor(Action<IPrimaryConstructorDefinitionContext> configure);
    IClassContext WithCtor(Action<IConstructorContext> configure);
    IClassContext WithCode(StringBuilder code);
}