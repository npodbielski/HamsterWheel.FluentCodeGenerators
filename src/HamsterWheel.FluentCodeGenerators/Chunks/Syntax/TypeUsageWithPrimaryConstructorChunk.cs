using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class TypeUsageWithPrimaryConstructorChunk(TypeNameChunk type, PrimaryConstructorCallChunk? ctor = null)
    : ICodeChunk
{
    private PrimaryConstructorCallChunk? _ctor = ctor;

    internal TypeNameChunk Type { get; } = type;

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        Type.AppendChunks(stringBuilder);
        _ctor?.AppendChunks(stringBuilder);

        return true;
    }

    public void AddPrimaryConstructor(PrimaryConstructorCallChunk ctorCall) => _ctor = ctorCall;

    public static TypeUsageWithPrimaryConstructorChunk FromType(TypeNameChunk chunk) => new(chunk);
}