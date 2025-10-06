using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Base;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Enums;

public class EnumChunk(string name = "MyEnum") : AttributesWithVisibilityChunk, INamedChunk
{
    private readonly List<EnumValueChunk> _valueChunks = [];

    public PascalCaseName Name { get; private set; } = name;
    IName INamedChunk.Name => Name;
    
    public void ReplaceName(string enumName) => Name = enumName;

    public void AddValue(EnumValueChunk valueChunk) => _valueChunks.Add(valueChunk);

    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        base.AppendChunks(stringBuilder);
        new EnumKeywordChunk().AppendChunks(stringBuilder);
        stringBuilder.Append(Name);
        new BracesChunk(
            new IndentedChunk(new NewLineDelimitedJoinChunk(_valueChunks.Select(ICodeChunk (c) => c).ToArray()))
        ).AppendChunks(stringBuilder);

        return true;
    }
}