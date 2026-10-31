using HamsterWheel.FluentCodeGenerators.Members;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IFieldContext : ITypeMemberWithType<IFieldContext>,
    ITypeMember<IFieldContext>,
    IMemberWithAccessModifier<IFieldContext>,
    IContext
{
    IFieldContext Named(IName name);
    IFieldContext MakeNullable();
    IFieldContext DisableNullabilityWarning();
    IFieldContext WithInitializer(string value);
    IFieldContext MakeConst();
    IFieldContext When(Func<MemberDefinitionRecord, bool> predicate);
}