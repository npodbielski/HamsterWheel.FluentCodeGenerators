using System;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IPropertyContext : IContext, ITypeMemberWithType<IPropertyContext>, ITypeMember<IPropertyContext>,
    IMemberWithAccessModifier<IPropertyContext>
{
    IPropertyContext Named(CamelCaseName name);
    IPropertyContext DisableNullabilityWarning();
    IPropertyContext WithInitializer(Action<IExpressionBodyContext> configure);
    IPropertyContext WithInitializer(ICodeChunk initChunk);
    IPropertyContext WithExpressionBody(Action<IExpressionBodyContext>? configure = null);
    IPropertyContext MakeGetOnly();
    IPropertyContext MakeOverride();
    IPropertyContext MakeSealed();
    IPropertyContext MakeNullable();
    IPropertyContext MakeComputed();
}