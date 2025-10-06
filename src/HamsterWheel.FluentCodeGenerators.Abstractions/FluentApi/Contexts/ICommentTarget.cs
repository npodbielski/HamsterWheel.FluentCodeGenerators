using System;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface ICommentTarget<out TContext>
{
    public TContext WithComment(Action<ISummaryCommentContext> configure);
}