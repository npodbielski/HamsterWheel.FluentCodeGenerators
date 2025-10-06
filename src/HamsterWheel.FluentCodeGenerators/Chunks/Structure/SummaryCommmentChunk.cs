using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Structure;

public class SummaryCommentChunk(string? comment = null) : ICodeChunk
{
    private readonly List<string> _commentLines = comment is not null ? [comment] : [];

    public void Append(string line) => _commentLines.Add(line);

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        var lines = new NewLineDelimitedJoinChunk(
        [
            new CommentChunk("<summary>"),
            .. _commentLines.SelectMany(c => c.Split('\r', '\n').Select(l => new CommentChunk(l))),
            new CommentChunk("</summary>")
        ]);

        lines.AppendChunks(stringBuilder);

        return true;
    }
}