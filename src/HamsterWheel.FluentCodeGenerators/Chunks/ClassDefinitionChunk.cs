using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Base;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks;

public class ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk name)
    : AttributesWithVisibilityChunk, INamedChunk
{
    internal TypeDefinitionWithPrimaryConstructorChunk ClassName { get; } = name;
    private bool _isStatic;
    private bool _isSealed;
    private bool _isAbstract;
    private bool _isPartial;
    public ImplementationsListChunk Implementations { get; private set; } = new();
    private SummaryCommentChunk? _comment;
    private readonly List<ICodeChunk> _stringChunks = [];
    private readonly List<CtorDefinitionChunk> _ctorChunks = [];
    private readonly List<MethodDefinitionChunk> _methodChunks = [];
    private readonly PropertiesCollectionChunk _propertiesCollectionChunk = new([]);
    private readonly FieldsCollectionChunk _fieldsCollectionChunk = new([]);
    public PascalCaseName Name => ClassName.TypeName;
    IName INamedChunk.Name => Name;

    public void ReplaceName(string className) => ClassName.ReplaceName(className);

    public void AddPrimaryCtor(PrimaryConstructorDefinitionChunk callChunk) =>
        ClassName.AddPrimaryConstructor(callChunk);

    public void AddCtor(CtorDefinitionChunk chunk)
    {
        chunk.SetName(ClassName);
        _ctorChunks.Add(chunk);
    }

    public void AddMethod(MethodDefinitionChunk chunk) => _methodChunks.Add(chunk);

    public void MakePartial() => _isPartial = true;

    public void MakeSealed() => _isSealed = true;
    public void MakeStatic() => _isStatic = true;
    public void MakeAbstract() => _isAbstract = true;

    public void Append(StringBuilder code) => _stringChunks.Add(new PlainValueChunk(code));

    public void AddProperty(PropertyDefinitionChunk newPropChunk) => _propertiesCollectionChunk.AddProp(newPropChunk);

    public void AddField(FieldDefinitionChunk newPropChunk) => _fieldsCollectionChunk.AddField(newPropChunk);

    public void AddComment(SummaryCommentChunk chunk) => _comment = chunk;

    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        if (_comment?.AppendChunks(stringBuilder) == true)
        {
            stringBuilder.AppendLine();
        }

        base.AppendChunks(stringBuilder);

        if (_isStatic)
        {
            new StaticKeywordChunk().AppendChunks(stringBuilder);
        }

        if (_isSealed)
        {
            new SealedKeywordChunk().AppendChunks(stringBuilder);
        }

        if (_isAbstract)
        {
            new AbstractKeywordChunk().AppendChunks(stringBuilder);
        }

        if (_isPartial)
        {
            new PartialKeywordChunk().AppendChunks(stringBuilder);
        }

        new ClassKeywordChunk().AppendChunks(stringBuilder);
        ClassName.AppendChunks(stringBuilder);
        Implementations.AppendChunks(stringBuilder);
        new BracesChunk(new IndentedChunk(new NewLineDelimitedJoinChunk(
        [
            _fieldsCollectionChunk,
            _propertiesCollectionChunk,
            .._ctorChunks,
            .._stringChunks,
            new DoubleNewLineDelimitedJoinChunk(_methodChunks)
        ]))).AppendChunks(stringBuilder);

        return true;
    }
}