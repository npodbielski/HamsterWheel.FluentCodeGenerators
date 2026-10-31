using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Member;

public class FieldDefinitionChunk : MemberDefinitionChunk
{
    private bool _isConst;
    private bool _isStatic;
    private TypeNameChunk _type = new("object");
    private InitializerChunk? _initializer;

    private FieldDefinitionChunk(string name) : base(name) => SetVisibility(MemberVisibility.Private);

    protected override bool RenderMember(StringBuilder stringBuilder)
    {
        if (_isStatic)
        {
            new StaticKeywordChunk().AppendChunks(stringBuilder);
        }
        else if (_isConst)
        {
            new ConstKeywordChunk().AppendChunks(stringBuilder);
        }

        _type.AppendChunks(stringBuilder);
        stringBuilder.Append($" {Name}");
        if (_initializer is not null)
        {
            stringBuilder.AppendSingleSpace();
            _initializer.AppendChunks(stringBuilder);
        }
        else
        {
            stringBuilder.Append(";");
        }

        return true;
    }

    public void DisableNullableWarning() => _initializer = new DisableNullabilityWarningChunk();

    public void MakeConst()
    {
        _isConst = true;
        _isStatic = false; // const fields cannot be marked static in C# (CS0504)
    }

    public void MakeNullable() => _type.MakeNullable();

    public void MakeStatic() => _isStatic = true;

    public void SetType(TypeNameChunk typeNameChunk) => _type = typeNameChunk;

    public void WithInitializer(string value) => _initializer = new InitializerChunk(value);

    public static FieldDefinitionChunk From(string name) => new(name);
}
