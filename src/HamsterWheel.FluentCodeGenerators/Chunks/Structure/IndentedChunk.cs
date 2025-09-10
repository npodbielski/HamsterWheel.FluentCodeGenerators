using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Structure;

public class IndentedChunk(params ICodeChunk[] chunks) : CompoundChunk(chunks)
{
    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        const string indent = "    ";
        var intermediateStringBuilder = new StringBuilder(indent);
        if (!base.AppendChunks(intermediateStringBuilder))
        {
            return false;
        }

        //add indentation
        intermediateStringBuilder.Replace('\n'.ToString(), $"\n{indent}");
        //strip indent from empty lines
        intermediateStringBuilder.Replace($"{indent}\n", "\n");
        stringBuilder.Append(intermediateStringBuilder.ToString().TrimEnd());
        stringBuilder.AppendLine();

        return true;
    }
}