using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class ParameterValueContext(CodeBuilderContextBase previous, ParameterValueChunk parameterValue)
    : CodeBuilderContextBase(previous), IParameterValueContext
{
    internal bool IsAttributeParameter { get; set; }

    public IParameterValueContext MakeNamed(string name)
    {
        parameterValue.AddName(name, IsAttributeParameter ? " = " : null);
        return this;
    }

    public IParameterValueContext UseStringValue(string value)
    {
        parameterValue.SetValue(new StringValueChunk(value));
        return this;
    }

    public IParameterValueContext UseExpression(string value)
    {
        parameterValue.SetValue(new PlainValueChunk(value));
        return this;
    }

    public IParameterValueContext UseConst(object value)
    {
        parameterValue.SetValue(value);
        return this;
    }

    public static ParameterValueContext From(CodeBuilderContextBase previous, ParameterValueChunk chunk) =>
        new(previous, chunk);
}