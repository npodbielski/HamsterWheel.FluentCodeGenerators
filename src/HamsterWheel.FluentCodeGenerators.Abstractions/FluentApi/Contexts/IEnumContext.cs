using System.Collections.Generic;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IEnumContext :
    IContext,
    IAttributeTarget<IEnumContext>,
    IMemberWithAccessModifier<IEnumContext>
{
    IEnumContext Named(string name);
    IEnumContext WithValue(string value, int? number = null);
    IEnumContext WithValues(IEnumerable<string> values);
}