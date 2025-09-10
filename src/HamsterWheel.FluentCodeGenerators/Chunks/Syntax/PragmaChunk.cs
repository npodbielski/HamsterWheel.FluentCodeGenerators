using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class PragmaChunk : ICodeChunk
{
    private readonly IList<ICodeChunk> _codeChunk = [];

    public void AddChunk(ICodeChunk codeChunk)
    {
        _codeChunk.Add(codeChunk);
    }

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        stringBuilder.Append("#");
        for (var index = 0; index < _codeChunk.Count; index++)
        {
            var chunk = _codeChunk[index];
            chunk.AppendChunks(stringBuilder);
            if (index < _codeChunk.Count - 1)
            {
                stringBuilder.AppendSingleSpace();
            }
        }

        return true;
    }
}