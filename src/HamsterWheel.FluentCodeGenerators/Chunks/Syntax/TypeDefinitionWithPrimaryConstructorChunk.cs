using System.Text;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class TypeDefinitionWithPrimaryConstructorChunk(TypeNameChunk type) : ICodeChunk
{
    internal PrimaryConstructorDefinitionChunk? PrimaryConstructor { get; private set; }

    public string TypeName => (PascalCaseName)type.Name;

    public void ReplaceName(string name) => type.ReplaceName(name);

    public void AddGenericArgument(string typeName) => type.AddGenericArgument(new TypeNameChunk(typeName));

    public void AddPrimaryConstructor(PrimaryConstructorDefinitionChunk callChunk)
    {
        PrimaryConstructor = callChunk;
        PrimaryConstructor.Name = (PascalCaseName)type.Name;
    }

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        type.AppendChunks(stringBuilder);
        PrimaryConstructor?.AppendChunks(stringBuilder);

        return true;
    }

    public static TypeDefinitionWithPrimaryConstructorChunk FromName(string? name = null) =>
        new(new TypeNameChunk(name ?? "MyClass"));
}