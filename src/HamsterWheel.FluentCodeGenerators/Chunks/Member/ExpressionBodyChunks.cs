using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Body;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Member;

public class ExpressionBodyChunks : AppendableChunk
{
    public bool AutoSemicolon { get; set; }

    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        stringBuilder.AppendSingleSpace();
        stringBuilder.AppendChunk<LambdaExpressionOperatorChunk>();
        stringBuilder.Append(" ");
        base.AppendChunks(stringBuilder);
        if (AutoSemicolon && !Chunks.Last(c => !c.Build().IsNullOrWhiteSpace()).Build().EndsWith(";"))
        {
            stringBuilder.Append(";");
        }

        return Chunks.Count > 0;
    }
}