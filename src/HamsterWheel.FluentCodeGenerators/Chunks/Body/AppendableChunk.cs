using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Body;

public class AppendableChunk : ICodeChunk
{
    private readonly List<ICodeChunk> _chunks = [];

    public IReadOnlyCollection<ICodeChunk> Chunks => _chunks.AsReadOnly();

    public void Append(ICodeChunk newChunk) => _chunks.Add(newChunk);

    public void Append(string statement) => _chunks.Add(new PlainValueChunk(statement));

    public virtual bool AppendChunks(StringBuilder stringBuilder) =>
        new DelimitedJoinerChunk(Delimiter.Empty, [.._chunks]).AppendChunks(stringBuilder);
}