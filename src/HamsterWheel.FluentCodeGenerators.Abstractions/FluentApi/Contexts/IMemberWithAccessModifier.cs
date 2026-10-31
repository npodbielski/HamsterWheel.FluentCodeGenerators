using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IMemberWithAccessModifier<out TContext>
{
    TContext SetVisibility(MemberVisibility visibility);
}