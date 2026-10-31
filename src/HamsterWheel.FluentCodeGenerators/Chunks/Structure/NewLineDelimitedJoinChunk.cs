namespace HamsterWheel.FluentCodeGenerators.Chunks.Structure;

public class NewLineDelimitedJoinChunk(IEnumerable<ICodeChunk>? codeChunks = null)
    : DelimitedJoinerChunk(Delimiter.SingleNewLine, codeChunks);