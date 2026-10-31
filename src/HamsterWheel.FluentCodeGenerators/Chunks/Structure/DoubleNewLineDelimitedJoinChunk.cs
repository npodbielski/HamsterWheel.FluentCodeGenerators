namespace HamsterWheel.FluentCodeGenerators.Chunks.Structure;

public class DoubleNewLineDelimitedJoinChunk(IEnumerable<ICodeChunk>? codeChunks = null)
    : DelimitedJoinerChunk(Delimiter.DoubleNewLine, codeChunks);