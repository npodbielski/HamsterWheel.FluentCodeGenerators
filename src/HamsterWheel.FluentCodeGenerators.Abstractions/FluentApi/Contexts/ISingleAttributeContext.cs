using System;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface ISingleAttributeContext
{
    ISingleAttributeContext From(Action<ITypeUsageContext> configure);
    ISingleAttributeContext WithParameter(Action<IParameterValueContext> configure);
}