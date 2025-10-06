using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class ImplementationsListChunk(TypeUsageWithPrimaryConstructorChunk? baseType = null) : ICodeChunk
{
    private readonly List<TypeNameChunk> _interfaces = [];
    internal TypeUsageWithPrimaryConstructorChunk? TypeWithCtor { get; private set; } = baseType;

    private bool HaveInterfaces() => _interfaces.Any();

    public void ReplaceBaseType(TypeNameChunk baseType) =>
        TypeWithCtor = TypeUsageWithPrimaryConstructorChunk.FromType(baseType);

    public void AddPrimaryCtorCall(PrimaryConstructorCallChunk callChunk)
    {
        TypeWithCtor ??= TypeUsageWithPrimaryConstructorChunk.FromType(TypeNameChunk.Object);
        TypeWithCtor.AddPrimaryConstructor(callChunk);
    }

    public void AddInterface(TypeNameChunk interfaceChunk) => _interfaces.Add(interfaceChunk);

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        if (TypeWithCtor is null && !HaveInterfaces())
        {
            return false;
        }

        stringBuilder.Append(" : ");
        TypeWithCtor?.AppendChunks(stringBuilder);

        if (!HaveInterfaces())
        {
            return true;
        }

        if (TypeWithCtor is not null)
        {
            stringBuilder.Append(", ");
        }

        var joiner = new CommaDelimitedJoinChunk(codeChunks: _interfaces);
        joiner.AppendChunks(stringBuilder);

        return true;
    }
}