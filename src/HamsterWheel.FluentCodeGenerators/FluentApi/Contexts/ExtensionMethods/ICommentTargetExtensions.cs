namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class ICommentTargetExtensions
{
    public static TContext WithComment<TContext>(this TContext context, string comment,
        Action<ISummaryCommentContext>? configure = null)
        where TContext : ICommentTarget<IContext>
    {
        context.WithComment(c =>
        {
            c.Append(comment);
            configure?.Invoke(c);
        });
        return context;
    }
}