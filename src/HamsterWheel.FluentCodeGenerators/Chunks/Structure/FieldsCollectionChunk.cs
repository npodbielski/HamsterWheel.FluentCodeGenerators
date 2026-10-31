using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Structure;

public class FieldsCollectionChunk(IEnumerable<FieldDefinitionChunk> chunks) : ICodeChunk
{
    private readonly List<FieldDefinitionChunk> _chunks = [.. chunks];

    public bool AppendChunks(StringBuilder stringBuilder) =>
        new NewLineDelimitedJoinChunk(_chunks).AppendChunks(stringBuilder);

    public void AddField(FieldDefinitionChunk newPropChunk) => _chunks.Add(newPropChunk);
}