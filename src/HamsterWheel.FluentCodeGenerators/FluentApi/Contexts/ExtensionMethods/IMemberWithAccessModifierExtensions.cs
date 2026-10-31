using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class IMemberWithAccessModifierExtensions
{
    extension<TContext>(IMemberWithAccessModifier<TContext> context)
    {
        public TContext MakeProtected() =>
            context.SetVisibility(MemberVisibility.Protected);

        public TContext MakePublic() =>
            context.SetVisibility(MemberVisibility.Public);

        public TContext MakePrivate() =>
            context.SetVisibility(MemberVisibility.Private);

        public TContext MakePrivateProtected() =>
            context.SetVisibility(MemberVisibility.PrivateProtected);

        public TContext MakeProtectedInternal() =>
            context.SetVisibility(MemberVisibility.ProtectedInternal);

        public TContext MakeInternal() =>
            context.SetVisibility(MemberVisibility.Internal);
    }
}