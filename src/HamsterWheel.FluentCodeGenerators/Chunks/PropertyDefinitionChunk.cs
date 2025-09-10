using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks;

public class PropertyDefinitionChunk(string name, TypeNameChunk type) : ICodeChunk
{
    private bool _isAuto = true;
    private bool _getOnly;
    private bool _isStatic;
    private bool _isOverride;
    private MemberVisibility _visibility = MemberVisibility.Public;
    //TODO: move to setter context
    private MemberVisibility? _setterVisibility = null;
    private AttributesDefinitionChunk? _attributes = null;
    private InitializerChunk? _initializer;
    private bool _isSealed;

    public ExpressionBodyChunks GetExpressionBodyChunk { get; set; } = new();
    public ExpressionBodyChunks? SetExpressionBodyChunk { get; set; } = null;
    public string Name { get; private set; } = name;

    internal TypeNameChunk PropertyType { get; private set; } = type;

    public void ReplaceName(string name) => Name = name;

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        _attributes?.AppendChunks(stringBuilder);
        new VisibilityKeywordCodeChunk(_visibility).AppendChunks(stringBuilder);
        if (_isStatic)
        {
            new StaticKeywordChunk().AppendChunks(stringBuilder);
        }

        if (_isSealed)
        {
            new SealedKeywordChunk().AppendChunks(stringBuilder);
        }

        if (_isOverride)
        {
            new OverrideKeywordChunk().AppendChunks(stringBuilder);
        }

        PropertyType.AppendChunks(stringBuilder);
        stringBuilder.Append($" {Name}");
        if (_isAuto)
        {
            //TODO: move to BracesChunk
            stringBuilder.Append(" { get; ");
            if (!_getOnly)
            {
                if (_setterVisibility is not null && _setterVisibility != _visibility)
                {
                    new VisibilityKeywordCodeChunk((MemberVisibility)_setterVisibility!).AppendChunks(stringBuilder);
                }

                stringBuilder.Append("set; ");
            }

            stringBuilder.Append("}");
            if (_initializer is not null)
            {
                stringBuilder.AppendSingleSpace();
                _initializer.AppendChunks(stringBuilder);
            }
        }
        else
        {
            if (_getOnly || SetExpressionBodyChunk is null)
            {
                GetExpressionBodyChunk.AppendChunks(stringBuilder);
            }
            else
            {
                new BracesChunk(
                        new IndentedChunk(
                            new PlainValueChunk("get"),
                            GetExpressionBodyChunk,
                            new EmptyLineChunk(),
                            new PlainValueChunk("set"),
                            SetExpressionBodyChunk
                        ))
                    .AppendChunks(stringBuilder);
            }
        }

        return true;
    }

    public void MakeGetOnly() => _getOnly = true;

    public void MakeComputed() => _isAuto = false;

    public void MakeOverride()
    {
        _isOverride = true;
        if (_isStatic)
        {
            throw new InvalidOperationException("Static property cannot be override.");
        }
    }

    public void MakeNullable() => PropertyType.MakeNullable();

    public void DisableNullableWarning() => _initializer = new DisableNullabilityWarningChunk();

    public void WithInitializer(string value) => _initializer = new InitializerChunk(value);

    public void SetType(TypeNameChunk typeNameChunk) => PropertyType = typeNameChunk;

    public void MakeStatic() => _isStatic = true;
    public void MakeSealed() => _isSealed = true;
    public void SetVisibility(MemberVisibility value) => _visibility = value;

    public static PropertyDefinitionChunk From(string name, string? type = null)
    {
        var typeNameChunk = new TypeNameChunk(type ?? "string");
        return new PropertyDefinitionChunk(new PascalCaseName(name), typeNameChunk);
    }
}