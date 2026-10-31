namespace HamsterWheel.FluentCodeGenerators.Chunks.Structure;

public class CommaDelimitedJoinChunk(IEnumerable<ICodeChunk>? codeChunks = null)
    : DelimitedJoinerChunk(CommaWithSpaceDelimiter, codeChunks)
{
    private const string CommaWithSpaceDelimiter = ", ";
}