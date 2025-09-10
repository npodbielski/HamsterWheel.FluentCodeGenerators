using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class IMemberWithAccessModifierExtensions
{
    public static TContext MakeProtected<TContext>(this IMemberWithAccessModifier<TContext> context) =>
        context.SetVisibility(MemberVisibility.Protected);

    public static TContext MakePublic<TContext>(this IMemberWithAccessModifier<TContext> context) =>
        context.SetVisibility(MemberVisibility.Public);

    public static TContext MakePrivate<TContext>(this IMemberWithAccessModifier<TContext> context) =>
        context.SetVisibility(MemberVisibility.Private);

    public static TContext MakePrivateProtected<TContext>(this IMemberWithAccessModifier<TContext> context) =>
        context.SetVisibility(MemberVisibility.PrivateProtected);

    public static TContext MakeProtectedInternal<TContext>(this IMemberWithAccessModifier<TContext> context) =>
        context.SetVisibility(MemberVisibility.ProtectedInternal);

    public static TContext MakeInternal<TContext>(this IMemberWithAccessModifier<TContext> context) =>
        context.SetVisibility(MemberVisibility.Internal);
}